
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 09/26/2019 01:12:28
-- Generated from EDMX file: C:\Users\Satwiko\source\repos\FIT5032_Assignment_Portfolio - Copy\Models\FIT5032_Assignment_Model.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [FIT5032_Assignment_Portfolio];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_HotelRoom]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Rooms] DROP CONSTRAINT [FK_HotelRoom];
GO
IF OBJECT_ID(N'[dbo].[FK_RoomRoomSpec]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RoomSpecs] DROP CONSTRAINT [FK_RoomRoomSpec];
GO
IF OBJECT_ID(N'[dbo].[FK_HotelHotelFacility]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[HotelFacilities] DROP CONSTRAINT [FK_HotelHotelFacility];
GO
IF OBJECT_ID(N'[dbo].[FK_RoomBooking]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Bookings] DROP CONSTRAINT [FK_RoomBooking];
GO
IF OBJECT_ID(N'[dbo].[FK_UserTypeUser]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Users] DROP CONSTRAINT [FK_UserTypeUser];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Rooms]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Rooms];
GO
IF OBJECT_ID(N'[dbo].[Hotels]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Hotels];
GO
IF OBJECT_ID(N'[dbo].[RoomSpecs]', 'U') IS NOT NULL
    DROP TABLE [dbo].[RoomSpecs];
GO
IF OBJECT_ID(N'[dbo].[HotelFacilities]', 'U') IS NOT NULL
    DROP TABLE [dbo].[HotelFacilities];
GO
IF OBJECT_ID(N'[dbo].[Bookings]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Bookings];
GO
IF OBJECT_ID(N'[dbo].[Users]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Users];
GO
IF OBJECT_ID(N'[dbo].[UserTypes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserTypes];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Rooms'
CREATE TABLE [dbo].[Rooms] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [RoomName] nvarchar(max)  NOT NULL,
    [Description] nvarchar(max)  NOT NULL,
    [Availability] bigint  NOT NULL,
    [Rate] smallint  NOT NULL,
    [HotelId] int  NOT NULL
);
GO

-- Creating table 'Hotels'
CREATE TABLE [dbo].[Hotels] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [HotelName] nvarchar(max)  NOT NULL,
    [Description] nvarchar(max)  NOT NULL,
    [HotelStar] smallint  NOT NULL,
    [Longitude] decimal(18,0)  NOT NULL,
    [Latitude] decimal(18,0)  NOT NULL,
    [UserId] nvarchar(max)  NULL,
    [Neighbourhood] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'RoomSpecs'
CREATE TABLE [dbo].[RoomSpecs] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Specification] nvarchar(max)  NOT NULL,
    [RoomId] int  NOT NULL
);
GO

-- Creating table 'HotelFacilities'
CREATE TABLE [dbo].[HotelFacilities] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Facility] nvarchar(max)  NOT NULL,
    [HotelId] int  NOT NULL
);
GO

-- Creating table 'Bookings'
CREATE TABLE [dbo].[Bookings] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Value] decimal(18,0)  NOT NULL,
    [Review] nvarchar(max)  NULL,
    [Rating] smallint  NULL,
    [RoomId] int  NOT NULL,
    [UserId] nvarchar(max)  NOT NULL,
    [Date] datetime  NOT NULL
);
GO

-- Creating table 'Users'
CREATE TABLE [dbo].[Users] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [UserName] nvarchar(max)  NOT NULL,
    [Password] nvarchar(max)  NOT NULL,
    [FirstName] nvarchar(max)  NOT NULL,
    [LastName] nvarchar(max)  NOT NULL,
    [Email] nvarchar(max)  NOT NULL,
    [Mobile] nvarchar(max)  NOT NULL,
    [UserTypeId] int  NOT NULL
);
GO

-- Creating table 'UserTypes'
CREATE TABLE [dbo].[UserTypes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Description] nvarchar(max)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Rooms'
ALTER TABLE [dbo].[Rooms]
ADD CONSTRAINT [PK_Rooms]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Hotels'
ALTER TABLE [dbo].[Hotels]
ADD CONSTRAINT [PK_Hotels]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'RoomSpecs'
ALTER TABLE [dbo].[RoomSpecs]
ADD CONSTRAINT [PK_RoomSpecs]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'HotelFacilities'
ALTER TABLE [dbo].[HotelFacilities]
ADD CONSTRAINT [PK_HotelFacilities]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Bookings'
ALTER TABLE [dbo].[Bookings]
ADD CONSTRAINT [PK_Bookings]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [PK_Users]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'UserTypes'
ALTER TABLE [dbo].[UserTypes]
ADD CONSTRAINT [PK_UserTypes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [HotelId] in table 'Rooms'
ALTER TABLE [dbo].[Rooms]
ADD CONSTRAINT [FK_HotelRoom]
    FOREIGN KEY ([HotelId])
    REFERENCES [dbo].[Hotels]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_HotelRoom'
CREATE INDEX [IX_FK_HotelRoom]
ON [dbo].[Rooms]
    ([HotelId]);
GO

-- Creating foreign key on [RoomId] in table 'RoomSpecs'
ALTER TABLE [dbo].[RoomSpecs]
ADD CONSTRAINT [FK_RoomRoomSpec]
    FOREIGN KEY ([RoomId])
    REFERENCES [dbo].[Rooms]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_RoomRoomSpec'
CREATE INDEX [IX_FK_RoomRoomSpec]
ON [dbo].[RoomSpecs]
    ([RoomId]);
GO

-- Creating foreign key on [HotelId] in table 'HotelFacilities'
ALTER TABLE [dbo].[HotelFacilities]
ADD CONSTRAINT [FK_HotelHotelFacility]
    FOREIGN KEY ([HotelId])
    REFERENCES [dbo].[Hotels]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_HotelHotelFacility'
CREATE INDEX [IX_FK_HotelHotelFacility]
ON [dbo].[HotelFacilities]
    ([HotelId]);
GO

-- Creating foreign key on [RoomId] in table 'Bookings'
ALTER TABLE [dbo].[Bookings]
ADD CONSTRAINT [FK_RoomBooking]
    FOREIGN KEY ([RoomId])
    REFERENCES [dbo].[Rooms]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_RoomBooking'
CREATE INDEX [IX_FK_RoomBooking]
ON [dbo].[Bookings]
    ([RoomId]);
GO

-- Creating foreign key on [UserTypeId] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [FK_UserTypeUser]
    FOREIGN KEY ([UserTypeId])
    REFERENCES [dbo].[UserTypes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserTypeUser'
CREATE INDEX [IX_FK_UserTypeUser]
ON [dbo].[Users]
    ([UserTypeId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------