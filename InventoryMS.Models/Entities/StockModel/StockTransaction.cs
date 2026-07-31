using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace InventoryMS.Models.Entities.StockModel
{
    public class StockTransaction
    {
        [Key]
        public int TransactionId { get; init; }
        public DateTime TransactionDate { get; set; }
        public int WarehouseId { get; set; }
        public int VariantId { get; set; }
        public int LotId { get; set; }
        public string TransactionType { get; set; }
        public string ReferenceType { get; set; }
        public int ReferenceId { get; set; }
        public decimal QuantityIn { get; set; }
        public decimal QuantityOut { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal BalanceAfter { get; set; }
        public int CreatedBy { get; set; }

    }
}
