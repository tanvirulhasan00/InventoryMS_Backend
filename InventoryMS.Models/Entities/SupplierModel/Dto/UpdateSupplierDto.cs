using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryMS.Models.Entities.SupplierModel.Dto
{
    public class UpdateSupplierDto
    {
        public string SupplierId { get; set; }
        public string? SupplierName { get; set; }
        public string? SupplierEmail { get; set; }
        public string? SupplierAddress { get; set; }
    }
}
