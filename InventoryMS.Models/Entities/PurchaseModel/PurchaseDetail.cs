using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace InventoryMS.Models.Entities.PurchaseModel
{
    public class PurchaseDetail
    {
        [Key]
        public int PurchaseDetailId { get; set; }
        public int PurchaseId { get; set; }
        public int VariantId { get; set; }
        public int LotId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Discount { get; set; }
        public decimal VatPercentage { get; set; }
        public decimal VatAmount { get; set; }
        public decimal Total { get; set; }
    }
}
