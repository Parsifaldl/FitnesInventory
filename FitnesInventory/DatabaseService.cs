using System;
using System.Collections.Generic;
using System.Linq;
using FitnesInventory.Models;

namespace FitnesInventory.Data
{
    public class DatabaseService
    {
        private FitnesInventoryDbContext _context;

        public DatabaseService()
        {
            _context = new FitnesInventoryDbContext();
        }

        public List<Equipment> GetAllEquipment()
        {
            return _context.Equipment.ToList();
        }

        public void AddEquipment(Equipment equipment)
        {
            _context.Equipment.Add(equipment);
            _context.SaveChanges();
        }

        public List<InventoryItem> GetAllInventoryItems()
        {
            return _context.Inventory_Item.ToList();
        }

        public void AddInventoryItem(InventoryItem item)
        {
            _context.Inventory_Item.Add(item);
            _context.SaveChanges();
        }

        public List<InventoryTransaction> GetAllTransactions()
        {
            return _context.Inventory_Transaction.ToList();
        }

        public void AddTransaction(InventoryTransaction transaction)
        {
            _context.Inventory_Transaction.Add(transaction);
            _context.SaveChanges();
        }

        public List<EquipmentCategory> GetEquipmentCategories()
        {
            return _context.Equipment_Category.ToList();
        }

        public void AddEquipmentCategory(EquipmentCategory category)
        {
            _context.Equipment_Category.Add(category);
            _context.SaveChanges();
        }

        public List<Location> GetLocations()
        {
            return _context.Location.ToList();
        }

        public void AddLocation(Location location)
        {
            _context.Location.Add(location);
            _context.SaveChanges();
        }

        public List<Supplier> GetSuppliers()
        {
            return _context.Supplier.ToList();
        }

        public void AddSupplier(Supplier supplier)
        {
            _context.Supplier.Add(supplier);
            _context.SaveChanges();
        }

        public List<InventoryCategory> GetInventoryCategories()
        {
            return _context.Inventory_Category.ToList();
        }

        public void AddInventoryCategory(InventoryCategory category)
        {
            _context.Inventory_Category.Add(category);
            _context.SaveChanges();
        }

        public List<Employee> GetEmployees()
        {
            return _context.Employee.ToList();
        }

        public void AddEmployee(Employee employee)
        {
            _context.Employee.Add(employee);
            _context.SaveChanges();
        }

        public User GetUserByUsername(string username)
        {
            return _context.Users.FirstOrDefault(u => u.Username == username);
        }

        public void UpdateLastLogin(int userId)
        {
            var user = _context.Users.Find(userId);
            if (user != null)
            {
                user.LastLogin = DateTime.Now;
                _context.SaveChanges();
            }
        }

        public List<User> GetAllUsers()
        {
            return _context.Users.ToList();
        }

        public void AddUser(User user)
        {
            user.CreatedAt = DateTime.Now;
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public void UpdateUser(User user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();
        }

        public void DeleteUser(int userId)
        {
            var user = _context.Users.Find(userId);
            if (user != null && user.Role != "Admin")
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
            }
        }

        public bool ChangePassword(int userId, string newPassword)
        {
            var user = _context.Users.Find(userId);
            if (user != null)
            {
                user.PasswordHash = newPassword;
                _context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}