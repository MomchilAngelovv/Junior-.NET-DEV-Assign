using JuniorAssign.Data.Models;
using JuniorAssign.Services.Users;
using JuniorAssign.Web.Models.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JuniorAssign.Web.Controllers
{
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
    }
}
