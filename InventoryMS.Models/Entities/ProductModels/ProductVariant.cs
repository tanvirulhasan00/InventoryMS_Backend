using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace InventoryMS.Models.Entities.ProductModels
{
    public class ProductVariant
    {
        [Key]
        public int VariantId { get; init; }
        public int ProductId { get; set; }
        public int ColorId { get; set; }
        public int SizeId { get; set; }
        public string SKU { get; set; }
        public string? Barcode { get; set; }
        public int MinimumStock { get; set; }
        public bool IsActive { get; set; }
    }
}
