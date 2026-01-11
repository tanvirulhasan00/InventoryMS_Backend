using InventoryMS.Models.Entities.CategoryModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace InventoryMS.Models.Entities.ItemModel
{
    public class Item
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string? ModelNumber { get; set; }
        public string? BrandName { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }

        public string? SourceName { get; set; }
        public string? SourcePhoneNumber { get; set; }

        public DateOnly PurchaseDate { get; set; }
        public DateOnly? WarrantyEnd { get; set; }

        public int CategoryId { get; set; }
        [ForeignKey(nameof(CategoryId))]
        public Category Category { get; set; }

        public int StockQuantity { get; set; }
        public bool IsActive { get; set; }
    }
}
