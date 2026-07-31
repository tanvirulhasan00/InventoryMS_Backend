using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace InventoryMS.Models.Entities.ProductModels
{
    public class Unit
    {
        [Key]
        public int UnitId { get; set; }
        public string UnitName { get; set; }
        public string UnitShortName { get; set; }
    }
}
