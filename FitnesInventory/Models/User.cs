using System;

namespace FitnesInventory.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string FullName { get; set; }
        public string Role { get; set; } 
        public bool CanViewEquipment { get; set; }
        public bool CanAddEquipment { get; set; }
        public bool CanViewInventory { get; set; }
        public bool CanAddInventory { get; set; }
        public bool CanViewTransactions { get; set; }
        public bool CanAddTransactions { get; set; }
        public bool CanViewEmployees { get; set; }
        public bool CanAddEmployees { get; set; }
        public bool CanViewSuppliers { get; set; }
        public bool CanAddSuppliers { get; set; }
        public bool CanViewLocations { get; set; }
        public bool CanAddLocations { get; set; }
        public bool CanViewCategories { get; set; }
        public bool CanAddCategories { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? LastLogin { get; set; }
    }
}
