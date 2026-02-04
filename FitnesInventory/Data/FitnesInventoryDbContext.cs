using Microsoft.EntityFrameworkCore;
using FitnesInventory.Models;

namespace FitnesInventory.Data
{
    public class FitnesInventoryDbContext : DbContext
    {
        public DbSet<EquipmentCategory> EquipmentCategories { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Equipment> Equipment { get; set; }
        public DbSet<InventoryCategory> InventoryCategories { get; set; }
        public DbSet<InventoryItem> InventoryItems { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<InventoryTransaction> InventoryTransactions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                "Server=DESKTOP-3VM4EPN\\SQLEXPRESS;Database=Fitnes_Inventory;Trusted_Connection=True;TrustServerCertificate=True");
        }
    }
}