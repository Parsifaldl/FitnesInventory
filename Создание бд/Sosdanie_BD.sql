
IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'Fitnes_Inventory')
BEGIN
    CREATE DATABASE Fitnes_Inventory;
END
GO

USE Fitnes_Inventory;
GO

CREATE TABLE Equipment_Category (
    category_id INT PRIMARY KEY IDENTITY(1,1),
    category_name VARCHAR(100) NOT NULL UNIQUE,
    description TEXT
);
GO

CREATE TABLE Location (
    location_id INT PRIMARY KEY IDENTITY(1,1),
    location_name VARCHAR(100) NOT NULL UNIQUE
);
GO

CREATE TABLE Supplier (
    supplier_id INT PRIMARY KEY IDENTITY(1,1),
    supplier_name VARCHAR(255) NOT NULL,
    contact_phone VARCHAR(50),
    contact_email VARCHAR(255),
    address TEXT
);
GO

CREATE TABLE Equipment (
    equipment_id INT PRIMARY KEY IDENTITY(1,1),
    serial_number VARCHAR(100) UNIQUE,
    equipment_name VARCHAR(255) NOT NULL,
    category_id INT NOT NULL,
    location_id INT,
    supplier_id INT,
    purchase_date DATE,
    purchase_price DECIMAL(15,2),
    status VARCHAR(50),
    FOREIGN KEY (category_id) REFERENCES Equipment_Category(category_id),
    FOREIGN KEY (location_id) REFERENCES Location(location_id),
    FOREIGN KEY (supplier_id) REFERENCES Supplier(supplier_id)
);
GO

CREATE TABLE Inventory_Category (
    category_id INT PRIMARY KEY IDENTITY(1,1),
    category_name VARCHAR(100) NOT NULL UNIQUE,
    unit_of_measure VARCHAR(20) NOT NULL
);
GO

CREATE TABLE Inventory_Item (
    item_id INT PRIMARY KEY IDENTITY(1,1),
    item_name VARCHAR(255) NOT NULL,
    category_id INT NOT NULL,
    supplier_id INT,
    min_stock_level INT NOT NULL DEFAULT 0,
    max_stock_level INT,
    current_quantity INT NOT NULL DEFAULT 0,
    FOREIGN KEY (category_id) REFERENCES Inventory_Category(category_id),
    FOREIGN KEY (supplier_id) REFERENCES Supplier(supplier_id)
);
GO

CREATE TABLE Employee (
    employee_id INT PRIMARY KEY IDENTITY(1,1),
    first_name VARCHAR(100) NOT NULL,
    last_name VARCHAR(100) NOT NULL,
    position VARCHAR(100)
);
GO

CREATE TABLE Inventory_Transaction (
    transaction_id INT PRIMARY KEY IDENTITY(1,1),
    item_id INT NOT NULL,
    transaction_type VARCHAR(50),
    transaction_date DATETIME DEFAULT GETDATE(),
    employee_id INT,
    FOREIGN KEY (item_id) REFERENCES Inventory_Item(item_id),
    FOREIGN KEY (employee_id) REFERENCES Employee(employee_id)
);
GO