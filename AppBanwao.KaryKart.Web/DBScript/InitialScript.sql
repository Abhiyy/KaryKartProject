
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 07/24/2016 09:53:47
-- Generated from EDMX file: D:\My work\DA work\Repo\kk\AppBanwao.KaryKart.Web\AppBanwao.KarryKart.Model\KarryKart.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [Karrykart_Test];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_UserAddressDetail_Users]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserAddressDetail] DROP CONSTRAINT [FK_UserAddressDetail_Users];
GO
IF OBJECT_ID(N'[dbo].[FK_UserDetail_Users]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserDetail] DROP CONSTRAINT [FK_UserDetail_Users];
GO
IF OBJECT_ID(N'[dbo].[FK_Users_Roles]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Users] DROP CONSTRAINT [FK_Users_Roles];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Brand]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Brand];
GO
IF OBJECT_ID(N'[dbo].[Category]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Category];
GO
IF OBJECT_ID(N'[dbo].[Country]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Country];
GO
IF OBJECT_ID(N'[dbo].[Coupon]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Coupon];
GO
IF OBJECT_ID(N'[dbo].[CouponImage]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CouponImage];
GO
IF OBJECT_ID(N'[dbo].[CouponType]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CouponType];
GO
IF OBJECT_ID(N'[dbo].[CouponValue]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CouponValue];
GO
IF OBJECT_ID(N'[dbo].[Currency]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Currency];
GO
IF OBJECT_ID(N'[dbo].[DeliverySlot]', 'U') IS NOT NULL
    DROP TABLE [dbo].[DeliverySlot];
GO
IF OBJECT_ID(N'[dbo].[Logs]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Logs];
GO
IF OBJECT_ID(N'[dbo].[Offers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Offers];
GO
IF OBJECT_ID(N'[dbo].[OTPHolder]', 'U') IS NOT NULL
    DROP TABLE [dbo].[OTPHolder];
GO
IF OBJECT_ID(N'[dbo].[Product]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Product];
GO
IF OBJECT_ID(N'[dbo].[ProductFeature]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ProductFeature];
GO
IF OBJECT_ID(N'[dbo].[ProductImage]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ProductImage];
GO
IF OBJECT_ID(N'[dbo].[ProductPrice]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ProductPrice];
GO
IF OBJECT_ID(N'[dbo].[ProductShipping]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ProductShipping];
GO
IF OBJECT_ID(N'[dbo].[ProductSizeMapping]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ProductSizeMapping];
GO
IF OBJECT_ID(N'[dbo].[Roles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Roles];
GO
IF OBJECT_ID(N'[dbo].[Size]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Size];
GO
IF OBJECT_ID(N'[dbo].[SizeType]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SizeType];
GO
IF OBJECT_ID(N'[dbo].[Slider]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Slider];
GO
IF OBJECT_ID(N'[dbo].[Subcategory]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Subcategory];
GO
IF OBJECT_ID(N'[dbo].[Unit]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Unit];
GO
IF OBJECT_ID(N'[dbo].[UserAddressDetail]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserAddressDetail];
GO
IF OBJECT_ID(N'[dbo].[UserDetail]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserDetail];
GO
IF OBJECT_ID(N'[dbo].[Users]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Users];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Brands'
CREATE TABLE [dbo].[Brand] (
    [BrandID] int IDENTITY(1,1) NOT NULL,
    [Name] varchar(100)  NULL
);
GO

-- Creating table 'Categories'
CREATE TABLE [dbo].[Category] (
    [CategoryID] int IDENTITY(1,1) NOT NULL,
    [Name] varchar(50)  NULL
);
GO

-- Creating table 'Countries'
CREATE TABLE [dbo].[Country] (
    [CountryID] int IDENTITY(1,1) NOT NULL,
    [CountryName] varchar(200)  NULL
);
GO

-- Creating table 'Currencies'
CREATE TABLE [dbo].[Currency] (
    [CurrencyID] int IDENTITY(1,1) NOT NULL,
    [Name] varchar(50)  NULL,
    [CountryID] int  NULL,
    [Shortform] varchar(10)  NULL
);
GO

-- Creating table 'DeliverySlots'
CREATE TABLE [dbo].[DeliverySlot] (
    [DeliverySlotID] int IDENTITY(1,1) NOT NULL,
    [Slot] varchar(100)  NULL
);
GO

-- Creating table 'Logs'
CREATE TABLE [dbo].[Logs] (
    [LogId] int IDENTITY(1,1) NOT NULL,
    [MessageType] varchar(50)  NULL,
    [Message] nvarchar(max)  NULL,
    [MethodName] varchar(70)  NULL,
    [FileName] varchar(50)  NULL,
    [UserName] varchar(255)  NULL,
    [EventTimeStamp] datetime  NULL,
    [IpAddress] varchar(20)  NULL,
    [Browser] varchar(500)  NULL
);
GO

-- Creating table 'Offers'
CREATE TABLE [dbo].[Offers] (
    [OfferID] int IDENTITY(1,1) NOT NULL,
    [OfferName] varchar(50)  NULL,
    [ImageLink] varchar(255)  NULL,
    [Active] bit  NULL
);
GO

-- Creating table 'Products'
CREATE TABLE [dbo].[Product] (
    [ProductID] uniqueidentifier  NOT NULL,
    [Name] varchar(200)  NULL,
    [Description] nvarchar(max)  NULL,
    [CategoryID] int  NULL,
    [BrandID] int  NULL,
    [SubCategoryID] int  NULL,
    [CreatedOn] datetime  NULL,
    [UpdatedOn] datetime  NULL,
    [CreatedBy] varchar(255)  NULL,
    [UpdatedBy] varchar(255)  NULL,
    [Active] bit  NULL
);
GO

-- Creating table 'ProductFeatures'
CREATE TABLE [dbo].[ProductFeature] (
    [FeatureID] int IDENTITY(1,1) NOT NULL,
    [ProductID] uniqueidentifier  NULL,
    [Feature] varchar(200)  NULL
);
GO

-- Creating table 'ProductImages'
CREATE TABLE [dbo].[ProductImage] (
    [ImageID] uniqueidentifier  NOT NULL,
    [ProductID] uniqueidentifier  NULL,
    [ImageLink] varchar(255)  NULL
);
GO

-- Creating table 'ProductPrices'
CREATE TABLE [dbo].[ProductPrice] (
    [PriceID] int IDENTITY(1,1) NOT NULL,
    [SizeID] int  NULL,
    [ProductID] uniqueidentifier  NULL,
    [Price] decimal(10,2)  NULL,
    [CurrencyID] int  NULL
);
GO

-- Creating table 'ProductShippings'
CREATE TABLE [dbo].[ProductShipping] (
    [ShippingCostID] int IDENTITY(1,1) NOT NULL,
    [SizeID] int  NULL,
    [ProductID] uniqueidentifier  NULL,
    [Cost] decimal(7,2)  NULL
);
GO

-- Creating table 'ProductSizeMappings'
CREATE TABLE [dbo].[ProductSizeMapping] (
    [ProductSizeMappingID] int IDENTITY(1,1) NOT NULL,
    [ProductID] uniqueidentifier  NULL,
    [SizeID] int  NULL,
    [UnitID] int  NULL,
    [Stock] int  NULL
);
GO

-- Creating table 'Sizes'
CREATE TABLE [dbo].[Size] (
    [SizeID] int IDENTITY(1,1) NOT NULL,
    [Name] varchar(10)  NULL,
    [SizeTypeID] int  NULL
);
GO

-- Creating table 'SizeTypes'
CREATE TABLE [dbo].[SizeType] (
    [SizeTypeID] int IDENTITY(1,1) NOT NULL,
    [Name] varchar(100)  NULL
);
GO

-- Creating table 'Sliders'
CREATE TABLE [dbo].[Slider] (
    [SliderID] int IDENTITY(1,1) NOT NULL,
    [Name] varchar(30)  NULL,
    [OfferHeading] varchar(100)  NULL,
    [ImageLink] varchar(250)  NULL,
    [Offer] varchar(300)  NULL,
    [SlideOrder] int  NULL,
    [Active] bit  NULL
);
GO

-- Creating table 'Subcategories'
CREATE TABLE [dbo].[Subcategory] (
    [SCategoryID] int IDENTITY(1,1) NOT NULL,
    [Name] varchar(100)  NULL,
    [CategoryID] int  NOT NULL
);
GO

-- Creating table 'Units'
CREATE TABLE [dbo].[Unit] (
    [UnitID] int IDENTITY(1,1) NOT NULL,
    [Name] varchar(50)  NULL
);
GO

-- Creating table 'Roles'
CREATE TABLE [dbo].[Roles] (
    [RoleID] int IDENTITY(1,1) NOT NULL,
    [RoleName] varchar(50)  NULL
);
GO

-- Creating table 'Users'
CREATE TABLE [dbo].[Users] (
    [UserID] uniqueidentifier  NOT NULL,
    [EmailAddress] varchar(255)  NULL,
    [Mobile] varchar(20)  NULL,
    [Password] varchar(50)  NULL,
    [DateCreated] datetime  NULL,
    [LastUpdated] datetime  NULL,
    [Active] bit  NULL,
    [RoleID] int  NULL
);
GO

-- Creating table 'OTPHolders'
CREATE TABLE [dbo].[OTPHolder] (
    [OTPID] int IDENTITY(1,1) NOT NULL,
    [OTPValue] varchar(50)  NULL,
    [OTPAssignedTo] varchar(255)  NULL
);
GO

-- Creating table 'Coupons'
CREATE TABLE [dbo].[Coupon] (
    [CouponID] uniqueidentifier  NOT NULL,
    [DisplayName] varchar(50)  NULL,
    [CouponTypeID] int  NULL,
    [Active] bit  NULL,
    [ExpiryDate] datetime  NULL,
    [CreatedDate] datetime  NULL,
    [UpdatedDate] datetime  NULL,
    [CreatedBy] datetime  NULL,
    [UpdatedBy] datetime  NULL
);
GO

-- Creating table 'CouponImages'
CREATE TABLE [dbo].[CouponImage] (
    [CouponImageID] int IDENTITY(1,1) NOT NULL,
    [CouponID] uniqueidentifier  NULL,
    [CouponImageurl] varchar(500)  NULL
);
GO

-- Creating table 'CouponTypes'
CREATE TABLE [dbo].[CouponType] (
    [CouponTypeID] int IDENTITY(1,1) NOT NULL,
    [Type] varchar(50)  NULL
);
GO

-- Creating table 'CouponValues'
CREATE TABLE [dbo].[CouponValue] (
    [CouponValueID] int IDENTITY(1,1) NOT NULL,
    [CouponID] uniqueidentifier  NULL,
    [CouponValue1] decimal(7,2)  NULL
);
GO

-- Creating table 'UserAddressDetails'
CREATE TABLE [dbo].[UserAddressDetail] (
    [AddressID] int IDENTITY(1,1) NOT NULL,
    [UserID] uniqueidentifier  NULL,
    [AddressLine1] varchar(250)  NULL,
    [AddressLine2] varchar(250)  NULL,
    [CityID] int  NULL,
    [StateID] int  NULL,
    [CountryID] int  NULL,
    [Pincode] varchar(10)  NULL
);
GO

-- Creating table 'UserDetails'
CREATE TABLE [dbo].[UserDetail] (
    [UserDetailsID] int IDENTITY(1,1) NOT NULL,
    [UserID] uniqueidentifier  NULL,
    [Salutation] varchar(10)  NULL,
    [FirstName] varchar(50)  NULL,
    [LastName] varchar(50)  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [BrandID] in table 'Brands'
ALTER TABLE [dbo].[Brand]
ADD CONSTRAINT [PK_Brand]
    PRIMARY KEY CLUSTERED ([BrandID] ASC);
GO

-- Creating primary key on [CategoryID] in table 'Categories'
ALTER TABLE [dbo].[Category]
ADD CONSTRAINT [PK_Category]
    PRIMARY KEY CLUSTERED ([CategoryID] ASC);
GO

-- Creating primary key on [CountryID] in table 'Countries'
ALTER TABLE [dbo].[Country]
ADD CONSTRAINT [PK_Country]
    PRIMARY KEY CLUSTERED ([CountryID] ASC);
GO

-- Creating primary key on [CurrencyID] in table 'Currencies'
ALTER TABLE [dbo].[Currency]
ADD CONSTRAINT [PK_Currency]
    PRIMARY KEY CLUSTERED ([CurrencyID] ASC);
GO

-- Creating primary key on [DeliverySlotID] in table 'DeliverySlots'
ALTER TABLE [dbo].[DeliverySlot]
ADD CONSTRAINT [PK_DeliverySlot]
    PRIMARY KEY CLUSTERED ([DeliverySlotID] ASC);
GO

-- Creating primary key on [LogId] in table 'Logs'
ALTER TABLE [dbo].[Logs]
ADD CONSTRAINT [PK_Logs]
    PRIMARY KEY CLUSTERED ([LogId] ASC);
GO

-- Creating primary key on [OfferID] in table 'Offers'
ALTER TABLE [dbo].[Offers]
ADD CONSTRAINT [PK_Offers]
    PRIMARY KEY CLUSTERED ([OfferID] ASC);
GO

-- Creating primary key on [ProductID] in table 'Products'
ALTER TABLE [dbo].[Product]
ADD CONSTRAINT [PK_Product]
    PRIMARY KEY CLUSTERED ([ProductID] ASC);
GO

-- Creating primary key on [FeatureID] in table 'ProductFeatures'
ALTER TABLE [dbo].[ProductFeature]
ADD CONSTRAINT [PK_ProductFeature]
    PRIMARY KEY CLUSTERED ([FeatureID] ASC);
GO

-- Creating primary key on [ImageID] in table 'ProductImages'
ALTER TABLE [dbo].[ProductImage]
ADD CONSTRAINT [PK_ProductImage]
    PRIMARY KEY CLUSTERED ([ImageID] ASC);
GO

-- Creating primary key on [PriceID] in table 'ProductPrices'
ALTER TABLE [dbo].[ProductPrice]
ADD CONSTRAINT [PK_ProductPrice]
    PRIMARY KEY CLUSTERED ([PriceID] ASC);
GO

-- Creating primary key on [ShippingCostID] in table 'ProductShippings'
ALTER TABLE [dbo].[ProductShipping]
ADD CONSTRAINT [PK_ProductShipping]
    PRIMARY KEY CLUSTERED ([ShippingCostID] ASC);
GO

-- Creating primary key on [ProductSizeMappingID] in table 'ProductSizeMappings'
ALTER TABLE [dbo].[ProductSizeMapping]
ADD CONSTRAINT [PK_ProductSizeMapping]
    PRIMARY KEY CLUSTERED ([ProductSizeMappingID] ASC);
GO

-- Creating primary key on [SizeID] in table 'Sizes'
ALTER TABLE [dbo].[Size]
ADD CONSTRAINT [PK_Size]
    PRIMARY KEY CLUSTERED ([SizeID] ASC);
GO

-- Creating primary key on [SizeTypeID] in table 'SizeTypes'
ALTER TABLE [dbo].[SizeType]
ADD CONSTRAINT [PK_SizeType]
    PRIMARY KEY CLUSTERED ([SizeTypeID] ASC);
GO

-- Creating primary key on [SliderID] in table 'Sliders'
ALTER TABLE [dbo].[Slider]
ADD CONSTRAINT [PK_Slider]
    PRIMARY KEY CLUSTERED ([SliderID] ASC);
GO

-- Creating primary key on [SCategoryID] in table 'Subcategories'
ALTER TABLE [dbo].[Subcategory]
ADD CONSTRAINT [PK_Subcategory]
    PRIMARY KEY CLUSTERED ([SCategoryID] ASC);
GO

-- Creating primary key on [UnitID] in table 'Units'
ALTER TABLE [dbo].[Unit]
ADD CONSTRAINT [PK_Unit]
    PRIMARY KEY CLUSTERED ([UnitID] ASC);
GO

-- Creating primary key on [RoleID] in table 'Roles'
ALTER TABLE [dbo].[Roles]
ADD CONSTRAINT [PK_Roles]
    PRIMARY KEY CLUSTERED ([RoleID] ASC);
GO

-- Creating primary key on [UserID] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [PK_Users]
    PRIMARY KEY CLUSTERED ([UserID] ASC);
GO

-- Creating primary key on [OTPID] in table 'OTPHolders'
ALTER TABLE [dbo].[OTPHolder]
ADD CONSTRAINT [PK_OTPHolder]
    PRIMARY KEY CLUSTERED ([OTPID] ASC);
GO

-- Creating primary key on [CouponID] in table 'Coupons'
ALTER TABLE [dbo].[Coupon]
ADD CONSTRAINT [PK_Coupon]
    PRIMARY KEY CLUSTERED ([CouponID] ASC);
GO

-- Creating primary key on [CouponImageID] in table 'CouponImages'
ALTER TABLE [dbo].[CouponImage]
ADD CONSTRAINT [PK_CouponImage]
    PRIMARY KEY CLUSTERED ([CouponImageID] ASC);
GO

-- Creating primary key on [CouponTypeID] in table 'CouponTypes'
ALTER TABLE [dbo].[CouponType]
ADD CONSTRAINT [PK_CouponType]
    PRIMARY KEY CLUSTERED ([CouponTypeID] ASC);
GO

-- Creating primary key on [CouponValueID] in table 'CouponValues'
ALTER TABLE [dbo].[CouponValue]
ADD CONSTRAINT [PK_CouponValue]
    PRIMARY KEY CLUSTERED ([CouponValueID] ASC);
GO

-- Creating primary key on [AddressID] in table 'UserAddressDetails'
ALTER TABLE [dbo].[UserAddressDetail]
ADD CONSTRAINT [PK_UserAddressDetail]
    PRIMARY KEY CLUSTERED ([AddressID] ASC);
GO

-- Creating primary key on [UserDetailsID] in table 'UserDetails'
ALTER TABLE [dbo].[UserDetail]
ADD CONSTRAINT [PK_UserDetail]
    PRIMARY KEY CLUSTERED ([UserDetailsID] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [RoleID] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [FK_Users_Roles]
    FOREIGN KEY ([RoleID])
    REFERENCES [dbo].[Roles]
        ([RoleID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Users_Roles'
CREATE INDEX [IX_FK_Users_Roles]
ON [dbo].[Users]
    ([RoleID]);
GO

-- Creating foreign key on [UserID] in table 'UserAddressDetails'
ALTER TABLE [dbo].[UserAddressDetail]
ADD CONSTRAINT [FK_UserAddressDetail_Users]
    FOREIGN KEY ([UserID])
    REFERENCES [dbo].[Users]
        ([UserID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserAddressDetail_Users'
CREATE INDEX [IX_FK_UserAddressDetail_Users]
ON [dbo].[UserAddressDetail]
    ([UserID]);
GO

-- Creating foreign key on [UserID] in table 'UserDetails'
ALTER TABLE [dbo].[UserDetail]
ADD CONSTRAINT [FK_UserDetail_Users]
    FOREIGN KEY ([UserID])
    REFERENCES [dbo].[Users]
        ([UserID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserDetail_Users'
CREATE INDEX [IX_FK_UserDetail_Users]
ON [dbo].[UserDetail]
    ([UserID]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------

INSERT INTO [Roles]
           ([RoleName])
     VALUES
           ('Administrator'),
           ('Customer')
GO




