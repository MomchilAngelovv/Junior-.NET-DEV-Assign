namespace JuniorAssign.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Identity;

    using JuniorAssign.Data.Models;
    using JuniorAssign.Services.Users;
    using JuniorAssign.Web.Models.Users;

    public class UsersController : Controller
    {
        private readonly IUsersService usersService;
        private readonly SignInManager<User> signInManager;

        public UsersController(IUsersService usersService, SignInManager<User> signInManager)
        {
            this.usersService = usersService;
            this.signInManager = signInManager;
        }

        public IActionResult Login()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLoginInputModel input)
        {
            if (this.ModelState.IsValid == false)
            {
                return this.BadRequest();
            }

            var user = this.usersService.GetBy(input.Username, input.Password);

            if (user == null)
            {
                return this.BadRequest();
            }

            await this.signInManager.SignInAsync(user, input.IsPersistent);
            return this.Redirect("/");
        }

        public IActionResult Register()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterInputModel input)
        {
            if (ModelState.IsValid == false)
            {
                return this.BadRequest();
            }

            if (input.Password != input.RepeatPassword)
            {
                return this.BadRequest();
            }

            await this.usersService.CreateAsync(input.Username, input.Password);
            return this.Redirect(nameof(Login));
        }

        public async Task<IActionResult> Logout()
        {
            await this.signInManager.SignOutAsync();
            return this.Redirect("/");
        }
    }
}
