using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryMS.Models.Entities.ProductModels.Dto
{
    public class CreateSizeDto
    {
        public string SizeName { get; set; }
        public int DisplayOrder { get; set; }
    }
}
