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
        private ApplicationUser applicationUser;

        public AuthService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            applicationUser = new();
        }

        public async Task<IEnumerable<IdentityError>> Register(Register register)
        {
            applicationUser.FirstName = register.FirstName;
            applicationUser.LastName = register.LastName;
            applicationUser.Email = register.Email;
            applicationUser.UserName = register.Email;

            var result = await _userManager.CreateAsync(applicationUser);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(applicationUser, "ADMIN");
            }

            return result.Errors;
        }
    }
}
