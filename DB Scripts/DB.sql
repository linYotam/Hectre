USE [master]
GO
/****** Object:  Database [Hectre]    Script Date: 23/07/2023 16:13:54 ******/
CREATE DATABASE [Hectre]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Hectre', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\Hectre.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Hectre_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\Hectre_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [Hectre] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Hectre].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Hectre] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Hectre] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Hectre] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Hectre] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Hectre] SET ARITHABORT OFF 
GO
ALTER DATABASE [Hectre] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Hectre] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Hectre] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Hectre] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Hectre] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Hectre] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Hectre] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Hectre] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Hectre] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Hectre] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Hectre] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Hectre] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Hectre] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Hectre] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Hectre] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Hectre] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Hectre] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Hectre] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [Hectre] SET  MULTI_USER 
GO
ALTER DATABASE [Hectre] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Hectre] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Hectre] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Hectre] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Hectre] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Hectre] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [Hectre] SET QUERY_STORE = OFF
GO
USE [Hectre]
GO
/****** Object:  Table [dbo].[Harvests]    Script Date: 23/07/2023 16:13:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Harvests](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[OrchardId] [uniqueidentifier] NOT NULL,
	[SupervisorId] [uniqueidentifier] NOT NULL,
	[PickerId] [uniqueidentifier] NOT NULL,
	[PickingDate] [datetime] NOT NULL,
	[Type] [nvarchar](50) NOT NULL,
	[BinCount] [int] NOT NULL,
	[HourlyWageRate] [decimal](10, 2) NOT NULL,
	[HoursWorked] [int] NOT NULL,
	[Variety] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK__Harvests__3214EC076E1D5881] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Orchards]    Script Date: 23/07/2023 16:13:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Orchards](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Block] [int] NOT NULL,
	[SubBlock] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Timesheets]    Script Date: 23/07/2023 16:13:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Timesheets](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[OrchardId] [uniqueidentifier] NOT NULL,
	[SupervisorId] [uniqueidentifier] NOT NULL,
	[PickerId] [uniqueidentifier] NOT NULL,
	[StartTime] [nvarchar](50) NOT NULL,
	[EndTime] [nvarchar](50) NOT NULL,
	[Activity] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK__Timeshee__3214EC0716B4C479] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 23/07/2023 16:13:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[user_id] [int] IDENTITY(1,1) NOT NULL,
	[user_name] [nvarchar](50) NOT NULL,
	[user_email] [nvarchar](50) NOT NULL,
	[user_password] [nvarchar](255) NOT NULL,
	[user_type] [nvarchar](10) NULL,
	[jwt_token] [nvarchar](255) NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[user_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Harvests] ON 

INSERT [dbo].[Harvests] ([Id], [OrchardId], [SupervisorId], [PickerId], [PickingDate], [Type], [BinCount], [HourlyWageRate], [HoursWorked], [Variety]) VALUES (3, N'769e6571-100e-4a18-a577-34a49f0c8ed3', N'87892a43-d96b-4b90-8c4c-925d07fe07fb', N'd7895f81-66b1-11ed-a52c-0242c0a85002', CAST(N'2023-06-01T12:30:00.000' AS DateTime), N'Bin', 396, CAST(22.50 AS Decimal(10, 2)), 5, N'Royal')
INSERT [dbo].[Harvests] ([Id], [OrchardId], [SupervisorId], [PickerId], [PickingDate], [Type], [BinCount], [HourlyWageRate], [HoursWorked], [Variety]) VALUES (9, N'999e6571-100e-4a18-a577-34a49f0c8ed3', N'87892a43-d96b-4b90-8c4c-925d07fe07fb', N'd7895f81-66b1-11ed-a52c-0242c0a85002', CAST(N'2023-06-01T12:30:00.000' AS DateTime), N'Bin', 523, CAST(24.50 AS Decimal(10, 2)), 12, N'Gala')
INSERT [dbo].[Harvests] ([Id], [OrchardId], [SupervisorId], [PickerId], [PickingDate], [Type], [BinCount], [HourlyWageRate], [HoursWorked], [Variety]) VALUES (10, N'888e6571-100e-4a18-a577-34a49f0c8ed3', N'97892a43-d96b-4b90-8c4c-925d07fe07fb', N'd9895f81-66b1-11ed-a52c-0242c0a85002', CAST(N'2023-05-01T12:30:00.000' AS DateTime), N'Bin', 874, CAST(30.00 AS Decimal(10, 2)), 18, N'Envy')
INSERT [dbo].[Harvests] ([Id], [OrchardId], [SupervisorId], [PickerId], [PickingDate], [Type], [BinCount], [HourlyWageRate], [HoursWorked], [Variety]) VALUES (11, N'769e6571-100e-4a18-a577-34a49f0c8ed3', N'87892a43-d96b-4b90-8c4c-925d07fe07fb', N'd7895f81-66b1-11ed-a52c-0242c0a85002', CAST(N'2023-06-01T12:30:00.000' AS DateTime), N'Bin', 582, CAST(22.50 AS Decimal(10, 2)), 5, N'Envy')
SET IDENTITY_INSERT [dbo].[Harvests] OFF
GO
INSERT [dbo].[Orchards] ([Id], [Name], [Block], [SubBlock]) VALUES (N'888e6571-100e-4a18-a577-34a49f0c8ed3', N'Grapes', 8, N'35C')
INSERT [dbo].[Orchards] ([Id], [Name], [Block], [SubBlock]) VALUES (N'769e6571-100e-4a18-a577-34a49f0c8ed3', N'Apples', 17, N'22B')
INSERT [dbo].[Orchards] ([Id], [Name], [Block], [SubBlock]) VALUES (N'999e6571-100e-4a18-a577-34a49f0c8ed3', N'Oranges', 12, N'10A')
GO
SET IDENTITY_INSERT [dbo].[Timesheets] ON 

INSERT [dbo].[Timesheets] ([Id], [OrchardId], [SupervisorId], [PickerId], [StartTime], [EndTime], [Activity]) VALUES (1, N'769e6571-100e-4a18-a577-34a49f0c8ed3', N'87892a43-d96b-4b90-8c4c-925d07fe07fb', N'd7895f81-66b1-11ed-a52c-0242c0a85002', N'2023-06-01 11:00:00', N'2023-06-01 15:00:00', N'Harvest')
SET IDENTITY_INSERT [dbo].[Timesheets] OFF
GO
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([user_id], [user_name], [user_email], [user_password], [user_type], [jwt_token]) VALUES (2, N'Yotam Lin', N'linyotam@gmail.com', N'$2a$11$ScdUGaqnYgAxpko32eHi5OnFhmoepbWJl5e4EE7UhfiXvWQozkzkK', N'admin', N' ')
INSERT [dbo].[Users] ([user_id], [user_name], [user_email], [user_password], [user_type], [jwt_token]) VALUES (22, N'Ella Lin', N'ellalin@gmail.com', N'$2a$11$0fVlafn5sBzsDVU5ZsZKnePXyBRTVGUAHbQrIxS9ASShgpbawsm6W', N'customer', N'')
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
ALTER TABLE [dbo].[Harvests]  WITH CHECK ADD  CONSTRAINT [FK_Harvest_Orchard] FOREIGN KEY([OrchardId])
REFERENCES [dbo].[Orchards] ([Id])
GO
ALTER TABLE [dbo].[Harvests] CHECK CONSTRAINT [FK_Harvest_Orchard]
GO
ALTER TABLE [dbo].[Timesheets]  WITH CHECK ADD  CONSTRAINT [FK_Timesheet_Orchard] FOREIGN KEY([OrchardId])
REFERENCES [dbo].[Orchards] ([Id])
GO
ALTER TABLE [dbo].[Timesheets] CHECK CONSTRAINT [FK_Timesheet_Orchard]
GO
USE [master]
GO
ALTER DATABASE [Hectre] SET  READ_WRITE 
GO
