using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryMS.Models.Request
{
    public class SoftDeleteReq
    {
        public string CustomerId { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
