using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace InventoryMS.Models.Entities.StockModel
{
    public class StockBalance
    {
        
        public int VariantId { get; init; }
        public int WarehouseId { get; init; }
        public int LotId { get; init; }
        public decimal AvailableQty { get; init; }
        public decimal ReservedQty { get; init; }
        public int LastUpdatedAt { get; init; }
    }
}
