using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace InventoryMS.Models.Entities.LotModel
{
    public class Lot
    {
        [Key]
        public int LotId { get; init; }
        public int LotNumber { get; set; }
        public int PurchaseId { get; set; }
        public DateTime ReceivedDate { get; set; }
        public int SupplierId { get; set; }
        public string? Remarks { get; set; }
    }
}
