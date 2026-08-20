using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryMS.Models.Entities.ProductModels.Dto
{
    public class UpdateUnitDto
    {
        public string UnitId { get; set; }
        public string UnitName { get; set; }
        public string UnitShortName { get; set; }
    }
}
