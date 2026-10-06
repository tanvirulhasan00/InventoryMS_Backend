using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryMS.Models.Entities.PurchaseModels.Dto
{
    public class UpdatePurchaseHeaderStatusDto
    {
        public string PurchaseId { get; init; }
        public string? Status { get; set; } // e.g., "Draft", "Confirmed", "Received", "Cancelled"
    }
}
