using MaxiShop.Application.Common;
using MaxiShop.Application.InputModels;
using MaxiShop.Application.Services.Interfaces;
using MaxiShop.Application.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MaxiShop.Application.Services
{
    internal class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signManager;
        private readonly IConfiguration _config;

        private ApplicationUser applicationUser;

        public AuthService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signManager = null, IConfiguration config = null)
        {
            _userManager = userManager;
            applicationUser = new();
            _signManager = signManager;
            _config = config;
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
                var token = await GenerateToken();

                LoginResponse loginResponse = new LoginResponse()
                {
                    UserId = applicationUser.Id,
                    Token = token
                };

                return loginResponse;
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

        public async Task<string> GenerateToken()
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JwtSettings:Key"]));

            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var roles = await _userManager.GetRolesAsync(applicationUser);

            var roleClaims = roles.Select(x=>new Claim(ClaimTypes.Role, x)).ToList();

            List<Claim> claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Email, applicationUser.Email)
            }.Union(roleClaims).ToList();

            var token = new JwtSecurityToken
                (
                issuer: _config["JwtSettings:Issuer"],
                audience: _config["JwtSettings: Audience"],
                claims: claims,
                signingCredentials: signingCredentials,
                expires: DateTime.UtcNow.AddMinutes(Convert.ToInt32(_config["JwtSeetings: DurationInMinutes"]))
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        } 
    }
}
