using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryMS.Models.Entities.ProductModels.Dto
{
    public class UpdateSizeDto
    {
        public string SizeId { get; set; }
        public string SizeName { get; set; }
        public int DisplayOrder { get; set; }
    }
}
