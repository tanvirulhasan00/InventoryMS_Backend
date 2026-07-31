using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace InventoryMS.Models.Entities.ProductModels
{
    public class Size
    {
        [Key]
        public int SizeId { get; init; }
        public string SizeName { get; init; }
        public int DisplayOrder { get; init; }
    }
}
