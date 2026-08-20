using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryMS.Models.Entities.ProductModels.Dto
{
    public class CreateProductDto
    {
        public string ProductName { get; set; }
        public string CategoryId { get; set; } // Foreign key to Category
        public string BrandId { get; set; } // Foreign key to Brand
        public string UnitId { get; set; } // Foreign key to Unit
    }
}
