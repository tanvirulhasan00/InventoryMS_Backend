using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace InventoryMS.Models.Entities.TransferModels
{
    public class TransferDetail
    {
        [Key]
        public int TransferDetailId { get; set; }
        public int TransferId { get; set; }
        public int VariantId { get; set; }
        public int LotId { get; set; }
        public decimal Quantity { get; set; }
    }
}
