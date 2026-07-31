using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace InventoryMS.Models.Entities.ProductModels
{
    public class Brand
    {
        [Key]
        public int BrandId { get; init; }
        public string BrandName { get; init; }
        public string? LicenseNo { get; init; }
        public bool IsActive { get; init; }
    }
}
