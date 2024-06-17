-- Drop the database if it exists
DROP DATABASE IF EXISTS SupplierManagement;

-- Create the database
CREATE DATABASE SupplierManagement;

-- Switch to the SupplierManagement database
USE SupplierManagement;

-- Create the Supplier table
CREATE TABLE Supplier (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Name VARCHAR(255) NOT NULL,
    RepName VARCHAR(255) NOT NULL,
    RepEmail VARCHAR(255) NOT NULL,
    Role VARCHAR(100) NOT NULL,
    Kvk INT(8) NOT NULL,
    City VARCHAR(100) NOT NULL,
    State VARCHAR(100) NOT NULL,
    Country VARCHAR(100) NOT NULL,
    PostalCode VARCHAR(20) NOT NULL,
    Street VARCHAR(255) NOT NULL,
    HouseNumber VARCHAR(10) NOT NULL,
    Status BOOLEAN NOT NULL DEFAULT true,
    CreatedDate DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP
);