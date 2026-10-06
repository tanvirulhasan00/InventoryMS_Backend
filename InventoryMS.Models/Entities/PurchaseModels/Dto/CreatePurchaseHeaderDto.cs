

namespace InventoryMS.Models.Entities.PurchaseModels.Dto
{
    public class CreatePurchaseHeaderDto
    {
        public int PurchaseNo { get; set; }
        public string SupplierId { get; set; } // Foreign key to the Supplier entity
        public string InvoiceNo { get; set; }
        public DateTime PurchaseDate { get; set; }
        public string WarehouseId { get; set; } // Foreign key to the Warehouse entity
        public string? Remarks { get; set; }
        public string? Status { get; set; } // e.g., "Draft", "Confirmed", "Received", "Cancelled"
        public string CreatedBy { get; set; } // Id of the user who created the purchase
        
      
    }
}
