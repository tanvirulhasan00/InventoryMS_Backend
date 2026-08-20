using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryMS.Models.Entities.ProductModels.Dto
{
    public class UpdateProductDto
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public string BrandId { get; set; } // Foreign key to Brand
        public string UnitId { get; set; } // Foreign key to Unit
    }
}
