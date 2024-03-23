using MaxiShop.Domain.Contracts;
using MaxiShop.Domain.Models;
using MaxiShop.Infrastructure.DbContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaxiShop.Infrastructure.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext dbcontext):base(dbcontext)
        {
            
        }

        public async Task UpdateAsync(Product product)
        {
            _dbContext.Update(product);
            await _dbContext.SaveChangesAsync();
        }
    }
}
