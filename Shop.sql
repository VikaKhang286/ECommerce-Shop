USE master;
GO
CREATE DATABASE EShop;
GO
USE EShop;
GO

-- DROP TABLE Banner;
GO
CREATE TABLE Banner
(
    BannerId TINYINT NOT NULL PRIMARY KEY IDENTITY(1, 1),
    Title NVARCHAR(64) NOT NULL,
    Subtitle NVARCHAR(64) NOT NULL,
    ImageUrl NVARCHAR(64) NOT NULL,
    Price DECIMAL(10, 2) NOT NULL
);
GO
SET IDENTITY_INSERT Banner ON
INSERT INTO Banner
    (BannerId, Title, SubTitle, ImageUrl, Price)
VALUES
    (1, 'Women''s latest fashion sale', 'Trending item', 'banner-1.jpg', 20),
    (2, 'Modern sunglasses', 'Trending accessories', 'banner-2.jpg', 15),
    (3, 'New fashion summer sale', 'Sale Offer', 'banner-3.jpg', 29);
SET IDENTITY_INSERT Banner OFF
GO
-- DROP TABLE Category;
GO
CREATE TABLE Category
(
    CategoryId TINYINT NOT NULL PRIMARY KEY IDENTITY(1, 1),
    CategoryName NVARCHAR(64) NOT NULL,
    Icon VARCHAR(16)
);
GO
-- DROP TABLE DealOfDay;
-- DROP TABLE Product;
-- DROP TABLE Category;
-- DROP TABLE Brand;
GO
CREATE TABLE Brand
(
    BrandId SMALLINT NOT NULL PRIMARY KEY IDENTITY(1, 1),
    CategoryId TINYINT NOT NULL REFERENCES Category(CategoryId),
    BrandName NVARCHAR(64) NOT NULL,
    ItemCount SMALLINT NOT NULL,
    Logo VARCHAR(32)
);
GO

SET IDENTITY_INSERT Category ON
INSERT INTO Category
    (CategoryId, CategoryName, Icon)
VALUES
    (1, 'Mobile', 'mobile.png'),
    (2, 'Tablet', 'tablet.png'),
    (3, 'Laptop', 'laptop.png')
SET IDENTITY_INSERT Category OFF
GO

--TRUNCATE TABLE Brand;
GO

SET IDENTITY_INSERT Brand ON
INSERT INTO Brand
    (BrandId, CategoryId, BrandName, ItemCount, Logo)
VALUES
    (1, 1, 'Samsung Mobile', 3, 'samsung.png'),
    (2, 1, 'Xiaomi Mobile', 3, 'xiaomi.png'),
    (3, 1, 'OPPO Mobile', 3, 'oppo.png'),
    (4, 2, 'iPad Tablet', 3, 'ipad.png'),
    (5, 2, 'Samsung Tablet', 3, 'samsung.png'),
    (6, 2, 'OPPO Tablet', 3, 'oppo.png'),
    (7, 3, 'MacBook Laptop', 3, 'macbook.png'),
    (8, 3, 'DELL Laptop', 3, 'dell.png'),
    (9, 3, 'HP Laptop', 3, 'hp.png');
SET IDENTITY_INSERT Brand OFF
GO

USE EShop;
GO

-- DROP TABLE Product;
GO
CREATE TABLE Product
(
    ProductId INT NOT NULL IDENTITY (1, 1) PRIMARY KEY,
    BrandId SMALLINT REFERENCES Brand(BrandId) NOT NULL,
    Title NVARCHAR(128) NOT NULL,
    ImageUrl VARCHAR(255) NOT NULL,
    Description NVARCHAR(MAX),
    Price DECIMAL(10, 2) NOT NULL,
    SaleOff DECIMAL(10, 2) NOT NULL,
);
GO
SET IDENTITY_INSERT Product ON
INSERT INTO Product
    (ProductId, BrandId, Title, ImageUrl, Price, SaleOff)
