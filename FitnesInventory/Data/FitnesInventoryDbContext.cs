using Microsoft.EntityFrameworkCore;
using FitnesInventory.Models;

namespace FitnesInventory.Data
{
    public class FitnesInventoryDbContext : DbContext
    {
        public FitnesInventoryDbContext()
        {
        }

        public FitnesInventoryDbContext(DbContextOptions<FitnesInventoryDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<EquipmentCategory> Equipment_Category { get; set; }
        public DbSet<Location> Location { get; set; }
        public DbSet<Supplier> Supplier { get; set; }
        public DbSet<Equipment> Equipment { get; set; }
        public DbSet<InventoryCategory> Inventory_Category { get; set; }
        public DbSet<InventoryItem> Inventory_Item { get; set; }
        public DbSet<Employee> Employee { get; set; }
        public DbSet<InventoryTransaction> Inventory_Transaction { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=Fitnes_Inventory;Trusted_Connection=True;TrustServerCertificate=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Users - БЕЗ HasColumnName (имена столбцов совпадают со свойствами)
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<User>().HasKey(u => u.UserId);
            modelBuilder.Entity<User>().Property(u => u.UserId).ValueGeneratedOnAdd();
            modelBuilder.Entity<User>().HasIndex(u => u.Username).IsUnique();

            // Equipment_Category
            modelBuilder.Entity<EquipmentCategory>().ToTable("Equipment_Category");
            modelBuilder.Entity<EquipmentCategory>().HasKey(e => e.CategoryId);
            modelBuilder.Entity<EquipmentCategory>().Property(e => e.CategoryId).HasColumnName("category_id");
            modelBuilder.Entity<EquipmentCategory>().Property(e => e.CategoryName).HasColumnName("category_name");
            modelBuilder.Entity<EquipmentCategory>().Property(e => e.Description).HasColumnName("description");

            // Location
            modelBuilder.Entity<Location>().ToTable("Location");
            modelBuilder.Entity<Location>().HasKey(l => l.LocationId);
            modelBuilder.Entity<Location>().Property(l => l.LocationId).HasColumnName("location_id");
            modelBuilder.Entity<Location>().Property(l => l.LocationName).HasColumnName("location_name");

            // Supplier
            modelBuilder.Entity<Supplier>().ToTable("Supplier");
            modelBuilder.Entity<Supplier>().HasKey(s => s.SupplierId);
            modelBuilder.Entity<Supplier>().Property(s => s.SupplierId).HasColumnName("supplier_id");
            modelBuilder.Entity<Supplier>().Property(s => s.SupplierName).HasColumnName("supplier_name");
            modelBuilder.Entity<Supplier>().Property(s => s.ContactPhone).HasColumnName("contact_phone");
            modelBuilder.Entity<Supplier>().Property(s => s.ContactEmail).HasColumnName("contact_email");
            modelBuilder.Entity<Supplier>().Property(s => s.Address).HasColumnName("address");

            // Equipment
            modelBuilder.Entity<Equipment>().ToTable("Equipment");
            modelBuilder.Entity<Equipment>().HasKey(e => e.EquipmentId);
            modelBuilder.Entity<Equipment>().Property(e => e.EquipmentId).HasColumnName("equipment_id");
            modelBuilder.Entity<Equipment>().Property(e => e.SerialNumber).HasColumnName("serial_number");
            modelBuilder.Entity<Equipment>().Property(e => e.EquipmentName).HasColumnName("equipment_name");
            modelBuilder.Entity<Equipment>().Property(e => e.CategoryId).HasColumnName("category_id");
            modelBuilder.Entity<Equipment>().Property(e => e.LocationId).HasColumnName("location_id");
            modelBuilder.Entity<Equipment>().Property(e => e.SupplierId).HasColumnName("supplier_id");
            modelBuilder.Entity<Equipment>().Property(e => e.PurchaseDate).HasColumnName("purchase_date");
            modelBuilder.Entity<Equipment>().Property(e => e.PurchasePrice).HasColumnName("purchase_price");
            modelBuilder.Entity<Equipment>().Property(e => e.Status).HasColumnName("status");

            // Inventory_Category
            modelBuilder.Entity<InventoryCategory>().ToTable("Inventory_Category");
            modelBuilder.Entity<InventoryCategory>().HasKey(i => i.CategoryId);
            modelBuilder.Entity<InventoryCategory>().Property(i => i.CategoryId).HasColumnName("category_id");
            modelBuilder.Entity<InventoryCategory>().Property(i => i.CategoryName).HasColumnName("category_name");
            modelBuilder.Entity<InventoryCategory>().Property(i => i.UnitOfMeasure).HasColumnName("unit_of_measure");

            // Inventory_Item
            modelBuilder.Entity<InventoryItem>().ToTable("Inventory_Item");
            modelBuilder.Entity<InventoryItem>().HasKey(i => i.ItemId);
            modelBuilder.Entity<InventoryItem>().Property(i => i.ItemId).HasColumnName("item_id");
            modelBuilder.Entity<InventoryItem>().Property(i => i.ItemName).HasColumnName("item_name");
            modelBuilder.Entity<InventoryItem>().Property(i => i.CategoryId).HasColumnName("category_id");
            modelBuilder.Entity<InventoryItem>().Property(i => i.SupplierId).HasColumnName("supplier_id");
            modelBuilder.Entity<InventoryItem>().Property(i => i.CurrentQuantity).HasColumnName("current_quantity");
            modelBuilder.Entity<InventoryItem>().Property(i => i.MinStockLevel).HasColumnName("min_stock_level");
            modelBuilder.Entity<InventoryItem>().Property(i => i.MaxStockLevel).HasColumnName("max_stock_level");

            // Employee
            modelBuilder.Entity<Employee>().ToTable("Employee");
            modelBuilder.Entity<Employee>().HasKey(e => e.EmployeeId);
            modelBuilder.Entity<Employee>().Property(e => e.EmployeeId).HasColumnName("employee_id");
            modelBuilder.Entity<Employee>().Property(e => e.FirstName).HasColumnName("first_name");
            modelBuilder.Entity<Employee>().Property(e => e.LastName).HasColumnName("last_name");
            modelBuilder.Entity<Employee>().Property(e => e.Position).HasColumnName("position");

            // Inventory_Transaction
            modelBuilder.Entity<InventoryTransaction>().ToTable("Inventory_Transaction");
            modelBuilder.Entity<InventoryTransaction>().HasKey(t => t.TransactionId);
            modelBuilder.Entity<InventoryTransaction>().Property(t => t.TransactionId).HasColumnName("transaction_id");
            modelBuilder.Entity<InventoryTransaction>().Property(t => t.ItemId).HasColumnName("item_id");
            modelBuilder.Entity<InventoryTransaction>().Property(t => t.TransactionType).HasColumnName("transaction_type");
            modelBuilder.Entity<InventoryTransaction>().Property(t => t.TransactionDate).HasColumnName("transaction_date");
            modelBuilder.Entity<InventoryTransaction>().Property(t => t.EmployeeId).HasColumnName("employee_id");

            base.OnModelCreating(modelBuilder);
        }
    }
}