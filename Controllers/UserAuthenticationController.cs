using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppBookStore.Models.DTO;
using AppBookStore.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace AppBookStore.Controllers
{
    public class UserAuthenticationController : Controller
    {
        private readonly IUserAuthenticationService _authService;

        public UserAuthenticationController(IUserAuthenticationService authService)
        {
            _authService = authService;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel login)
        {
            if (!ModelState.IsValid)
            {
                return View(login);
            }

            var result = await _authService.LoginAsync(login);

            if (result.StatusCode == 1)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                TempData["msg"] = result.Message;
                return RedirectToAction(nameof(Login));
            }
        }

        public async Task<IActionResult> Logout()
        {
            await _authService.LogoutAsync();
            return RedirectToAction(nameof(Login));
        }
    }
}