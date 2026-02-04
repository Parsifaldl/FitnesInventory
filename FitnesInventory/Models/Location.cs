using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnesInventory.Models
{
    public class Location
    {
        public int LocationId { get; set; }
        public string LocationName { get; set; }

        public virtual ICollection<Equipment> Equipment { get; set; }
    }
}