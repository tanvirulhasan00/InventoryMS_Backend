using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace InventoryMS.Models.Entities.ProductModels
{
    public class Color
    {
        [Key]
        public int ColorId { get; init; }
        public string ColorName { get; init; }
        public string ColorCode { get; init; }
    }
}
