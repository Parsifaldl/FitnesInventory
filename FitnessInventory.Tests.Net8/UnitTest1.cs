using Xunit;
using FitnesInventory.Models;
using System.Collections.Generic;

namespace FitnessInventory.Tests.Net8
{
    public class UnitTest1
    {
        [Fact]
        public void UserTest_AdminUser_ReturnsCorrectValues()
        {
            var admin = new User
            {
                UserId = 1,
                Username = "admin",
                FullName = "Системный администратор",
                Role = "Admin"
            };

            Assert.Equal(1, admin.UserId);
            Assert.Equal("admin", admin.Username);
            Assert.Equal("Системный администратор", admin.FullName);
            Assert.Equal("Admin", admin.Role);
        }

        [Fact]
        public void EmployeeTest_ReturnsCorrectValues()
        {
            var employee = new Employee
            {
                EmployeeId = 1,
                FirstName = "Иван",
                LastName = "Иванов",
                Position = "Сотрудник Отдела качества"
            };

            Assert.Equal(1, employee.EmployeeId);
            Assert.Equal("Иван", employee.FirstName);
            Assert.Equal("Иванов", employee.LastName);
            Assert.Equal("Сотрудник Отдела качества", employee.Position);
        }

        [Fact]
        public void SupplierTest_ReturnsCorrectValues()
        {
            var supplier = new Supplier
            {
                SupplierId = 1,
                SupplierName = "ООО Тмыв денег",
                ContactPhone = "+7987654321",
                ContactEmail = "work@gmail.com",
                Address = "ул.Пушкина, д14"
            };

            Assert.Equal(1, supplier.SupplierId);
            Assert.Equal("ООО Тмыв денег", supplier.SupplierName);
            Assert.Equal("+7987654321", supplier.ContactPhone);
            Assert.Equal("work@gmail.com", supplier.ContactEmail);
            Assert.Equal("ул.Пушкина, д14", supplier.Address);
        }

        [Fact]
        public void EquipmentTest_ReturnsCorrectValues()
        {
            var equipment = new Equipment
            {
                EquipmentId = 4,
                SerialNumber = "SN001",
                EquipmentName = "Беговая дорожка",
                PurchasePrice = 15000.00m,
                Status = "В наличии"
            };

            Assert.Equal(4, equipment.EquipmentId);
            Assert.Equal("SN001", equipment.SerialNumber);
            Assert.Equal("Беговая дорожка", equipment.EquipmentName);
            Assert.Equal(15000.00m, equipment.PurchasePrice);
            Assert.Equal("В наличии", equipment.Status);
        }

        [Fact]
        public void LoginTest_AdminCredentials_ReturnsTrue()
        {
            string username = "admin";
            string password = "admin123";
            bool result = (username == "admin" && password == "admin123");
            Assert.True(result);
        }

        [Fact]
        public void LoginTest_InvalidCredentials_ReturnsFalse()
        {
            string username = "wrong";
            string password = "wrong";
            bool result = (username == "admin" && password == "admin123");
            Assert.False(result);
        }

        [Fact]
        public void UserListTest_AddUser_UserAddedToList()
        {
            var users = new List<User>
            {
                new User { Username = "admin" },
                new User { Username = "loh" }
            };
            users.Add(new User { Username = "newuser" });
            Assert.Equal(3, users.Count);
        }

        [Fact]
        public void UserListTest_DeleteUser_UserRemovedFromList()
        {
            var user = new User { Username = "loh" };
            var users = new List<User>
            {
                new User { Username = "admin" },
                user,
                new User { Username = "newuser" }
            };
            users.Remove(user);
            Assert.Equal(2, users.Count);
        }
    }
}