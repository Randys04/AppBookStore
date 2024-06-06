using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppBookStore.Models.Domain;
using AppBookStore.Models.DTO;
using AppBookStore.Repositories.Abstract;
using Microsoft.AspNetCore.Identity;

namespace AppBookStore.Repositories.Implementation
{
    public class UserAuthenticationService : IUserAuthenticationService
    {
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;

        public UserAuthenticationService(UserManager<AppUser> _userManager, SignInManager<AppUser> _signInManager)
        {
            userManager = _userManager;
            signInManager = _signInManager;
        }

        public async Task<Status> LoginAsync(LoginModel login)
        {
            var status = new Status();
            var user = await userManager.FindByNameAsync(login.Username!);

            if (user == null)
            {
                status.StatusCode = 0;
                status.Message = "Invalid Username";
                return status;
            }

            if(!await userManager.CheckPasswordAsync(user, login.Password!)) // valida si la contrasena es valida, entra al if en caso de que NO lo sea
            {
                status.StatusCode = 0;
                status.Message = "Invalid password";
                return status;
            }

            var result = await signInManager.PasswordSignInAsync(user, login.Password!, true, false);

            if(!result.Succeeded)
            {
                status.StatusCode = 0;
                status.Message = "Credentials are invlaid";
                return status;
            }

            status.StatusCode = 1;
            status.Message = "Successful login";
            return status;
        }

        public async Task LogoutAsync()
        {
            await signInManager.SignOutAsync();
        }
    }
}