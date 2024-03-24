﻿using MaxiShop.Application.DTO.Category;
using MaxiShop.Application.DTO.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaxiShop.Application.Services.Interfaces
{
    public interface IProductService
    {
        Task<ProductDto> GetByIdAsync(int id);
        Task<IEnumerable<ProductDto>> GetAllAsync();
        Task<ProductDto> CreateAsync(CreateProductDto createProductDto);
        Task UpdateAsync(UpdateProductDto updateProductDto);
        Task DeleteAsync(int id);
    }
}