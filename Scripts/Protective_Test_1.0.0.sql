USE [master]
GO
/****** Object:  Database [ProtectiveTest]    Script Date: 5/13/2014 10:20:03 AM ******/
CREATE DATABASE [ProtectiveTest] ON  PRIMARY 
( NAME = N'ProtectiveTest', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.SQL2012\MSSQL\DATA\ProtectiveTest.mdf' , SIZE = 3072KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'ProtectiveTest_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.SQL2012\MSSQL\DATA\ProtectiveTest_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ProtectiveTest].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [ProtectiveTest] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [ProtectiveTest] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [ProtectiveTest] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [ProtectiveTest] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [ProtectiveTest] SET ARITHABORT OFF 
GO
ALTER DATABASE [ProtectiveTest] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [ProtectiveTest] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [ProtectiveTest] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [ProtectiveTest] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [ProtectiveTest] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [ProtectiveTest] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [ProtectiveTest] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [ProtectiveTest] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [ProtectiveTest] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [ProtectiveTest] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [ProtectiveTest] SET  DISABLE_BROKER 
GO
ALTER DATABASE [ProtectiveTest] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [ProtectiveTest] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [ProtectiveTest] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [ProtectiveTest] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [ProtectiveTest] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [ProtectiveTest] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [ProtectiveTest] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [ProtectiveTest] SET RECOVERY FULL 
GO
ALTER DATABASE [ProtectiveTest] SET  MULTI_USER 
GO
ALTER DATABASE [ProtectiveTest] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [ProtectiveTest] SET DB_CHAINING OFF 
GO
EXEC sys.sp_db_vardecimal_storage_format N'ProtectiveTest', N'ON'
GO
USE [ProtectiveTest]
GO
/****** Object:  Table [dbo].[Messages]    Script Date: 5/13/2014 10:20:03 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Messages](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[MessageText] [nvarchar](100) NOT NULL,
	[IsStarred] [bit] NOT NULL,
	[AddedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Messages] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Roles]    Script Date: 5/13/2014 10:20:03 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[Id] [int] NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[UserRoles]    Script Date: 5/13/2014 10:20:03 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserRoles](
	[UserId] [int] NOT NULL,
	[RoleId] [int] NOT NULL,
 CONSTRAINT [PK_UserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Users]    Script Date: 5/13/2014 10:20:03 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](50) NOT NULL,
	[FirstName] [nvarchar](128) NOT NULL,
	[LastName] [nvarchar](128) NOT NULL,
	[Email] [nvarchar](256) NOT NULL,
	[PasswordHash] [nvarchar](max) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[IsApproved] [bit] NOT NULL,
	[IsLockedOut] [bit] NOT NULL,
	[PasswordReset] [bit] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
INSERT [dbo].[Roles] ([Id], [Name], [CreatedDate]) VALUES (1, N'Administrator', CAST(0x0000A32A00A0764A AS DateTime))
GO
INSERT [dbo].[Roles] ([Id], [Name], [CreatedDate]) VALUES (2, N'Viewer', CAST(0x0000A32A00A0764A AS DateTime))
GO
INSERT [dbo].[UserRoles] ([UserId], [RoleId]) VALUES (1, 1)
GO
INSERT [dbo].[UserRoles] ([UserId], [RoleId]) VALUES (1, 2)
GO
SET IDENTITY_INSERT [dbo].[Users] ON 

GO
INSERT [dbo].[Users] ([Id], [UserName], [FirstName], [LastName], [Email], [PasswordHash], [IsDeleted], [IsApproved], [IsLockedOut], [PasswordReset], [CreateDate]) VALUES (1, N'admin', N'Administrator', N'', N'admin@test.com', N'ANa+oTOZSGG8tAgkkSpYYKWVXJpqgPkeFA4SaBU+a/EBhRNW+hxUpLtq4xORsGeKIQ==', 0, 1, 0, 0, CAST(0x0000A32A00A17F14 AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
ALTER TABLE [dbo].[Messages] ADD  CONSTRAINT [DF_Messages_IsStarred]  DEFAULT ((0)) FOR [IsStarred]
GO
ALTER TABLE [dbo].[Messages] ADD  CONSTRAINT [DF_Messages_AddedDate]  DEFAULT (getdate()) FOR [AddedDate]
GO
ALTER TABLE [dbo].[Roles] ADD  CONSTRAINT [DF_Roles_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_Users_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO
ALTER TABLE [dbo].[UserRoles]  WITH NOCHECK ADD  CONSTRAINT [FK_UserRoles_Roles] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Roles] ([Id])
GO
ALTER TABLE [dbo].[UserRoles] CHECK CONSTRAINT [FK_UserRoles_Roles]
GO
ALTER TABLE [dbo].[UserRoles]  WITH NOCHECK ADD  CONSTRAINT [FK_UserRoles_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[UserRoles] CHECK CONSTRAINT [FK_UserRoles_User]
GO
USE [master]
GO
ALTER DATABASE [ProtectiveTest] SET  READ_WRITE 
GO
