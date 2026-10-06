using InventoryMS.Models.Entities.PurchaseModels;
using InventoryMS.Models.Entities.SupplierModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace InventoryMS.Models.Entities.LotModel
{
    public class Lot
    {
        [Key]
        public Guid LotId { get; init; }
        public int LotNumber { get; set; }
        public Guid PurchaseDetailId { get; set; } // Foreign key to the Purchase entity
        [ForeignKey(nameof(PurchaseDetailId))]
        public PurchaseDetail PurchaseDetail { get; set; }
        public DateTime ReceivedDate { get; set; }
        public string? Status { get; set; } // e.g., "Draft", "Confirmed", "Received", "Cancelled"
        public Guid SupplierId { get; set; } // Foreign key to the Supplier entity
        [ForeignKey(nameof(SupplierId))]
        public Supplier Supplier { get; set; }
        public string? Remarks { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