VALUES
    (1, 1, 'Samsung Galaxy A57', 'samsung-galaxy-a57.jpg', 3400, 5000),
    (2, 1, 'Samsung Galaxy S26 Ultra', 'samsung-galaxy-s26-ultra.jpg', 3000, 8400),
    (3, 1, 'Samsung Galaxy A37', 'samsung-galaxy-a37.jpg', 1000, 4200),
    (4, 2, 'Xiaomi 17', 'xiaomi-17.jpg', 1000, 2400),
    (5, 2, 'Xiaomi 17 Ultra', 'xiaomi-17-ultra.jpg', 6500, 6200),
    (6, 2, 'Xiaomi Redmi Note 15', 'xiaomi-redmi-note-15.jpg', 7800, 5600),
    (7, 3, 'OPPO Find X9s', 'oppo-find-x9s.jpg', 3000, 2000),
    (8, 3, 'OPPO Find X9 Ultra', 'oppo-find-x9-ultra.jpg', 3000, 2000),
    (9, 3, 'OPPO Reno15', 'oppo-reno15.jpg', 2000, 4000),
    (10, 4, 'iPad Air M4', 'ipad-air-m4.jpg', 1000, 3000),
    (11, 4, 'iPad Pro M5', 'ipad-pro-m5.jpg', 1200, 2400),
    (12, 4, 'iPad A16 WiFi', 'ipad-a16-wifi.jpg', 2200, 4400),
    (13, 5, 'Samsung Galaxy Tab A11+', 'samsung-galaxy-tab-a11-plus.jpg', 3200, 4800),
    (14, 5, 'Samsung Galaxy Tab S10 Lite', 'samsung-galaxy-tab-s10-lite.jpg', 2500, 5000),
    (15, 5, 'Samsung Galaxy Tab A11', 'samsung-galaxy-tab-a11.jpg', 4000, 8000),
    (16, 6, 'OPPO Pad 5', 'oppo-pad-5.jpg', 4500, 9000),
    (17, 6, 'OPPO Pad SE WiFi', 'oppo-pad-se-sliver.jpg', 2300, 4600),
    (18, 6, 'OPPO Pad Neo WiFi', 'oppo-pad-neo.jpg', 2400, 4800),
    (19, 7, 'MacBook Neo A18 Pro 8GB/256GB', 'macbook-neo-a18-pro.jpg', 5200, 6500),
    (20, 7, 'MacBook Air M5 16GB/512GB', 'macbook-air-m5.jpg', 4400, 6600),
    (21, 7, 'MacBook Pro M5 16GB/512GB', 'macbook-pro-m5.jpg', 3300, 6700),
    (22, 8, 'Dell 15 R5', 'dell-15-r5.jpg', 3600, 6800),
    (23, 8, 'Dell 15 i7', 'dell-15-i7.jpg', 4100, 7700),
    (24, 8, 'Dell 15 i5', 'dell-15-i5.jpg', 4200, 8800),
    (25, 9, 'HP 15 R5', 'hp-15-r5.jpg', 5100, 9000),
    (26, 9, 'HP 240R G10 Core 5', 'hp-240r-g10-core-5.jpg', 5200, 8600),
    (27, 9, 'HP Pavilion 16 Ultra 5', 'hp-pavilion-16-ultra-5.jpg', 4900, 8800)
GO


--  SELECT * FROM Product;

-- DROP TABLE Member;
GO
CREATE TABLE Member
(
    MemberId VARCHAR(32) NOT NULL PRIMARY KEY,
    Email VARCHAR(64) NOT NULL UNIQUE,
    Password VARBINARY(64) NOT NULL,
    GivenName NVARCHAR(32) NOT NULL,
    Surname NVARCHAR(32),
    LoginDate DATETIME NOT NULL DEFAULT GETDATE(),
    RegisterDate DATETIME NOT NULL DEFAULT GETDATE(),
    Role SMALLINT NOT NULL DEFAULT 0
);
GO

-- SELECT REPLACE (NEWID(), '-', '');
GO
INSERT INTO Member
    (MemberId, Email, [Password], GivenName, Surname, Role)
VALUES
    ('87C375B23BED41A1BDA75108CD786E00', 'khang442000@gmail.com', HASHBYTES('SHA2_512', '123'), N'Khang', N'Vo', 3);
GO

-- DROP PROC SaveMember;
GO
CREATE PROC SaveMember(
    @MemberId VARCHAR(32),
    @Email VARCHAR(64),
    @Password VARBINARY(64),
    @GivenName NVARCHAR(32),
    @Surname NVARCHAR(32) = NULL,
    @Role SMALLINT
)
AS
BEGIN
    IF NOT EXISTS(SELECT * FROM Member WHERE MemberId = @MemberId OR Email = @Email)
        INSERT INTO Member (MemberId, Email, [Password], GivenName, Surname, [Role]) VALUES 
        (@MemberId, @Email, @Password, @GivenName, @Surname, @Role);
END 
GO     

-- DROP TABLE Cart
GO
CREATE TABLE Cart
(
    CartId UNIQUEIDENTIFIER NOT NULL DEFAULT NEWID() PRIMARY KEY,
    MemberId VARCHAR(32) NOT NULL REFERENCES Member(MemberId),
    ProductId INT NOT NULL REFERENCES Product(ProductId),
    Quantity SMALLINT NOT NULL DEFAULT 1,
    AddedDate DATETIME NOT NULL DEFAULT GETDATE(),
    UpdatedDate DATETIME NOT NULL DEFAULT GETDATE()
);
GO
-- DROP PROC SaveCart;
GO
CREATE PROC SaveCart(
    @MemberId VARCHAR(32),
    @ProductId INT,
    @Quantity SMALLINT
)
AS
BEGIN
    IF EXISTS(SELECT * FROM Cart WHERE ProductId = @ProductId AND MemberId = @MemberId)
        UPDATE Cart SET Quantity = Quantity + @Quantity, UpdatedDate = GETDATE() WHERE ProductId = @ProductId AND MemberId = @MemberId;
    ELSE
        INSERT INTO Cart(MemberId, ProductId, Quantity) VALUES(@MemberId, @ProductId, @Quantity)
END

