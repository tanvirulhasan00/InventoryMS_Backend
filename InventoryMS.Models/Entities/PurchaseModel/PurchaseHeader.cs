using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace InventoryMS.Models.Entities.PurchaseModel
{
    public class PurchaseHeader
    {
        [Key]
        public int PurchaseId { get; set; }
        public int PurchaseNo { get; set; }
        public int SupplierId { get; set; }
        public string InvoiceNo { get; set; }
        public DateTime PurchaseDate { get; set; }
        public int WarehouseId { get; set; }
        public string? Remarks { get; set; }
        public int CreatedBy { get; set; }
    }
}
