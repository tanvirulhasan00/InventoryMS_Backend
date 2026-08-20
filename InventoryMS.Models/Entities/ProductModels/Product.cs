using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        [ForeignKey(nameof(CategoryId))]
        public Category Category { get; set; }
        public Guid BrandId { get; set; } // Foreign key to Brand
        [ForeignKey(nameof(BrandId))]
        public Brand Brand { get; set; }
        public Guid UnitId { get; set; } // Foreign key to Unit
        [ForeignKey(nameof(UnitId))]
        public Unit Unit { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
