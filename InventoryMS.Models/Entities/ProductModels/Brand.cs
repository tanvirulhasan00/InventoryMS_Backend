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
        public string BrandName { get; set; }
        public string BrandDescription { get; set; }
        public string? LicenseNo { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsActive { get; set; }
    }
}
