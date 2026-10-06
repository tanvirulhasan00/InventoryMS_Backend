using InventoryMS.Models.Entities.SupplierModel;
using InventoryMS.Models.Entities.WarehouseModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace InventoryMS.Models.Entities.PurchaseModels
{
    public class PurchaseHeader
    {
        [Key]
        public Guid PurchaseId { get; init; }
        public int PurchaseNo { get; set; }
        public Guid SupplierId { get; set; } // Foreign key to the Supplier entity
        [ForeignKey(nameof(SupplierId))]
        public Supplier Supplier { get; set; }
        public string InvoiceNo { get; set; }
        public DateTime PurchaseDate { get; set; }
        public Guid WarehouseId { get; set; } // Foreign key to the Warehouse entity
        [ForeignKey(nameof(WarehouseId))]
        public Warehouse Warehouse { get; set; }
        public string? Remarks { get; set; }
        public string? Status { get; set; } // e.g., "Draft", "Confirmed", "Received", "Cancelled"
        public Guid CreatedBy { get; set; } // Id of the user who created the purchase
        public DateTime CreatedAt { get; set; }
        public Guid UpdatedBy { get; set; } // Id of the user who updated the purchase
        public DateTime UpdatedAt { get; set; }
    }
}
