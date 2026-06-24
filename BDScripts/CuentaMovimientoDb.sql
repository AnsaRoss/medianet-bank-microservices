USE [master]
GO
/****** Object:  Database [CuentaMovimientoDb]    Script Date: 24/6/2026 15:12:47 ******/
CREATE DATABASE [CuentaMovimientoDb]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'CuentaMovimientoDb', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\CuentaMovimientoDb.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'CuentaMovimientoDb_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\CuentaMovimientoDb_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [CuentaMovimientoDb] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [CuentaMovimientoDb].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [CuentaMovimientoDb] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [CuentaMovimientoDb] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [CuentaMovimientoDb] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [CuentaMovimientoDb] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [CuentaMovimientoDb] SET ARITHABORT OFF 
GO
ALTER DATABASE [CuentaMovimientoDb] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [CuentaMovimientoDb] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [CuentaMovimientoDb] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [CuentaMovimientoDb] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [CuentaMovimientoDb] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [CuentaMovimientoDb] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [CuentaMovimientoDb] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [CuentaMovimientoDb] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [CuentaMovimientoDb] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [CuentaMovimientoDb] SET  ENABLE_BROKER 
GO
ALTER DATABASE [CuentaMovimientoDb] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [CuentaMovimientoDb] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [CuentaMovimientoDb] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [CuentaMovimientoDb] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [CuentaMovimientoDb] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [CuentaMovimientoDb] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [CuentaMovimientoDb] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [CuentaMovimientoDb] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [CuentaMovimientoDb] SET  MULTI_USER 
GO
ALTER DATABASE [CuentaMovimientoDb] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [CuentaMovimientoDb] SET DB_CHAINING OFF 
GO
ALTER DATABASE [CuentaMovimientoDb] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [CuentaMovimientoDb] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [CuentaMovimientoDb] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [CuentaMovimientoDb] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [CuentaMovimientoDb] SET QUERY_STORE = ON
GO
ALTER DATABASE [CuentaMovimientoDb] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [CuentaMovimientoDb]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 24/6/2026 15:12:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Cuentas]    Script Date: 24/6/2026 15:12:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cuentas](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[NumeroCuenta] [nvarchar](450) NOT NULL,
	[TipoCuenta] [nvarchar](max) NOT NULL,
	[SaldoInicial] [decimal](18, 2) NOT NULL,
	[SaldoActual] [decimal](18, 2) NOT NULL,
	[Estado] [bit] NOT NULL,
	[ClienteId] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Cuentas] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Movimientos]    Script Date: 24/6/2026 15:12:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Movimientos](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Fecha] [datetime2](7) NOT NULL,
	[TipoMovimiento] [nvarchar](max) NOT NULL,
	[Valor] [decimal](18, 2) NOT NULL,
	[Saldo] [decimal](18, 2) NOT NULL,
	[CuentaId] [int] NOT NULL,
 CONSTRAINT [PK_Movimientos] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20260623205231_InitialCreateCuentaMovimiento', N'6.0.36')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20260624170428_CambiarClienteIdAString', N'6.0.36')
GO
SET IDENTITY_INSERT [dbo].[Cuentas] ON 

