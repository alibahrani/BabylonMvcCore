using AliAspCore.Models.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AliAspCore.Controllers
{
    public class AccountController: Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signinManager;
        public AccountController(UserManager<IdentityUser> userManager, 
            SignInManager<IdentityUser> signinManager
            )
        {
            _userManager = userManager;
            _signinManager = signinManager;
        }


        public IActionResult Login()
        {
            return View(new LoginViewModel());

        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel login, string returnUrl = null)
        {
            if(!ModelState.IsValid)
            {
                return View();
            }
            var result = await _signinManager.PasswordSignInAsync(
                login.EmailAddress, login.Password, login.RememberMe, false
                );
            if(!result.Succeeded)
            {
                ModelState.AddModelError("", "Login Error!");
                return View();
            }

            if (string.IsNullOrWhiteSpace(returnUrl))
                return RedirectToAction("Index", "Home");

            return Redirect(returnUrl); 
        }

        [HttpPost]
        public async Task<IActionResult> Logout(string returnUrl = null)
        {
            await _signinManager.SignOutAsync();
            if (string.IsNullOrWhiteSpace(returnUrl))
                return RedirectToAction("Index", "Home");
            return Redirect(returnUrl);
        }

        public IActionResult Register()
        {
            return View(new RegisterViewModel());
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registeration)
        {
            if (!ModelState.IsValid)
                return View(registeration);
            var newUser = new IdentityUser
            {
                Email = registeration.EmailAddress,
                UserName = registeration.EmailAddress,
            };

            var result =await _userManager.CreateAsync(newUser, registeration.Password);
            if (!result.Succeeded)
            {
                foreach(var error in result.Errors.Select(x => x.Description))
                {
                    ModelState.AddModelError("", error);
                }
                return View();
            }
            return RedirectToAction("Login");
        }
    }
}
