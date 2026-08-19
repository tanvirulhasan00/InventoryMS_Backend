using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace InventoryMS.Models.Entities.CustomerModel
{
    public class Customer
    {
        [Key]
        public Guid CustomerId { get; init; }
        public string CustomerName { get; set; }
        public string? CompanyName { get; set; }
        public string PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsActive { get; set; }

        //soft delete
        public bool IsDeleted { get; set; } = false;
        public DateTime DeletedAt { get; set; }

    }
}
