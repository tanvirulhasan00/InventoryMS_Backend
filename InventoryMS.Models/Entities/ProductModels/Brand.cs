using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace InventoryMS.Models.Entities.ProductModels
{
    public class Brand
    {
        [Key]
        public Guid BrandId { get; init; }
        public string BrandName { get; init; }
        public string BrandDescription { get; init; }
        public string? LicenseNo { get; init; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsActive { get; init; }
    }
}
