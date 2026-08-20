using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace InventoryMS.Models.Entities.ProductModels
{
    public class Product
    {
        [Key]
        public Guid ProductId { get; init; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public Guid CategoryId { get; set; } // Foreign key to Category
        public Guid BrandId { get; set; } // Foreign key to Brand
        public Guid UnitId { get; set; } // Foreign key to Unit
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
