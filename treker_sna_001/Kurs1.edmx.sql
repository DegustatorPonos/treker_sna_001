
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 04/12/2025 20:12:35
-- Generated from EDMX file: C:\Users\хорек2\source\repos\treker_sna_001\treker_sna_001\Kurs1.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [Kurs1];
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

-- Creating table 'Users'
CREATE TABLE [dbo].[Users] (
    [IdUser] int IDENTITY(1,1) NOT NULL,
    [userLogin] nvarchar(max)  NOT NULL,
    [userPassword] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Journals'
CREATE TABLE [dbo].[Journals] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [UserIdUser] int  NOT NULL,
    [TypeDream] nvarchar(max)  NOT NULL,
    [Feelings] nvarchar(max)  NOT NULL,
    [WakeUpCount] int  NOT NULL,
    [TimeDown] datetime  NOT NULL,
    [TimeWakeUp] datetime  NOT NULL,
    [Stress] nvarchar(max)  NOT NULL,
    [Phisical] nvarchar(max)  NOT NULL,
    [Temperature] nvarchar(max)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [IdUser] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [PK_Users]
    PRIMARY KEY CLUSTERED ([IdUser] ASC);
GO

-- Creating primary key on [Id] in table 'Journals'
ALTER TABLE [dbo].[Journals]
ADD CONSTRAINT [PK_Journals]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [UserIdUser] in table 'Journals'
ALTER TABLE [dbo].[Journals]
ADD CONSTRAINT [FK_UserJournal1]
    FOREIGN KEY ([UserIdUser])
    REFERENCES [dbo].[Users]
        ([IdUser])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserJournal1'
CREATE INDEX [IX_FK_UserJournal1]
ON [dbo].[Journals]
    ([UserIdUser]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------