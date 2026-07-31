using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace InventoryMS.Models.Entities.SalesModel
{
    public class SalesHeader
    {
        [Key]
        public int SalesId { get; init; }
        public string InvoiceNo { get; set; }
        public int CustomerId { get; set; }
        public DateTime SalesDate { get; set; }
        public int WarehouseId { get; set; }
        public string? Remarks { get; set; }
        public int CreatedBy { get; set; }
    }
}
