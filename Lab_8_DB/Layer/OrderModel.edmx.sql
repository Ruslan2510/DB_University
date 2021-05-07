
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 05/05/2021 11:38:07
-- Generated from EDMX file: C:\Users\Lenovo\source\repos\Lab_8_DB\Layer\OrderModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [Lab_8_DB];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------


-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'OrderSet'
CREATE TABLE [dbo].[OrderSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Status] nvarchar(max)  NOT NULL,
    [Comment] nvarchar(max)  NOT NULL,
    [Broker_Id] int  NOT NULL,
    [Client_Id] int  NOT NULL
);
GO

-- Creating table 'ClientSet'
CREATE TABLE [dbo].[ClientSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Surname] nvarchar(max)  NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Patronymic] nvarchar(max)  NOT NULL,
    [Email] nvarchar(max)  NOT NULL,
    [Phone] nvarchar(max)  NOT NULL,
    [Balance] decimal(18,0)  NOT NULL
);
GO

-- Creating table 'BrokerSet'
CREATE TABLE [dbo].[BrokerSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Surname] nvarchar(max)  NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Patronymic] nvarchar(max)  NOT NULL,
    [Email] nvarchar(max)  NOT NULL,
    [Phone] nvarchar(max)  NOT NULL,
    [Experience] nvarchar(max)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'OrderSet'
ALTER TABLE [dbo].[OrderSet]
ADD CONSTRAINT [PK_OrderSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ClientSet'
ALTER TABLE [dbo].[ClientSet]
ADD CONSTRAINT [PK_ClientSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'BrokerSet'
ALTER TABLE [dbo].[BrokerSet]
ADD CONSTRAINT [PK_BrokerSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Broker_Id] in table 'OrderSet'
ALTER TABLE [dbo].[OrderSet]
ADD CONSTRAINT [FK_BrokerOrder]
    FOREIGN KEY ([Broker_Id])
    REFERENCES [dbo].[BrokerSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_BrokerOrder'
CREATE INDEX [IX_FK_BrokerOrder]
ON [dbo].[OrderSet]
    ([Broker_Id]);
GO

-- Creating foreign key on [Client_Id] in table 'OrderSet'
ALTER TABLE [dbo].[OrderSet]
ADD CONSTRAINT [FK_ClientOrder]
    FOREIGN KEY ([Client_Id])
    REFERENCES [dbo].[ClientSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ClientOrder'
CREATE INDEX [IX_FK_ClientOrder]
ON [dbo].[OrderSet]
    ([Client_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------