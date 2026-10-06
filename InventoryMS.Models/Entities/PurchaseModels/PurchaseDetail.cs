using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace InventoryMS.Models.Entities.PurchaseModels
{
    public class PurchaseDetail
    {
        [Key]
        public Guid PurchaseDetailId { get; init; }
        public Guid PurchaseId { get; set; } // Foreign key to the PurchaseHeader entity
        public Guid? VariantId { get; set; } // Foreign key to the ProductVariant entity
       
        public int OrderQuantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Discount { get; set; }
        public decimal VatPercentage { get; set; }
        public decimal VatAmount { get; set; }
        public decimal Total { get; set; }
        public string? Remarks { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
