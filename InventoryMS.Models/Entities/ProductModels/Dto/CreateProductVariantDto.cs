using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryMS.Models.Entities.ProductModels.Dto
{
    public class CreateProductVariantDto
    {
        public string ProductId { get; set; } // Foreign key to the Product entity
        public string ColorId { get; set; } // Foreign key to the Color entity
        public string SizeId { get; set; } // Foreign key to the Size entity
        public string? LotId { get; set; } //  Foreign key to the Lot entity
        public string SKU { get; set; }
        public string? Barcode { get; set; }
        public int MinimumStock { get; set; }
    }
}
