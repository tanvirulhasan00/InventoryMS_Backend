using InventoryMS.Models.Entities.PurchaseModels;
using InventoryMS.Models.Entities.SupplierModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace InventoryMS.Models.Entities.LotModel.Dto
{
    public class UpdateLotDto
    {
        public string LotId { get; set; }
        public int LotNumber { get; set; }
        public string PurchaseId { get; set; } // Foreign key to the Purchase entity
        public DateTime ReceivedDate { get; set; }
        public string SupplierId { get; set; } // Foreign key to the Supplier entity
        public string? Remarks { get; set; }
    }
}
