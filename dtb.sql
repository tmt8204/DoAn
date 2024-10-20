USE [master]
GO
/****** Object:  Database [demo]    Script Date: 10/20/2024 7:36:35 PM ******/
CREATE DATABASE [demo]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'demo', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\demo.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'demo_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\demo_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [demo] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [demo].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [demo] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [demo] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [demo] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [demo] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [demo] SET ARITHABORT OFF 
GO
ALTER DATABASE [demo] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [demo] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [demo] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [demo] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [demo] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [demo] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [demo] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [demo] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [demo] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [demo] SET  DISABLE_BROKER 
GO
ALTER DATABASE [demo] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [demo] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [demo] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [demo] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [demo] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [demo] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [demo] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [demo] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [demo] SET  MULTI_USER 
GO
ALTER DATABASE [demo] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [demo] SET DB_CHAINING OFF 
GO
ALTER DATABASE [demo] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [demo] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [demo] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [demo] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [demo] SET QUERY_STORE = ON
GO
ALTER DATABASE [demo] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [demo]
GO
/****** Object:  Table [dbo].[Categories]    Script Date: 10/20/2024 7:36:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categories](
	[CategoryID] [int] NOT NULL,
	[CategoryName] [nvarchar](50) NULL,
 CONSTRAINT [PK__Categori__19093A2BFE2DC325] PRIMARY KEY CLUSTERED 
(
	[CategoryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Customers]    Script Date: 10/20/2024 7:36:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customers](
	[CustomerID] [int] NOT NULL,
	[CustomerName] [nvarchar](100) NULL,
	[PhoneNumber] [nvarchar](20) NULL,
	[Email] [nvarchar](100) NULL,
	[Address] [nvarchar](200) NULL,
 CONSTRAINT [PK__Customer__A4AE64B860D26E3C] PRIMARY KEY CLUSTERED 
(
	[CustomerID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Employees]    Script Date: 10/20/2024 7:36:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employees](
	[EmployeeID] [int] NOT NULL,
	[EmployeeName] [nvarchar](100) NULL,
	[PhoneNumber] [nvarchar](20) NULL,
	[Email] [nvarchar](100) NULL,
	[Address] [nvarchar](200) NULL,
 CONSTRAINT [PK__Employee__7AD04FF1E09D7360] PRIMARY KEY CLUSTERED 
(
	[EmployeeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Invoices]    Script Date: 10/20/2024 7:36:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Invoices](
	[InvoiceID] [int] NOT NULL,
	[OrderID] [int] NULL,
	[InvoiceDate] [date] NULL,
	[TotalAmount] [decimal](10, 2) NULL,
 CONSTRAINT [PK__Invoices__D796AAD552C0C4DB] PRIMARY KEY CLUSTERED 
(
	[InvoiceID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderDetails]    Script Date: 10/20/2024 7:36:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderDetails](
	[OrderDetailID] [int] NOT NULL,
	[OrderID] [int] NULL,
	[ProductID] [int] NULL,
	[Quantity] [int] NULL,
	[UnitPrice] [decimal](10, 2) NULL,
	[TotalPrice]  AS ([Quantity]*[UnitPrice]) PERSISTED,
 CONSTRAINT [PK__OrderDet__D3B9D30CFB3905A6] PRIMARY KEY CLUSTERED 
(
	[OrderDetailID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Orders]    Script Date: 10/20/2024 7:36:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Orders](
	[OrderID] [int] NOT NULL,
	[OrderDate] [date] NULL,
	[CustomerID] [int] NULL,
	[EmployeeID] [int] NULL,
	[TotalAmount] [decimal](10, 2) NULL,
 CONSTRAINT [PK__Orders__C3905BAFD1A4155C] PRIMARY KEY CLUSTERED 
(
	[OrderID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Products]    Script Date: 10/20/2024 7:36:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
	[ProductID] [int] NOT NULL,
	[ProductName] [nvarchar](100) NULL,
	[UnitPrice] [decimal](10, 2) NULL,
	[StockQuantity] [int] NULL,
	[CategoryID] [int] NULL,
	[SupplierID] [int] NULL,
	[ProductImage] [nvarchar](255) NULL,
 CONSTRAINT [PK__Products__B40CC6ED2C29621D] PRIMARY KEY CLUSTERED 
(
	[ProductID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Revenue]    Script Date: 10/20/2024 7:36:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Revenue](
	[RevenueID] [int] NOT NULL,
	[InvoiceID] [int] NULL,
	[RevenueDate] [date] NULL,
	[RevenueAmount] [decimal](10, 2) NULL,
PRIMARY KEY CLUSTERED 
(
	[RevenueID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Suppliers]    Script Date: 10/20/2024 7:36:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Suppliers](
	[SupplierID] [int] NOT NULL,
	[SupplierName] [nvarchar](100) NULL,
	[ContactName] [nvarchar](100) NULL,
	[PhoneNumber] [nvarchar](20) NULL,
	[Email] [nvarchar](100) NULL,
	[Address] [nvarchar](200) NULL,
 CONSTRAINT [PK__Supplier__4BE6669447DB95E9] PRIMARY KEY CLUSTERED 
(
	[SupplierID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 10/20/2024 7:36:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[UserID] [int] NOT NULL,
	[Username] [nvarchar](50) NULL,
	[Password] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[Categories] ([CategoryID], [CategoryName]) VALUES (1, N'Điện thoại')
INSERT [dbo].[Categories] ([CategoryID], [CategoryName]) VALUES (2, N'Máy tính xách tay')
INSERT [dbo].[Categories] ([CategoryID], [CategoryName]) VALUES (3, N'Thời trang')
INSERT [dbo].[Categories] ([CategoryID], [CategoryName]) VALUES (4, N'Bàn phím')
INSERT [dbo].[Categories] ([CategoryID], [CategoryName]) VALUES (5, N'Đồ điện tử')
GO
INSERT [dbo].[Customers] ([CustomerID], [CustomerName], [PhoneNumber], [Email], [Address]) VALUES (1, N'Nguyễn Minh A', N'0908999123', N'customer1@example.com', N'123 Đường Hoa, TP.HCM')
INSERT [dbo].[Customers] ([CustomerID], [CustomerName], [PhoneNumber], [Email], [Address]) VALUES (2, N'Lê Anh K', N'0911222333', N'customer2@example.com', N'456 Đường Lý, Hà Nội')
INSERT [dbo].[Customers] ([CustomerID], [CustomerName], [PhoneNumber], [Email], [Address]) VALUES (4, N'Nguyễn Văn A', N'0944555666', N'customer4@example.com', N'321 Đường Nguyễn, Cần Thơ')
GO
INSERT [dbo].[Employees] ([EmployeeID], [EmployeeName], [PhoneNumber], [Email], [Address]) VALUES (1, N'Nguyễn Văn A', N'0903123456', N'anv@example.com', N'123 Đường ABC, TP.HCM')
INSERT [dbo].[Employees] ([EmployeeID], [EmployeeName], [PhoneNumber], [Email], [Address]) VALUES (2, N'Lê Thị B', N'0912765432', N'blt@example.com', N'456 Đường XYZ, Hà Nội')
INSERT [dbo].[Employees] ([EmployeeID], [EmployeeName], [PhoneNumber], [Email], [Address]) VALUES (3, N'Trần Văn A', N'0934345678', N'ctv@example.com', N'789 Đường KLM, Đà Nẵng')
INSERT [dbo].[Employees] ([EmployeeID], [EmployeeName], [PhoneNumber], [Email], [Address]) VALUES (4, N'a', N'23', N'a', N'sss')
INSERT [dbo].[Employees] ([EmployeeID], [EmployeeName], [PhoneNumber], [Email], [Address]) VALUES (5, N'Nhung', N'0938475847', N'nguyennhung@gmail.com', N'Thủ Đức')
GO
INSERT [dbo].[Invoices] ([InvoiceID], [OrderID], [InvoiceDate], [TotalAmount]) VALUES (1, 1, CAST(N'2024-10-06' AS Date), CAST(40000000.00 AS Decimal(10, 2)))
INSERT [dbo].[Invoices] ([InvoiceID], [OrderID], [InvoiceDate], [TotalAmount]) VALUES (2, 2, CAST(N'2024-10-07' AS Date), CAST(40000000.00 AS Decimal(10, 2)))
INSERT [dbo].[Invoices] ([InvoiceID], [OrderID], [InvoiceDate], [TotalAmount]) VALUES (3, 3, CAST(N'2024-10-08' AS Date), CAST(25000000.00 AS Decimal(10, 2)))
GO
INSERT [dbo].[OrderDetails] ([OrderDetailID], [OrderID], [ProductID], [Quantity], [UnitPrice]) VALUES (1, 1, 1, 2, CAST(20000000.00 AS Decimal(10, 2)))
INSERT [dbo].[OrderDetails] ([OrderDetailID], [OrderID], [ProductID], [Quantity], [UnitPrice]) VALUES (2, 2, 2, 1, CAST(40000000.00 AS Decimal(10, 2)))
INSERT [dbo].[OrderDetails] ([OrderDetailID], [OrderID], [ProductID], [Quantity], [UnitPrice]) VALUES (3, 3, 3, 5, CAST(5000000.00 AS Decimal(10, 2)))
GO
INSERT [dbo].[Orders] ([OrderID], [OrderDate], [CustomerID], [EmployeeID], [TotalAmount]) VALUES (1, CAST(N'2024-10-01' AS Date), 1, 1, CAST(5000000.00 AS Decimal(10, 2)))
INSERT [dbo].[Orders] ([OrderID], [OrderDate], [CustomerID], [EmployeeID], [TotalAmount]) VALUES (2, CAST(N'2024-10-02' AS Date), 2, 2, CAST(10000000.00 AS Decimal(10, 2)))
INSERT [dbo].[Orders] ([OrderID], [OrderDate], [CustomerID], [EmployeeID], [TotalAmount]) VALUES (3, CAST(N'2024-10-30' AS Date), 2, 2, CAST(5000000.00 AS Decimal(10, 2)))
INSERT [dbo].[Orders] ([OrderID], [OrderDate], [CustomerID], [EmployeeID], [TotalAmount]) VALUES (4, CAST(N'2025-11-27' AS Date), 2, 2, CAST(10000000.00 AS Decimal(10, 2)))
INSERT [dbo].[Orders] ([OrderID], [OrderDate], [CustomerID], [EmployeeID], [TotalAmount]) VALUES (5, CAST(N'2024-10-09' AS Date), 2, 1, CAST(2000000.00 AS Decimal(10, 2)))
GO
INSERT [dbo].[Products] ([ProductID], [ProductName], [UnitPrice], [StockQuantity], [CategoryID], [SupplierID], [ProductImage]) VALUES (1, N'iPhone 13', CAST(20000000.00 AS Decimal(10, 2)), 100, 1, 1, N'C:\Users\DRT\Downloads\Picture-20241018T014614Z-001\Picture\Iphone.jpg')
INSERT [dbo].[Products] ([ProductID], [ProductName], [UnitPrice], [StockQuantity], [CategoryID], [SupplierID], [ProductImage]) VALUES (2, N'MacBook Pro', CAST(40000000.00 AS Decimal(10, 2)), 50, 5, 1, N'C:/Users/DRT/Downloads/Picture-20241018T014614Z-001/Picture/Laptop.jpg')
INSERT [dbo].[Products] ([ProductID], [ProductName], [UnitPrice], [StockQuantity], [CategoryID], [SupplierID], [ProductImage]) VALUES (3, N'Máy giặt', CAST(5000000.00 AS Decimal(10, 2)), 200, 5, 1, N'C:/Users/DRT/Downloads/Picture-20241018T014614Z-001/Picture/Maygiat.png')
INSERT [dbo].[Products] ([ProductID], [ProductName], [UnitPrice], [StockQuantity], [CategoryID], [SupplierID], [ProductImage]) VALUES (4, N'Giày thể thao', CAST(1000000.00 AS Decimal(10, 2)), 150, 3, 2, N'C:/Users/DRT/Downloads/Picture-20241018T014614Z-001/Picture/Giày.jpg')
INSERT [dbo].[Products] ([ProductID], [ProductName], [UnitPrice], [StockQuantity], [CategoryID], [SupplierID], [ProductImage]) VALUES (5, N'Tivi', CAST(50000000.00 AS Decimal(10, 2)), 30, 5, 3, N'C:/Users/DRT/Downloads/Picture-20241018T014614Z-001/Picture/TV.png')
INSERT [dbo].[Products] ([ProductID], [ProductName], [UnitPrice], [StockQuantity], [CategoryID], [SupplierID], [ProductImage]) VALUES (6, N'Bàn Phím', CAST(120000.00 AS Decimal(10, 2)), 100, 1, 1, N'C:\Users\DRT\Downloads\Picture-20241018T014614Z-001\Picture\Bàn phím Customer.png')
INSERT [dbo].[Products] ([ProductID], [ProductName], [UnitPrice], [StockQuantity], [CategoryID], [SupplierID], [ProductImage]) VALUES (7, N'Nón Lá', CAST(65000.00 AS Decimal(10, 2)), 200, 3, 4, N'C:\Users\DRT\Downloads\Picture-20241018T014614Z-001\Picture\Nón lá.png')
GO
INSERT [dbo].[Revenue] ([RevenueID], [InvoiceID], [RevenueDate], [RevenueAmount]) VALUES (1, 1, CAST(N'2024-10-11' AS Date), CAST(40000000.00 AS Decimal(10, 2)))
INSERT [dbo].[Revenue] ([RevenueID], [InvoiceID], [RevenueDate], [RevenueAmount]) VALUES (2, 2, CAST(N'2024-10-12' AS Date), CAST(40000000.00 AS Decimal(10, 2)))
INSERT [dbo].[Revenue] ([RevenueID], [InvoiceID], [RevenueDate], [RevenueAmount]) VALUES (3, 3, CAST(N'2024-10-13' AS Date), CAST(2500000.00 AS Decimal(10, 2)))
GO
INSERT [dbo].[Suppliers] ([SupplierID], [SupplierName], [ContactName], [PhoneNumber], [Email], [Address]) VALUES (1, N'Công ty ABC', N'Nguyễn Văn A', N'0909123456', N'a@example.com', N'123 Đường ABC, TP.HCM')
INSERT [dbo].[Suppliers] ([SupplierID], [SupplierName], [ContactName], [PhoneNumber], [Email], [Address]) VALUES (2, N'Công ty XYZ', N'Lê Thị B', N'0909765432', N'b@example.com', N'456 Đường XYZ, Hà Nội')
INSERT [dbo].[Suppliers] ([SupplierID], [SupplierName], [ContactName], [PhoneNumber], [Email], [Address]) VALUES (3, N'Công ty KLM', N'Trần Văn C', N'0912345678', N'c@example.com', N'789 Đường KLM, Đà Nẵng')
INSERT [dbo].[Suppliers] ([SupplierID], [SupplierName], [ContactName], [PhoneNumber], [Email], [Address]) VALUES (4, N'Công ty EFG', N'Phạm Thị D', N'0919876543', N'd@example.com', N'321 Đường EFG, Cần Thơ')
INSERT [dbo].[Suppliers] ([SupplierID], [SupplierName], [ContactName], [PhoneNumber], [Email], [Address]) VALUES (5, N'Công ty UVW', N'Hoàng Văn E', N'0911234567', N'e@example.com', N'654 Đường UVW, Hải Phòng')
GO
INSERT [dbo].[Users] ([UserID], [Username], [Password]) VALUES (1, N'admin', N'123456')
INSERT [dbo].[Users] ([UserID], [Username], [Password]) VALUES (2, N'user1', N'abcdef')
INSERT [dbo].[Users] ([UserID], [Username], [Password]) VALUES (3, N'user2', N'password123')
INSERT [dbo].[Users] ([UserID], [Username], [Password]) VALUES (4, N'user3', N'12345678')
INSERT [dbo].[Users] ([UserID], [Username], [Password]) VALUES (5, N'user4', N'qwerty')
GO
ALTER TABLE [dbo].[Invoices]  WITH CHECK ADD  CONSTRAINT [FK__Invoices__OrderI__4AB81AF0] FOREIGN KEY([OrderID])
REFERENCES [dbo].[Orders] ([OrderID])
GO
ALTER TABLE [dbo].[Invoices] CHECK CONSTRAINT [FK__Invoices__OrderI__4AB81AF0]
GO
ALTER TABLE [dbo].[OrderDetails]  WITH CHECK ADD  CONSTRAINT [FK__OrderDeta__Order__46E78A0C] FOREIGN KEY([OrderID])
REFERENCES [dbo].[Orders] ([OrderID])
GO
ALTER TABLE [dbo].[OrderDetails] CHECK CONSTRAINT [FK__OrderDeta__Order__46E78A0C]
GO
ALTER TABLE [dbo].[OrderDetails]  WITH CHECK ADD  CONSTRAINT [FK__OrderDeta__Produ__47DBAE45] FOREIGN KEY([ProductID])
REFERENCES [dbo].[Products] ([ProductID])
GO
ALTER TABLE [dbo].[OrderDetails] CHECK CONSTRAINT [FK__OrderDeta__Produ__47DBAE45]
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [FK__Orders__Customer__4316F928] FOREIGN KEY([CustomerID])
REFERENCES [dbo].[Customers] ([CustomerID])
GO
ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [FK__Orders__Customer__4316F928]
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [FK__Orders__Employee__440B1D61] FOREIGN KEY([EmployeeID])
REFERENCES [dbo].[Employees] ([EmployeeID])
GO
ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [FK__Orders__Employee__440B1D61]
GO
ALTER TABLE [dbo].[Products]  WITH CHECK ADD  CONSTRAINT [FK__Products__Catego__3B75D760] FOREIGN KEY([CategoryID])
REFERENCES [dbo].[Categories] ([CategoryID])
GO
ALTER TABLE [dbo].[Products] CHECK CONSTRAINT [FK__Products__Catego__3B75D760]
GO
ALTER TABLE [dbo].[Products]  WITH CHECK ADD  CONSTRAINT [FK__Products__Suppli__3C69FB99] FOREIGN KEY([SupplierID])
REFERENCES [dbo].[Suppliers] ([SupplierID])
GO
ALTER TABLE [dbo].[Products] CHECK CONSTRAINT [FK__Products__Suppli__3C69FB99]
GO
ALTER TABLE [dbo].[Revenue]  WITH CHECK ADD  CONSTRAINT [FK__Revenue__Invoice__4D94879B] FOREIGN KEY([InvoiceID])
REFERENCES [dbo].[Invoices] ([InvoiceID])
GO
ALTER TABLE [dbo].[Revenue] CHECK CONSTRAINT [FK__Revenue__Invoice__4D94879B]
GO
USE [master]
GO
ALTER DATABASE [demo] SET  READ_WRITE 
GO
