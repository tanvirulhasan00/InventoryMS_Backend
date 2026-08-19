using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryMS.Models.Entities.CustomerModel.Dto
{
    public class CreateCustomerReqDto
    {
        public string CustomerName { get; set; }
        public string? CompanyName { get; set; }
        public string PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
    }
}
