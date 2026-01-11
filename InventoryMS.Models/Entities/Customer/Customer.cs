using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace InventoryMS.Models.Entities.Customer
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string GranterPhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsActive { get; set; }

        //images
        public string? ImageUrl { get; set; }
        public string? NidImageUrl { get; set; }
        public string? GranterNidImageUrl { get; set; }

        //soft delete
        public bool IsDeleted { get; set; } = false;
        public DateTime DeletedAt { get; set; }

    }
}
