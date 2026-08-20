using InventoryMS.Models.Entities.LotModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace InventoryMS.Models.Entities.ProductModels
{
    public class ProductVariant
    {
        [Key]
        public Guid VariantId { get; init; }
        public Guid ProductId { get; set; } // Foreign key to the Product entity
        [ForeignKey(nameof(ProductId))]
        public Product Product { get; set; }
        public Guid ColorId { get; set; } // Foreign key to the Color entity
        [ForeignKey(nameof(ColorId))]
        public Color Color { get; set; }
        public Guid SizeId { get; set; } // Foreign key to the Size entity
        [ForeignKey(nameof(SizeId))]
        public Size Size { get; set; }
        public Guid? LotId { get; set; } //  Foreign key to the Lot entity
        [ForeignKey(nameof(LotId))]
        public Lot? Lot { get; set; }
        public string SKU { get; set; }
        public string? Barcode { get; set; }
        public int MinimumStock { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
