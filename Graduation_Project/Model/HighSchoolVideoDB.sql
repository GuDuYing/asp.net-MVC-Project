
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 12/25/2016 21:23:47
-- Generated from EDMX file: E:\DataCode\Graduation_Project\asp.net-MVC-Project\Graduation_Project\Model\HighSchoolVideoDB.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [HighSchoolVideoDB];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[T_Course]', 'U') IS NOT NULL
    DROP TABLE [dbo].[T_Course];
GO
IF OBJECT_ID(N'[dbo].[T_Discuss]', 'U') IS NOT NULL
    DROP TABLE [dbo].[T_Discuss];
GO
IF OBJECT_ID(N'[dbo].[T_Exercises]', 'U') IS NOT NULL
    DROP TABLE [dbo].[T_Exercises];
GO
IF OBJECT_ID(N'[dbo].[T_LOG]', 'U') IS NOT NULL
    DROP TABLE [dbo].[T_LOG];
GO
IF OBJECT_ID(N'[dbo].[T_Manage]', 'U') IS NOT NULL
    DROP TABLE [dbo].[T_Manage];
GO
IF OBJECT_ID(N'[dbo].[T_Option]', 'U') IS NOT NULL
    DROP TABLE [dbo].[T_Option];
GO
IF OBJECT_ID(N'[dbo].[T_SchoolInfo]', 'U') IS NOT NULL
    DROP TABLE [dbo].[T_SchoolInfo];
GO
IF OBJECT_ID(N'[dbo].[T_SelectCourse]', 'U') IS NOT NULL
    DROP TABLE [dbo].[T_SelectCourse];
GO
IF OBJECT_ID(N'[dbo].[T_Student]', 'U') IS NOT NULL
    DROP TABLE [dbo].[T_Student];
GO
IF OBJECT_ID(N'[dbo].[T_Teacher]', 'U') IS NOT NULL
    DROP TABLE [dbo].[T_Teacher];
GO
IF OBJECT_ID(N'[dbo].[T_User]', 'U') IS NOT NULL
    DROP TABLE [dbo].[T_User];
GO
IF OBJECT_ID(N'[dbo].[T_Video]', 'U') IS NOT NULL
    DROP TABLE [dbo].[T_Video];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'T_Course'
CREATE TABLE [dbo].[T_Course] (
    [courseID] int IDENTITY(1,1) NOT NULL,
    [courseName] varchar(25)  NULL,
    [TID] int  NOT NULL,
    [startTime] datetime  NULL,
    [endTime] datetime  NULL,
    [courseAbout] varchar(50)  NULL
);
GO

-- Creating table 'T_Discuss'
CREATE TABLE [dbo].[T_Discuss] (
    [QID] int IDENTITY(1,1) NOT NULL,
    [QueUID] int  NULL,
    [QueContent] varchar(100)  NULL,
    [DisTime] datetime  NULL,
    [AnsID] int  NULL,
    [VID] int  NULL
);
GO

-- Creating table 'T_Exercises'
CREATE TABLE [dbo].[T_Exercises] (
    [EID] int IDENTITY(1,1) NOT NULL,
    [EContent] varchar(100)  NULL,
    [Answer] varchar(50)  NULL,
    [Type] varchar(20)  NULL,
    [VID] int  NULL
);
GO

-- Creating table 'T_LOG'
CREATE TABLE [dbo].[T_LOG] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [DATES] datetime  NULL,
    [LEVELS] nvarchar(20)  NULL,
    [LOGGER] nvarchar(200)  NULL,
    [CLIENTUSER] nvarchar(100)  NULL,
    [CLIENTIP] nvarchar(20)  NULL,
    [REQUESTURL] nvarchar(500)  NULL,
    [ACTION] nvarchar(20)  NULL,
    [MESSAGE] nvarchar(4000)  NULL,
    [EXCEPTION] nvarchar(4000)  NULL
);
GO

-- Creating table 'T_Manage'
CREATE TABLE [dbo].[T_Manage] (
    [MID] int IDENTITY(1,1) NOT NULL,
    [MType] varchar(20)  NULL,
    [MContent] varchar(50)  NULL,
    [MTime] datetime  NULL,
    [MStatus] varchar(20)  NULL
);
GO

-- Creating table 'T_Option'
CREATE TABLE [dbo].[T_Option] (
    [OID] int IDENTITY(1,1) NOT NULL,
    [OptionNum] varchar(10)  NULL,
    [OContent] varchar(50)  NULL,
    [EID] int  NULL
);
GO

-- Creating table 'T_SchoolInfo'
CREATE TABLE [dbo].[T_SchoolInfo] (
    [SchoolID] int IDENTITY(1,1) NOT NULL,
    [SchoolName] varchar(30)  NULL,
    [SchoolAbout] varchar(100)  NULL,
    [BuildTime] datetime  NULL
);
GO

