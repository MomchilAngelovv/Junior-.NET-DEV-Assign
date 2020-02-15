namespace JuniorAssign.Api.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Mvc;

    using JuniorAssign.Services.Users;
    using JuniorAssign.Api.Models.Users;

    public class UsersController : ApiControllerBase
    {
        private readonly IUsersService usersService;

        public UsersController(IUsersService usersService)
        {
            this.usersService = usersService;
        }

        public ActionResult<List<UserDetailsApiModel>> GetAll()
        {
            var users = this.usersService
                .GetAll()
                .Select(u => new UserDetailsApiModel 
                {
                    Id = u.Id,
                    Username = u.Username
                })
                .ToList();

            return users;
        }

        [HttpPost]
        public async Task<ActionResult> Create(CreateUserInputModel input)
        {
            var createdUserId = await this.usersService.CreateAsync(input.Username, input.Password);
            return this.Created($"/users/{createdUserId}", null);
        }

        [HttpGet("{userId}")]
        public ActionResult<UserDetailsApiModel> Get(string userId)
        {
            var user = this.usersService.GetBy(userId);

            if (user == null)
            {
                return this.NotFound();
            }

            var userDetailsApiModel = new UserDetailsApiModel
            {
                Id = user.Id,
                Username = user.Username
            };

            return userDetailsApiModel;
        }

        [HttpPut("{userId}")]
        public async Task<ActionResult> Update(string userId, UpdateUserInputModel input)
        {
            await this.usersService.UpdateAsync(userId, input.Username, input.Password);
            return this.Ok();
        }
    }
}
