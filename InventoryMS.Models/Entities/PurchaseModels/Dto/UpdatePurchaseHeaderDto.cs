using InventoryMS.Models.Entities.SupplierModel;
using InventoryMS.Models.Entities.WarehouseModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace InventoryMS.Models.Entities.PurchaseModels.Dto
{
    public class UpdatePurchaseHeaderDto
    {
        public string PurchaseId { get; init; }
        public int PurchaseNo { get; set; }
        public string SupplierId { get; set; } // Foreign key to the Supplier entity
        public string InvoiceNo { get; set; }
        public string WarehouseId { get; set; } // Foreign key to the Warehouse entity
        public string? Remarks { get; set; }
        public string UpdatedBy { get; set; } // Id of the user who updated the purchase
        public DateTime UpdatedAt { get; set; }
    }
}
