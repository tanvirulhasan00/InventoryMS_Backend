using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace InventoryMS.Models.Entities.AdjustmentModels
{
    public class AdjustmentDetail
    {
        [Key]
        public int AdjustmentDetailId { get; set; }
        public int AdjustmentId { get; set; }
        public int VariantId { get; set; }
        public int LotId { get; set; }
        public decimal Quantity { get; set; }
        public string AdjustmentType { get; set; }
        public string Remarks { get; set; }
    }
}
