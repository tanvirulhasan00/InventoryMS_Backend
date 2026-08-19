using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryMS.Models.Entities.CustomerModel.Dto
{
    public class UpdateCustomerPhoneNumberReqDto
    {
        public string CustomerId { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
