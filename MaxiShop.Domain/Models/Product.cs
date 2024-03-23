using MaxiShop.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaxiShop.Domain.Models
{
    public class Product:BaseModel
    {
        public int CategoryID { get; set; }

        [ForeignKey("CategoryID")]
        public Category Category { get; set; }
        
        public int BrandID { get; set; }

        [ForeignKey("BrandID")]
        public Brand Brand { get; set; }

        public string Name { get; set; }

        public string Specification { get; set; }

        public double Price { get; set; }
    }
}
