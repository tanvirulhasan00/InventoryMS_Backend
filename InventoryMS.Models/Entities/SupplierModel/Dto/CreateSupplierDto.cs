using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace InventoryMS.Models.Entities.SupplierModel.Dto
{
    public class CreateSupplierDto
    {
        [Required]
        public string SupplierId { get; set; }
        public string? SupplierName { get; set; }
        public List<string>? SupplierPhoneNumber { get; set; }
        public string? SupplierEmail { get; set; }
        public string? SupplierAddress { get; set; }
    }
}
