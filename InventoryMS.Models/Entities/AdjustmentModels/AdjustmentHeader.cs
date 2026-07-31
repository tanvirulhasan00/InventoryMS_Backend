using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace InventoryMS.Models.Entities.AdjustmentModels
{
    public class AdjustmentHeader
    {
        [Key]
        public int AdjustmentId { get; set; }
        public int WarehouseId { get; set; }
        public DateTime AdjustmentDate { get; set; }
        public string Reason { get; set; }
        public int CreatedBy { get; set; }

    }
}
