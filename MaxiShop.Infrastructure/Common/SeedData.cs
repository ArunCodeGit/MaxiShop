using MaxiShop.Domain.Models;
using MaxiShop.Infrastructure.DbContexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaxiShop.Infrastructure.Common
{
    public class SeedData
    {
        public static async Task SeedRoles(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();

            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            var roles = new List<IdentityRole>
            {
                new IdentityRole{Name="ADMIN", NormalizedName="ADMIN"},
                new IdentityRole{Name="CUSTOMER", NormalizedName="CUSTOMER"}
            };

            foreach(var role in roles)
            {
                if(!await roleManager.RoleExistsAsync(role.Name))
                {
                    await roleManager.CreateAsync(role);
                }
            }
        }


        public static async Task SeedDataAsync(ApplicationDbContext _dbcontext)
        {
            if (!_dbcontext.Brand.Any())
            {
                await _dbcontext.AddRangeAsync(
                    new Brand
                    {
                        BrandName="Apple",
                        EstablishedYear=1990
                    },
                    new Brand
                    {
                        BrandName="Samsung",
                        EstablishedYear=1995
                    },
                    new Brand
                    {
                        BrandName="Nokia",
                        EstablishedYear=1992
                    },
                    new Brand
                    {
                        BrandName="HP",
                        EstablishedYear=2000
                    },
                    new Brand
                    {
                        BrandName="DELL",
                        EstablishedYear = 2001
                    });
                await _dbcontext.SaveChangesAsync();
            }
        }
    }
}
