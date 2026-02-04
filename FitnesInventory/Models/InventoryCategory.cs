using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnesInventory.Models
{
    public class InventoryCategory
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string UnitOfMeasure { get; set; }

        public virtual ICollection<InventoryItem> InventoryItems { get; set; }
    }
}