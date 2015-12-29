
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 02/27/2015 21:18:11
-- Generated from EDMX file: D:\PROJECTS\UniSuperScrapper\branches\devel\UniSuperScrapper.DbAccess\Unisuper.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [UniSuper];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_UserBalance]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Balances] DROP CONSTRAINT [FK_UserBalance];
GO
IF OBJECT_ID(N'[dbo].[FK_UserTransaction]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Transactions] DROP CONSTRAINT [FK_UserTransaction];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Users]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Users];
GO
IF OBJECT_ID(N'[dbo].[Balances]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Balances];
GO
IF OBJECT_ID(N'[dbo].[Transactions]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Transactions];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Users'
CREATE TABLE [dbo].[Users] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [FirstName] nchar(50)  NOT NULL,
    [MiddleName] nchar(50)  NULL,
    [LastName] nchar(50)  NOT NULL,
    [UnisuperUsername] nvarchar(max)  NOT NULL,
    [UnisuperPassword] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Balances'
CREATE TABLE [dbo].[Balances] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [TimeStamp] datetime  NOT NULL,
    [Amount] decimal(18,2)  NOT NULL,
    [UserId] int  NOT NULL
);
GO

-- Creating table 'Transactions'
CREATE TABLE [dbo].[Transactions] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [TimeStamp] datetime  NOT NULL,
    [Amount] decimal(18,2)  NOT NULL,
    [UserId] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [PK_Users]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Balances'
ALTER TABLE [dbo].[Balances]
ADD CONSTRAINT [PK_Balances]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Transactions'
ALTER TABLE [dbo].[Transactions]
ADD CONSTRAINT [PK_Transactions]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [UserId] in table 'Balances'
ALTER TABLE [dbo].[Balances]
ADD CONSTRAINT [FK_UserBalance]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserBalance'
CREATE INDEX [IX_FK_UserBalance]
ON [dbo].[Balances]
    ([UserId]);
GO

-- Creating foreign key on [UserId] in table 'Transactions'
ALTER TABLE [dbo].[Transactions]
ADD CONSTRAINT [FK_UserTransaction]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserTransaction'
CREATE INDEX [IX_FK_UserTransaction]
ON [dbo].[Transactions]
    ([UserId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------