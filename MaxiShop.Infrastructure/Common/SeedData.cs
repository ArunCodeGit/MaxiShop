using MaxiShop.Domain.Models;
using MaxiShop.Infrastructure.DbContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaxiShop.Infrastructure.Common
{
    public class SeedData
    {
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
