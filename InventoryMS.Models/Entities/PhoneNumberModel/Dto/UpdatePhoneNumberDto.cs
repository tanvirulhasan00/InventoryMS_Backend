using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryMS.Models.Entities.PhoneNumberModel.Dto
{
    public class UpdatePhoneNumberDto
    {
        public string PhoneNumberId { get; set; }
        public string? Number { get; set; }
        public string? OwnerId { get; set; }
    }
}
