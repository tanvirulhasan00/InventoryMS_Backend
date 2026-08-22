using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace InventoryMS.Models.Entities.PhoneNumberModel.Dto
{
    public class CreatePhoneNumberDto
    {
        public string? Number { get; set; }
        [Required]
        public string OwnerId { get; set; }
    }
}
