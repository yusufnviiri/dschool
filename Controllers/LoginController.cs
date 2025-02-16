﻿using victors.Actions;
using victors.Models.Context;
using victors.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using victors.Models.Helper;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication;

namespace victors.Controllers
{

    public class LoginController : Controller
    {
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        public StudentActions studentActions { get; set; } = new();

        private readonly ApplicationDbContext _db;

        private readonly UserActions _userActions = new();

       
        private readonly SignInManager<User> _signInManager;

        public LoginController(IMapper mapper, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
        }


        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(UserRegistrationModel userModel)
        {
            if (!ModelState.IsValid)
            {
                return View(userModel);
            }

            var user = _mapper.Map<User>(userModel);

            var result = await _userManager.CreateAsync(user, userModel.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }

                return View(userModel);
            }
            switch (userModel.Function)
            {
                case "Burser":
                    await _userManager.AddToRoleAsync(user, "Burser");
                    break;
                case "Administrator":
                    await _userManager.AddToRoleAsync(user, "Administrator");
                    break;
                case "Parent":
                    await _userManager.AddToRoleAsync(user, "Parent");
                    break;
                default:
                    await _userManager.AddToRoleAsync(user, "Visitor");
                    break;
            }

            return RedirectToAction(nameof(HomeController.Index), "Home");
        }






        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(UserLoginModel userModel, string returnUrl = null)
        {
            if (!ModelState.IsValid)
            {
                return View(userModel);
            }

            var result = await _signInManager.PasswordSignInAsync(userModel.Email, userModel.Password, userModel.RememberMe, false);
            if (result.Succeeded)
            {
                return RedirectToLocal(returnUrl);
            }
            else
            {
                ModelState.AddModelError("", "Invalid UserName or Password");
                return View();
            }
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
                return Redirect(returnUrl);
            else
                return RedirectToAction(nameof(HomeController.Index), "Home");

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction(nameof(HomeController.Index), "Home");
        }






    }

}
