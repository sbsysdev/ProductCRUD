-- Database
CREATE DATABASE db_products;
USE db_products;

-- Tables
CREATE TABLE categories (
category_id INT IDENTITY(1,1) PRIMARY KEY,
category_name VARCHAR(32) NOT NULL UNIQUE,
category_created DATETIME NOT NULL DEFAULT GETDATE(),
);

CREATE TABLE products (
product_id INT IDENTITY(1,1) PRIMARY KEY,
category_id INT FOREIGN KEY REFERENCES categories(category_id) ON DELETE CASCADE ON UPDATE CASCADE,
product_name VARCHAR(32) NOT NULL UNIQUE,
price FLOAT CHECK (price >= 0) DEFAULT 0,
stock INT CHECK (stock >= 0) DEFAULT 0,
product_created DATETIME NOT NULL DEFAULT GETDATE(),
);

-- Stored Procedures
GO
-- Category Stored Procedure
CREATE PROCEDURE sp_create_category
@category_name VARCHAR(32)
AS
INSERT INTO categories (category_name) VALUES (@category_name);

GO

CREATE PROCEDURE sp_read_all_category
AS
SELECT c.category_id, c.category_name, COUNT(p.product_id) AS category_products, c.category_created
FROM categories AS c LEFT JOIN products AS p ON c.category_id = p.category_id
GROUP BY c.category_id, c.category_name, c.category_created
ORDER BY c.category_id, c.category_name, c.category_created;

GO

CREATE PROCEDURE sp_read_category_by_id
@category_id int
AS
SELECT c.category_id, c.category_name, COUNT(p.product_id) AS category_products, c.category_created
FROM categories AS c LEFT JOIN products AS p ON c.category_id = p.category_id
WHERE c.category_id = @category_id
GROUP BY c.category_id, c.category_name, c.category_created
ORDER BY c.category_id, c.category_name, c.category_created;

GO

CREATE PROCEDURE sp_update_category_by_id
@category_id int,
@category_name VARCHAR(32)
AS
UPDATE categories
SET category_name = @category_name
WHERE category_id = @category_id;

GO

CREATE PROCEDURE sp_delete_category_by_id
@category_id int
AS
DELETE FROM categories WHERE category_id = @category_id;

GO

EXEC sp_read_all_category;
INSERT INTO products (category_id, product_name) VALUES (4, 'Delete cascade 1');
INSERT INTO products (category_id, product_name) VALUES (4, 'Delete cascade 2');
INSERT INTO products (category_id, product_name) VALUES (4, 'Delete cascade 3');
EXEC sp_delete_category_by_id @category_id = 4;
SELECT * FROM products;
