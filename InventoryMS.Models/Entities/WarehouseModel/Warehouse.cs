using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace InventoryMS.Models.Entities.WarehouseModel
{
    public class Warehouse
    {
        [Key]
        public Guid WarehouseId { get; init; }
        public string WarehouseName { get; set; }
        public string WarehouseCode { get; set; }
        public string PhoneNumber { get; set; }
        public string Location { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsActive { get; set; }
    }
}
