namespace JuniorAssign.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;

    using JuniorAssign.Services.Users;
    using JuniorAssign.Services.Contragents;
    using JuniorAssign.Web.Models.Contragents;

    [Authorize]
    public class ContragentsController : Controller
    {
        private readonly IContragentsService contragentsService;
        private readonly IUsersService usersService;

        public ContragentsController(IContragentsService contragentsService, IUsersService usersService)
        {
            this.contragentsService = contragentsService;
            this.usersService = usersService;
        }

        public IActionResult All()
        {
            var userId = this.usersService.GetUserIdBy(this.User.Identity.Name);
            var contragents = this.contragentsService
                .GetAll(userId)
                .Select(c => new ContragentViewModel 
                { 
                    Id = c.Id,
                    Name = c.Name
                });

            var contragentsAllViewModel = new ContragentsAllViewModel
            {
                Contragents = contragents
            };

            return this.View(contragentsAllViewModel);
        }

        public IActionResult Create()
        {
            var emptyContragentCreateInputModel = new ContragentCreateInputModel();
            return this.View(emptyContragentCreateInputModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ContragentCreateInputModel input)
        {
            if (this.ModelState.IsValid == false)
            {
                return this.View(input);
            }

            var userId = this.usersService.GetUserIdBy(this.User.Identity.Name);

            if (userId == null)
            {
                return this.NotFound();
            }

            await this.contragentsService.CreateAsync(input.Name, input.Address, input.Email, input.VatNumber, userId);
            return this.RedirectToAction(nameof(All));
        }

        public IActionResult Details(string id)
        {
            var contragent = this.contragentsService.GetBy(id);

            var contragentDetailsViewModel = new ContragentDetailsViewModel
            {
                Name = contragent.Name,
                Address = contragent.Address,
                Email = contragent.Email,
                VatNumber = contragent.VatNumber
            };

            return this.View(contragentDetailsViewModel);
        }
    }
}
