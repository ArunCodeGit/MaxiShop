﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaxiShop.Application.DTO.Product
{
    public class CreateProductDto
    {
        public int CategoryID { get; set; }

        public int BrandID { get; set; }

        public string Name { get; set; }

        public string Specification { get; set; }

        public double Price { get; set; }
    }
}
