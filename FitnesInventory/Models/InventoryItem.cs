using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnesInventory.Models
{
    public class InventoryItem
    {
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public int CategoryId { get; set; }
        public int? SupplierId { get; set; }
        public int MinStockLevel { get; set; }
        public int? MaxStockLevel { get; set; }
        public int CurrentQuantity { get; set; }

        public virtual InventoryCategory Category { get; set; }
        public virtual Supplier Supplier { get; set; }
        public virtual ICollection<InventoryTransaction> InventoryTransactions { get; set; }
    }
}