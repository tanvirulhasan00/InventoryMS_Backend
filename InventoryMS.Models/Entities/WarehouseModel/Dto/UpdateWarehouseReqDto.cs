using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryMS.Models.Entities.WarehouseModel.Dto
{
    public class UpdateWarehouseReqDto
    {
        public string WarehouseId { get; set; }
        public string? WarehouseName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Location { get; set; }
    }
}
