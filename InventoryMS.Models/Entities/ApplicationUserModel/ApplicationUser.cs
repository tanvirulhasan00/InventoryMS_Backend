using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryMS.Models.Entities.ApplicationUserModel
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
        public string Password { get; set; }
    }
}
