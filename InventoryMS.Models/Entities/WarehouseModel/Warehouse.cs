using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace InventoryMS.Models.Entities.WarehouseModel
{
    public class Warehouse
    {
        [Key]
        public int WarehouseId { get; set; }
        public string WarehouseName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public bool IsActive { get; set; }
    }
}
