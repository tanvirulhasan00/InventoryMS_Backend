

namespace InventoryMS.Models.Entities.PhoneNumberModel
{
    public class PhoneNumber
    {
        public Guid PhoneNumberId { get; init; }
        public string? Number { get; set; }
        public string OwnerId { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
