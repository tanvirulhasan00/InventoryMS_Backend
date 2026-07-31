using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace InventoryMS.Models.Entities.SalesModel
{
    public class SalesDetail
    {
        [Key]
        public int SalesDetailId { get; init; }
        public int SalesId { get; set; }
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
