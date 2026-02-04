using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnesInventory.Models
{
    public class Supplier
    {
        public int SupplierId { get; set; }
        public string SupplierName { get; set; }
        public string ContactPhone { get; set; }
        public string ContactEmail { get; set; }
        public string Address { get; set; }

        public virtual ICollection<Equipment> Equipment { get; set; }
        public virtual ICollection<InventoryItem> InventoryItems { get; set; }
    }
}