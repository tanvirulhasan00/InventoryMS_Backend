

namespace InventoryMS.Models.Entities.LotModel.Dto
{
    public class CreateLotDto
    {
        public int LotNumber { get; set; }
        public string PurchaseId { get; set; } // Foreign key to the Purchase entity
        public DateTime ReceivedDate { get; set; }
        public string SupplierId { get; set; } // Foreign key to the Supplier entity
        public string? Remarks { get; set; }
    }
}
