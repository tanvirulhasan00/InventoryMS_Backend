using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace InventoryMS.Models.Entities.TransferModels
{
    public class TransferHeader
    {
        [Key]
        public int TransferId { get; set; }
        public string FromWarehouse { get; set; }
        public string ToWarehouse { get; set; }
        public DateTime TransferDate { get; set; }
        public int CreatedBy { get; set; }

    }
}