-- Creating table 'T_SelectCourse'
CREATE TABLE [dbo].[T_SelectCourse] (
    [SCID] int IDENTITY(1,1) NOT NULL,
    [SID] int  NOT NULL,
    [CourseID] int  NOT NULL
);
GO

-- Creating table 'T_Student'
CREATE TABLE [dbo].[T_Student] (
    [SID] int IDENTITY(1,1) NOT NULL,
    [StuNum] varchar(12)  NULL,
    [StuName] varchar(20)  NULL,
    [StuSex] varchar(4)  NULL,
    [SchoolID] int  NULL,
    [UserID] int  NULL
);
GO

-- Creating table 'T_Teacher'
CREATE TABLE [dbo].[T_Teacher] (
    [TID] int IDENTITY(1,1) NOT NULL,
    [TeacherNum] int  NULL,
    [TeacherName] varchar(20)  NULL,
    [TeacherSex] varchar(4)  NULL,
    [ProfessionalTitle] varchar(8)  NULL,
    [TeacherAbout] varchar(50)  NULL,
    [SchoolID] int  NULL,
    [UserID] int  NULL
);
GO

-- Creating table 'T_User'
CREATE TABLE [dbo].[T_User] (
    [UID] int IDENTITY(1,1) NOT NULL,
    [UserName] varchar(30)  NULL,
    [UserEmail] varchar(30)  NULL,
    [UserPwd] varchar(20)  NULL,
    [UsrPhone] varchar(24)  NULL,
    [UserStatu] varchar(8)  NULL,
    [UserType] varchar(16)  NULL
);
GO

-- Creating table 'T_Video'
CREATE TABLE [dbo].[T_Video] (
    [VID] int IDENTITY(1,1) NOT NULL,
    [VideoName] varchar(30)  NULL,
    [VideoAbout] varchar(50)  NULL,
    [InitTime] datetime  NULL,
    [VideoID] varchar(30)  NULL,
    [IsHaveExerc] smallint  NULL,
    [CourseID] int  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [courseID] in table 'T_Course'
ALTER TABLE [dbo].[T_Course]
ADD CONSTRAINT [PK_T_Course]
    PRIMARY KEY CLUSTERED ([courseID] ASC);
GO

-- Creating primary key on [QID] in table 'T_Discuss'
ALTER TABLE [dbo].[T_Discuss]
ADD CONSTRAINT [PK_T_Discuss]
    PRIMARY KEY CLUSTERED ([QID] ASC);
GO

-- Creating primary key on [EID] in table 'T_Exercises'
ALTER TABLE [dbo].[T_Exercises]
ADD CONSTRAINT [PK_T_Exercises]
    PRIMARY KEY CLUSTERED ([EID] ASC);
GO

-- Creating primary key on [ID] in table 'T_LOG'
ALTER TABLE [dbo].[T_LOG]
ADD CONSTRAINT [PK_T_LOG]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [MID] in table 'T_Manage'
ALTER TABLE [dbo].[T_Manage]
ADD CONSTRAINT [PK_T_Manage]
    PRIMARY KEY CLUSTERED ([MID] ASC);
GO

-- Creating primary key on [OID] in table 'T_Option'
ALTER TABLE [dbo].[T_Option]
ADD CONSTRAINT [PK_T_Option]
    PRIMARY KEY CLUSTERED ([OID] ASC);
GO

-- Creating primary key on [SchoolID] in table 'T_SchoolInfo'
ALTER TABLE [dbo].[T_SchoolInfo]
ADD CONSTRAINT [PK_T_SchoolInfo]
    PRIMARY KEY CLUSTERED ([SchoolID] ASC);
GO

-- Creating primary key on [SCID] in table 'T_SelectCourse'
ALTER TABLE [dbo].[T_SelectCourse]
ADD CONSTRAINT [PK_T_SelectCourse]
    PRIMARY KEY CLUSTERED ([SCID] ASC);
GO

-- Creating primary key on [SID] in table 'T_Student'
ALTER TABLE [dbo].[T_Student]
ADD CONSTRAINT [PK_T_Student]
    PRIMARY KEY CLUSTERED ([SID] ASC);
GO

-- Creating primary key on [TID] in table 'T_Teacher'
ALTER TABLE [dbo].[T_Teacher]
ADD CONSTRAINT [PK_T_Teacher]
    PRIMARY KEY CLUSTERED ([TID] ASC);
GO

-- Creating primary key on [UID] in table 'T_User'
ALTER TABLE [dbo].[T_User]
ADD CONSTRAINT [PK_T_User]
    PRIMARY KEY CLUSTERED ([UID] ASC);
GO

-- Creating primary key on [VID] in table 'T_Video'
ALTER TABLE [dbo].[T_Video]
ADD CONSTRAINT [PK_T_Video]
    PRIMARY KEY CLUSTERED ([VID] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------