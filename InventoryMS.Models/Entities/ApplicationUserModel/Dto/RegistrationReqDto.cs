using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryMS.Models.Entities.ApplicationUserModel.Dto
{
    public class RegistrationReqDto
    {
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Role { get; set; }
    }
}
