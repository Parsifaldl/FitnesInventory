USE Fitnes_Inventory
GO
CREATE TABLE Users (
    UserId INT PRIMARY KEY IDENTITY(1,1),
    Username NVARCHAR(50) UNIQUE NOT NULL,
    PasswordHash NVARCHAR(255) NOT NULL,
    FullName NVARCHAR(100) NOT NULL,
    Role NVARCHAR(20) NOT NULL DEFAULT 'User',
    CanViewEquipment BIT NOT NULL DEFAULT 1,
    CanAddEquipment BIT NOT NULL DEFAULT 0,
    CanViewInventory BIT NOT NULL DEFAULT 1,
    CanAddInventory BIT NOT NULL DEFAULT 0,
    CanViewTransactions BIT NOT NULL DEFAULT 1,
    CanAddTransactions BIT NOT NULL DEFAULT 0,
    CanViewEmployees BIT NOT NULL DEFAULT 1,
    CanAddEmployees BIT NOT NULL DEFAULT 0,
    CanViewSuppliers BIT NOT NULL DEFAULT 1,
    CanAddSuppliers BIT NOT NULL DEFAULT 0,
    CanViewLocations BIT NOT NULL DEFAULT 1,
    CanAddLocations BIT NOT NULL DEFAULT 0,
    CanViewCategories BIT NOT NULL DEFAULT 1,
    CanAddCategories BIT NOT NULL DEFAULT 0,
    CreatedAt DATETIME NOT NULL DEFAULT GETDATE(),
    LastLogin DATETIME NULL
);

-- Добавляем администратора по умолчанию (пароль: admin123)
INSERT INTO Users (Username, PasswordHash, FullName, Role, 
    CanViewEquipment, CanAddEquipment, CanViewInventory, CanAddInventory,
    CanViewTransactions, CanAddTransactions, CanViewEmployees, CanAddEmployees,
    CanViewSuppliers, CanAddSuppliers, CanViewLocations, CanAddLocations,
    CanViewCategories, CanAddCategories)
VALUES ('admin', 'admin123', 'Системный администратор', 'Admin',
    1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1);
GO