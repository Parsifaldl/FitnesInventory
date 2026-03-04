using FitnesInventory.Data;
using FitnesInventory.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace FitnesInventory
{
    public class DatabaseService
    {
        private readonly FitnesInventoryDbContext _context;

        public DatabaseService()
        {
            _context = new FitnesInventoryDbContext();
        }

        public List<Equipment> GetAllEquipment()
        {
            return _context.Equipment
                .Include(e => e.Category)
                .Include(e => e.Location)
                .Include(e => e.Supplier)
                .ToList();
        }

        public void AddEquipment(Equipment equipment)
        {
            _context.Equipment.Add(equipment);
            _context.SaveChanges();
        }

        public List<EquipmentCategory> GetEquipmentCategories()
        {
            return _context.EquipmentCategories.ToList();
        }

        public void AddEquipmentCategory(EquipmentCategory category)
        {
            _context.EquipmentCategories.Add(category);
            _context.SaveChanges();
        }

        public List<Location> GetLocations()
        {
            return _context.Locations.ToList();
        }

        public void AddLocation(Location location)
        {
            _context.Locations.Add(location);
            _context.SaveChanges();
        }

        public List<Supplier> GetSuppliers()
        {
            return _context.Suppliers.ToList();
        }

        public void AddSupplier(Supplier supplier)
        {
            _context.Suppliers.Add(supplier);
            _context.SaveChanges();
        }

        public List<InventoryItem> GetAllInventoryItems()
        {
            return _context.InventoryItems
                .Include(i => i.Category)
                .Include(i => i.Supplier)
                .ToList();
        }

        public void AddInventoryItem(InventoryItem item)
        {
            _context.InventoryItems.Add(item);
            _context.SaveChanges();
        }

        public List<InventoryCategory> GetInventoryCategories()
        {
            return _context.InventoryCategories.ToList();
        }

        public void AddInventoryCategory(InventoryCategory category)
        {
            _context.InventoryCategories.Add(category);
            _context.SaveChanges();
        }

        public List<Employee> GetEmployees()
        {
            return _context.Employees.ToList();
        }

        public void AddEmployee(Employee employee)
        {
            _context.Employees.Add(employee);
            _context.SaveChanges();
        }

        public List<InventoryTransaction> GetAllTransactions()
        {
            return _context.InventoryTransactions
                .Include(t => t.Item)
                .Include(t => t.Employee)
                .ToList();
        }

        public void AddTransaction(InventoryTransaction transaction)
        {
            _context.InventoryTransactions.Add(transaction);
            _context.SaveChanges();
        }
    }
}