INSERT [dbo].[Cuentas] ([Id], [NumeroCuenta], [TipoCuenta], [SaldoInicial], [SaldoActual], [Estado], [ClienteId]) VALUES (1, N'00123456789', N'AHORROS', CAST(1000.00 AS Decimal(18, 2)), CAST(1600.00 AS Decimal(18, 2)), 1, N'CLI0001')
INSERT [dbo].[Cuentas] ([Id], [NumeroCuenta], [TipoCuenta], [SaldoInicial], [SaldoActual], [Estado], [ClienteId]) VALUES (2, N'00123456780', N'AHORROS', CAST(2000.00 AS Decimal(18, 2)), CAST(1400.00 AS Decimal(18, 2)), 1, N'CLI0002')
INSERT [dbo].[Cuentas] ([Id], [NumeroCuenta], [TipoCuenta], [SaldoInicial], [SaldoActual], [Estado], [ClienteId]) VALUES (4, N'00123456781', N'AHORROS', CAST(100.00 AS Decimal(18, 2)), CAST(100.00 AS Decimal(18, 2)), 0, N'CLI0003')
INSERT [dbo].[Cuentas] ([Id], [NumeroCuenta], [TipoCuenta], [SaldoInicial], [SaldoActual], [Estado], [ClienteId]) VALUES (7, N'00123456783', N'AHORROS', CAST(100.00 AS Decimal(18, 2)), CAST(500.00 AS Decimal(18, 2)), 1, N'CLI0006')
INSERT [dbo].[Cuentas] ([Id], [NumeroCuenta], [TipoCuenta], [SaldoInicial], [SaldoActual], [Estado], [ClienteId]) VALUES (10, N'00123456784', N'AHORROS', CAST(100.00 AS Decimal(18, 2)), CAST(100.00 AS Decimal(18, 2)), 1, N'CLI0006')
SET IDENTITY_INSERT [dbo].[Cuentas] OFF
GO
SET IDENTITY_INSERT [dbo].[Movimientos] ON 

INSERT [dbo].[Movimientos] ([Id], [Fecha], [TipoMovimiento], [Valor], [Saldo], [CuentaId]) VALUES (1, CAST(N'2026-06-23T21:19:25.1270000' AS DateTime2), N'Deposito', CAST(600.00 AS Decimal(18, 2)), CAST(1600.00 AS Decimal(18, 2)), 1)
INSERT [dbo].[Movimientos] ([Id], [Fecha], [TipoMovimiento], [Valor], [Saldo], [CuentaId]) VALUES (2, CAST(N'2026-06-23T21:19:25.1270000' AS DateTime2), N'Retiro', CAST(-600.00 AS Decimal(18, 2)), CAST(1400.00 AS Decimal(18, 2)), 2)
INSERT [dbo].[Movimientos] ([Id], [Fecha], [TipoMovimiento], [Valor], [Saldo], [CuentaId]) VALUES (3, CAST(N'2026-06-24T13:53:39.6008538' AS DateTime2), N'Deposito', CAST(40.00 AS Decimal(18, 2)), CAST(1440.00 AS Decimal(18, 2)), 2)
INSERT [dbo].[Movimientos] ([Id], [Fecha], [TipoMovimiento], [Valor], [Saldo], [CuentaId]) VALUES (4, CAST(N'2026-06-24T13:55:44.1351787' AS DateTime2), N'Reverso', CAST(-40.00 AS Decimal(18, 2)), CAST(1400.00 AS Decimal(18, 2)), 2)
INSERT [dbo].[Movimientos] ([Id], [Fecha], [TipoMovimiento], [Valor], [Saldo], [CuentaId]) VALUES (5, CAST(N'2026-06-24T14:35:18.9647737' AS DateTime2), N'Deposito', CAST(400.00 AS Decimal(18, 2)), CAST(500.00 AS Decimal(18, 2)), 7)
SET IDENTITY_INSERT [dbo].[Movimientos] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Cuentas_NumeroCuenta]    Script Date: 24/6/2026 15:12:47 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Cuentas_NumeroCuenta] ON [dbo].[Cuentas]
(
	[NumeroCuenta] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Movimientos_CuentaId]    Script Date: 24/6/2026 15:12:47 ******/
CREATE NONCLUSTERED INDEX [IX_Movimientos_CuentaId] ON [dbo].[Movimientos]
(
	[CuentaId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Movimientos]  WITH CHECK ADD  CONSTRAINT [FK_Movimientos_Cuentas_CuentaId] FOREIGN KEY([CuentaId])
REFERENCES [dbo].[Cuentas] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Movimientos] CHECK CONSTRAINT [FK_Movimientos_Cuentas_CuentaId]
GO
USE [master]
GO
ALTER DATABASE [CuentaMovimientoDb] SET  READ_WRITE 
GO
