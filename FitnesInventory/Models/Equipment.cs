using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnesInventory.Models
{
    public class Equipment
    {
        public int EquipmentId { get; set; }
        public string SerialNumber { get; set; }
        public string EquipmentName { get; set; }
        public int CategoryId { get; set; }
        public int? LocationId { get; set; }
        public int? SupplierId { get; set; }
        public DateTime? PurchaseDate { get; set; }
        public decimal? PurchasePrice { get; set; }
        public string Status { get; set; }

        public virtual EquipmentCategory Category { get; set; }
        public virtual Location Location { get; set; }
        public virtual Supplier Supplier { get; set; }
    }
}