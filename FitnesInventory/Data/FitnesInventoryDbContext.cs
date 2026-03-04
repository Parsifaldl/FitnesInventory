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
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(
                    "Server=DESKTOP-3VM4EPN\\SQLEXPRESS;Database=Fitnes_Inventory;Trusted_Connection=True;TrustServerCertificate=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EquipmentCategory>().ToTable("Equipment_Category");
            modelBuilder.Entity<Location>().ToTable("Location");
            modelBuilder.Entity<Supplier>().ToTable("Supplier");
            modelBuilder.Entity<Equipment>().ToTable("Equipment");
            modelBuilder.Entity<InventoryCategory>().ToTable("Inventory_Category");
            modelBuilder.Entity<InventoryItem>().ToTable("Inventory_Item");
            modelBuilder.Entity<Employee>().ToTable("Employee");
            modelBuilder.Entity<InventoryTransaction>().ToTable("Inventory_Transaction");

            modelBuilder.Entity<EquipmentCategory>().HasKey(e => e.CategoryId);
            modelBuilder.Entity<Location>().HasKey(l => l.LocationId);
            modelBuilder.Entity<Supplier>().HasKey(s => s.SupplierId);
            modelBuilder.Entity<Equipment>().HasKey(e => e.EquipmentId);
            modelBuilder.Entity<InventoryCategory>().HasKey(i => i.CategoryId);
            modelBuilder.Entity<InventoryItem>().HasKey(i => i.ItemId);
            modelBuilder.Entity<Employee>().HasKey(e => e.EmployeeId);
            modelBuilder.Entity<InventoryTransaction>().HasKey(t => t.TransactionId);

            modelBuilder.Entity<EquipmentCategory>()
                .Property(e => e.CategoryId)
                .HasColumnName("category_id");

            modelBuilder.Entity<EquipmentCategory>()
                .Property(e => e.CategoryName)
                .HasColumnName("category_name");

            modelBuilder.Entity<EquipmentCategory>()
                .Property(e => e.Description)
                .HasColumnName("description");

            modelBuilder.Entity<Location>()
                .Property(l => l.LocationId)
                .HasColumnName("location_id");

            modelBuilder.Entity<Location>()
                .Property(l => l.LocationName)
                .HasColumnName("location_name");

            modelBuilder.Entity<Supplier>()
                .Property(s => s.SupplierId)
                .HasColumnName("supplier_id");

            modelBuilder.Entity<Supplier>()
                .Property(s => s.SupplierName)
                .HasColumnName("supplier_name");

            modelBuilder.Entity<Supplier>()
                .Property(s => s.ContactPhone)
                .HasColumnName("contact_phone");

            modelBuilder.Entity<Supplier>()
                .Property(s => s.ContactEmail)
                .HasColumnName("contact_email");

            modelBuilder.Entity<Supplier>()
                .Property(s => s.Address)
                .HasColumnName("address");

            modelBuilder.Entity<Equipment>()
                .Property(e => e.EquipmentId)
                .HasColumnName("equipment_id");

            modelBuilder.Entity<Equipment>()
                .Property(e => e.SerialNumber)
                .HasColumnName("serial_number");

            modelBuilder.Entity<Equipment>()
                .Property(e => e.EquipmentName)
                .HasColumnName("equipment_name");

            modelBuilder.Entity<Equipment>()
                .Property(e => e.CategoryId)
                .HasColumnName("category_id");

            modelBuilder.Entity<Equipment>()
                .Property(e => e.LocationId)
                .HasColumnName("location_id");

            modelBuilder.Entity<Equipment>()
                .Property(e => e.SupplierId)
                .HasColumnName("supplier_id");

            modelBuilder.Entity<Equipment>()
                .Property(e => e.PurchaseDate)
                .HasColumnName("purchase_date");

            modelBuilder.Entity<Equipment>()
                .Property(e => e.PurchasePrice)
                .HasColumnName("purchase_price");

            modelBuilder.Entity<Equipment>()
                .Property(e => e.Status)
                .HasColumnName("status");

            modelBuilder.Entity<InventoryCategory>()
                .Property(i => i.CategoryId)
                .HasColumnName("category_id");

            modelBuilder.Entity<InventoryCategory>()
                .Property(i => i.CategoryName)
                .HasColumnName("category_name");

            modelBuilder.Entity<InventoryCategory>()
                .Property(i => i.UnitOfMeasure)
                .HasColumnName("unit_of_measure");

            modelBuilder.Entity<InventoryItem>()
                .Property(i => i.ItemId)
                .HasColumnName("item_id");

            modelBuilder.Entity<InventoryItem>()
                .Property(i => i.ItemName)
                .HasColumnName("item_name");

            modelBuilder.Entity<InventoryItem>()
                .Property(i => i.CategoryId)
                .HasColumnName("category_id");

            modelBuilder.Entity<InventoryItem>()
                .Property(i => i.SupplierId)
                .HasColumnName("supplier_id");

            modelBuilder.Entity<InventoryItem>()
                .Property(i => i.MinStockLevel)
                .HasColumnName("min_stock_level");

            modelBuilder.Entity<InventoryItem>()
                .Property(i => i.MaxStockLevel)
                .HasColumnName("max_stock_level");

            modelBuilder.Entity<InventoryItem>()
                .Property(i => i.CurrentQuantity)
                .HasColumnName("current_quantity");

            modelBuilder.Entity<Employee>()
                .Property(e => e.EmployeeId)
                .HasColumnName("employee_id");

            modelBuilder.Entity<Employee>()
                .Property(e => e.FirstName)
                .HasColumnName("first_name");

            modelBuilder.Entity<Employee>()
                .Property(e => e.LastName)
                .HasColumnName("last_name");

            modelBuilder.Entity<Employee>()
                .Property(e => e.Position)
                .HasColumnName("position");

            modelBuilder.Entity<InventoryTransaction>()
                .Property(t => t.TransactionId)
                .HasColumnName("transaction_id");

            modelBuilder.Entity<InventoryTransaction>()
                .Property(t => t.ItemId)
                .HasColumnName("item_id");

            modelBuilder.Entity<InventoryTransaction>()
                .Property(t => t.TransactionType)
                .HasColumnName("transaction_type");

            modelBuilder.Entity<InventoryTransaction>()
                .Property(t => t.TransactionDate)
                .HasColumnName("transaction_date");

            modelBuilder.Entity<InventoryTransaction>()
                .Property(t => t.EmployeeId)
                .HasColumnName("employee_id");

            modelBuilder.Entity<EquipmentCategory>()
                .HasIndex(e => e.CategoryName)
                .IsUnique();

            modelBuilder.Entity<Location>()
                .HasIndex(l => l.LocationName)
                .IsUnique();

            modelBuilder.Entity<Supplier>()
                .HasIndex(s => s.SupplierName)
                .IsUnique(false);

            modelBuilder.Entity<Equipment>()
                .HasIndex(e => e.SerialNumber)
                .IsUnique();

            modelBuilder.Entity<InventoryCategory>()
                .HasIndex(ic => ic.CategoryName)
                .IsUnique();

            modelBuilder.Entity<InventoryItem>()
                .Property(i => i.MinStockLevel)
                .HasDefaultValue(0);

            modelBuilder.Entity<InventoryItem>()
                .Property(i => i.CurrentQuantity)
                .HasDefaultValue(0);

            modelBuilder.Entity<InventoryTransaction>()
                .HasOne(t => t.Item)
                .WithMany(i => i.InventoryTransactions)
                .HasForeignKey(t => t.ItemId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<InventoryTransaction>()
                .HasOne(t => t.Employee)
                .WithMany(e => e.InventoryTransactions)
                .HasForeignKey(t => t.EmployeeId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Equipment>()
                .HasOne(e => e.Category)
                .WithMany(c => c.Equipment)
                .HasForeignKey(e => e.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Equipment>()
                .HasOne(e => e.Location)
                .WithMany(l => l.Equipment)
                .HasForeignKey(e => e.LocationId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Equipment>()
                .HasOne(e => e.Supplier)
                .WithMany(s => s.Equipment)
                .HasForeignKey(e => e.SupplierId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<InventoryItem>()
                .HasOne(i => i.Category)
                .WithMany(c => c.InventoryItems)
                .HasForeignKey(i => i.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<InventoryItem>()
                .HasOne(i => i.Supplier)
                .WithMany(s => s.InventoryItems)
                .HasForeignKey(i => i.SupplierId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Equipment>()
                .Property(e => e.PurchasePrice)
                .HasPrecision(18, 2);

            base.OnModelCreating(modelBuilder);
        }
    }
}