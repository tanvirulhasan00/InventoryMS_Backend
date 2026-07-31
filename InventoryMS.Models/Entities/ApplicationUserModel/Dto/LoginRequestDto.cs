using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryMS.Models.Entities.ApplicationUserModel.Dto
{
    public class LoginRequestDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; } = false;
    }
}
