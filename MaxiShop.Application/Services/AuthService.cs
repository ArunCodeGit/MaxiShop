using MaxiShop.Application.Common;
using MaxiShop.Application.InputModels;
using MaxiShop.Application.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaxiShop.Application.Services
{
    internal class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signManager;

        private ApplicationUser applicationUser;

        public AuthService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signManager = null)
        {
            _userManager = userManager;
            applicationUser = new();
            _signManager = signManager;
        }

        public async Task<IEnumerable<IdentityError>> Register(Register register)
        {
            applicationUser.FirstName = register.FirstName;
            applicationUser.LastName = register.LastName;
            applicationUser.Email = register.Email;
            applicationUser.UserName = register.Email;

            var result = await _userManager.CreateAsync(applicationUser, register.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(applicationUser, "ADMIN");
            }

            return result.Errors;
        }

        public async Task<object> Login(Login login)
        {
            applicationUser = await _userManager.FindByEmailAsync(login.Email);

            if(applicationUser == null)
            {
                return "Invalid Email Address";
            }

            var result = await _signManager.PasswordSignInAsync(login.Email, login.Password, isPersistent:true, lockoutOnFailure:true);

            var isValidCredential = await _userManager.CheckPasswordAsync(applicationUser, login.Password);

            if (result.Succeeded)
            {
                return true;
            }
            else
            {
                if (result.IsLockedOut)
                {
                    return "Your account is locked, contact system admin";
                }
                if (result.IsNotAllowed)
                {
                    return "Please verify Email Address";
                }
                if(isValidCredential == false)
                {
                    return "Invalid Password";
                }
                else
                {
                    return "Login Failed!";
                }
            }
        }
    }
}
