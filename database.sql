USE [WaterQualityDB]
GO

/****** Object: Table [dbo].[Users] Script Date: 1/18/2026 4:57:06 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Users] (
    [UserID]           INT            IDENTITY (1, 1) NOT NULL,
    [Username]         NVARCHAR (50)  NOT NULL,
    [Email]            NVARCHAR (100) NOT NULL,
    [Password]         NVARCHAR (255) NOT NULL,
    [RegistrationDate] DATETIME2 (7)  NOT NULL,
    [PhoneNumber]      NVARCHAR (15)  NOT NULL,
    [Governorate]      NVARCHAR (50)  NOT NULL
);

USE [WaterQualityDB]
GO

/****** Object: Table [dbo].[WaterReports] Script Date: 1/18/2026 4:58:01 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[WaterReports] (
    [ReportID]      INT            IDENTITY (1, 1) NOT NULL,
    [UserID]        INT            NOT NULL,
    [Location]      NVARCHAR (100) NOT NULL,
    [ReportDate]    DATETIME2 (7)  NOT NULL,
    [PollutionType] NVARCHAR (50)  NOT NULL,
    [Description]   NVARCHAR (500) NOT NULL,
    [Status]        NVARCHAR (20)  NOT NULL,
    [SourceType]    NVARCHAR (50)  NOT NULL
);


GO
CREATE NONCLUSTERED INDEX [IX_WaterReports_UserID]
    ON [dbo].[WaterReports]([UserID] ASC);


GO
ALTER TABLE [dbo].[WaterReports]
    ADD CONSTRAINT [PK_WaterReports] PRIMARY KEY CLUSTERED ([ReportID] ASC);


GO
ALTER TABLE [dbo].[WaterReports]
    ADD CONSTRAINT [FK_WaterReports_Users_UserID] FOREIGN KEY ([UserID]) REFERENCES [dbo].[Users] ([UserID]) ON DELETE CASCADE;


USE [WaterQualityDB]
GO

/****** Object: Table [dbo].[__EFMigrationsHistory] Script Date: 1/18/2026 4:58:30 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[__EFMigrationsHistory] (
    [MigrationId]    NVARCHAR (150) NOT NULL,
    [ProductVersion] NVARCHAR (32)  NOT NULL
);


