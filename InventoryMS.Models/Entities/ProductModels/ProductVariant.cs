using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace InventoryMS.Models.Entities.ProductModels
{
    public class ProductVariant
    {
        [Key]
        public Guid VariantId { get; init; }
        public Guid ProductId { get; set; } // Foreign key to the Product entity
        public Guid ColorId { get; set; } // Foreign key to the Color entity
        public Guid SizeId { get; set; } // Foreign key to the Size entity
        public string SKU { get; set; }
        public string? Barcode { get; set; }
        public int MinimumStock { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
