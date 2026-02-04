using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnesInventory.Models
{
    public class InventoryTransaction
    {
        public int TransactionId { get; set; }
        public int ItemId { get; set; }
        public string TransactionType { get; set; }
        public DateTime TransactionDate { get; set; }
        public int? EmployeeId { get; set; }

        public virtual InventoryItem Item { get; set; }
        public virtual Employee Employee { get; set; }
    }
}