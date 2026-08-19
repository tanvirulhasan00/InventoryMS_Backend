using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryMS.Models.Entities.ProductModels.Dto
{
    public class UpdateCategoryDto
    {
        public string CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
    }
}
