USE [master]
GO
/****** Object:  Database [CodorniX]    Script Date: 29/05/2017 13:06:28 ******/
CREATE DATABASE [CodorniX]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'CodorniX', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL13.SQLEXPRESS\MSSQL\DATA\CodorniX.mdf' , SIZE = 131072KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'CodorniX_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL13.SQLEXPRESS\MSSQL\DATA\CodorniX_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [CodorniX] SET COMPATIBILITY_LEVEL = 130
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [CodorniX].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [CodorniX] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [CodorniX] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [CodorniX] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [CodorniX] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [CodorniX] SET ARITHABORT OFF 
GO
ALTER DATABASE [CodorniX] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [CodorniX] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [CodorniX] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [CodorniX] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [CodorniX] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [CodorniX] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [CodorniX] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [CodorniX] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [CodorniX] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [CodorniX] SET  DISABLE_BROKER 
GO
ALTER DATABASE [CodorniX] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [CodorniX] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [CodorniX] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [CodorniX] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [CodorniX] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [CodorniX] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [CodorniX] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [CodorniX] SET RECOVERY FULL 
GO
ALTER DATABASE [CodorniX] SET  MULTI_USER 
GO
ALTER DATABASE [CodorniX] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [CodorniX] SET DB_CHAINING OFF 
GO
ALTER DATABASE [CodorniX] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [CodorniX] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [CodorniX] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [CodorniX] SET QUERY_STORE = OFF
GO
USE [CodorniX]
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET MAXDOP = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET LEGACY_CARDINALITY_ESTIMATION = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET PARAMETER_SNIFFING = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET QUERY_OPTIMIZER_HOTFIXES = PRIMARY;
GO
USE [CodorniX]
GO
/****** Object:  UserDefinedFunction [dbo].[CSVtoTable]    Script Date: 29/05/2017 13:06:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:        <Kumar Pankaj Verma>
-- Create date: <05-Apr-2011>
-- Description:    <Convert CSV to Table>
-- =============================================
CREATE FUNCTION [dbo].[CSVtoTable]
(
    @LIST varchar(7000),
    @Delimeter varchar(10)
)
RETURNS @RET1 TABLE (RESULT varchar(100))
AS
BEGIN
    DECLARE @RET TABLE(RESULT varchar(100))
    
    IF LTRIM(RTRIM(@LIST))='' RETURN  

    DECLARE @START BIGINT
    DECLARE @LASTSTART BIGINT
    SET @LASTSTART=0
    SET @START=CHARINDEX(@Delimeter,@LIST,0)

    IF @START=0
    INSERT INTO @RET VALUES(SUBSTRING(@LIST,0,LEN(@LIST)+1))

    WHILE(@START >0)
    BEGIN
        INSERT INTO @RET VALUES(SUBSTRING(@LIST,@LASTSTART,@START-@LASTSTART))
        SET @LASTSTART=@START+1
        SET @START=CHARINDEX(@Delimeter,@LIST,@START+1)
        IF(@START=0)
        INSERT INTO @RET VALUES(SUBSTRING(@LIST,@LASTSTART,LEN(@LIST)+1))
    END
    
    INSERT INTO @RET1 SELECT * FROM @RET
    RETURN 
END


GO
/****** Object:  Table [dbo].[AppUser]    Script Date: 29/05/2017 13:06:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AppUser](
	[uid] [uniqueidentifier] NOT NULL,
	[username] [nvarchar](50) NOT NULL,
	[firstname] [nvarchar](50) NOT NULL,
	[password] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[uid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Area]    Script Date: 29/05/2017 13:06:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Area](
	[UidArea] [uniqueidentifier] NOT NULL,
	[VchNombre] [nvarchar](50) NOT NULL,
	[UidDepartamento] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_Area] PRIMARY KEY CLUSTERED 
(
	[UidArea] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Departamento]    Script Date: 29/05/2017 13:06:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Departamento](
	[UidDepartamento] [uniqueidentifier] NOT NULL,
	[VchNombre] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Departamento] PRIMARY KEY CLUSTERED 
(
	[UidDepartamento] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Direccion]    Script Date: 29/05/2017 13:06:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Direccion](
	[UidDireccion] [uniqueidentifier] NOT NULL,
	[UidPais] [uniqueidentifier] NOT NULL,
	[UidEstado] [uniqueidentifier] NOT NULL,
	[VchMunicipio] [nvarchar](30) NOT NULL,
	[VchCiudad] [nvarchar](30) NOT NULL,
	[VchColonia] [nvarchar](20) NOT NULL,
	[VchCalle] [nvarchar](20) NOT NULL,
	[VchConCalle] [nvarchar](20) NOT NULL,
	[VchYCalle] [nvarchar](20) NULL,
	[VchNoExt] [nvarchar](20) NOT NULL,
	[VchNoInt] [nvarchar](20) NULL,
	[VchReferencia] [nvarchar](200) NULL,
 CONSTRAINT [PK_Direccion] PRIMARY KEY CLUSTERED 
(
	[UidDireccion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Empresa]    Script Date: 29/05/2017 13:06:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Empresa](
	[UidEmpresa] [uniqueidentifier] NOT NULL,
	[VchNombreComercial] [nvarchar](50) NOT NULL,
	[VchRazonSocial] [nvarchar](60) NOT NULL,
	[VchGiro] [nvarchar](40) NOT NULL,
	[ChRFC] [nchar](13) NOT NULL,
	[DtFechaRegistro] [date] NOT NULL,
 CONSTRAINT [PK_Empresa] PRIMARY KEY CLUSTERED 
(
	[UidEmpresa] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[EmpresaDireccion]    Script Date: 29/05/2017 13:06:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmpresaDireccion](
	[UidEmpresa] [uniqueidentifier] NOT NULL,
	[UidDireccion] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_EmpresaDireccion] PRIMARY KEY CLUSTERED 
(
	[UidEmpresa] ASC,
	[UidDireccion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[EmpresaTelefono]    Script Date: 29/05/2017 13:06:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmpresaTelefono](
	[UidEmpresa] [uniqueidentifier] NOT NULL,
	[UidTelefono] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_EmpresaTelefono] PRIMARY KEY CLUSTERED 
(
	[UidEmpresa] ASC,
	[UidTelefono] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Estado]    Script Date: 29/05/2017 13:06:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Estado](
	[UidEstado] [uniqueidentifier] NOT NULL,
	[UidPais] [uniqueidentifier] NOT NULL,
	[VchNombre] [nvarchar](20) NOT NULL,
 CONSTRAINT [PK_Estado] PRIMARY KEY CLUSTERED 
(
	[UidEstado] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Estatus]    Script Date: 29/05/2017 13:06:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Estatus](
	[SidStatus] [uniqueidentifier] NOT NULL,
	[VchStatus] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Estatus] PRIMARY KEY CLUSTERED 
(
	[SidStatus] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Modulo]    Script Date: 29/05/2017 13:06:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Modulo](
	[MidModulo] [uniqueidentifier] NOT NULL,
	[VchModulo] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Modulo] PRIMARY KEY CLUSTERED 
(
	[MidModulo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Pais]    Script Date: 29/05/2017 13:06:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pais](
	[UidPais] [uniqueidentifier] NOT NULL,
	[VchNombre] [nvarchar](20) NOT NULL,
 CONSTRAINT [PK_Pais] PRIMARY KEY CLUSTERED 
(
	[UidPais] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Perfil]    Script Date: 29/05/2017 13:06:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Perfil](
	[PidPerfil] [uniqueidentifier] NOT NULL,
	[VchPerfil] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Perfil] PRIMARY KEY CLUSTERED 
(
	[PidPerfil] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Permisos]    Script Date: 29/05/2017 13:06:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Permisos](
	[PidPermiso] [uniqueidentifier] NOT NULL,
	[PidPerfil] [uniqueidentifier] NOT NULL,
	[MidModulo] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_Permisos] PRIMARY KEY CLUSTERED 
(
	[PidPermiso] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Sucursal]    Script Date: 29/05/2017 13:06:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sucursal](
	[UidSucursal] [uniqueidentifier] NOT NULL,
	[VchNombre] [nvarchar](50) NOT NULL,
	[DtFechaRegistro] [date] NOT NULL,
	[UidEmpresa] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_Sucursal] PRIMARY KEY CLUSTERED 
(
	[UidSucursal] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SucursalDireccion]    Script Date: 29/05/2017 13:06:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SucursalDireccion](
	[UidSucursal] [uniqueidentifier] NOT NULL,
	[UidDireccion] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_SucursalDireccion] PRIMARY KEY CLUSTERED 
(
	[UidSucursal] ASC,
	[UidDireccion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SucursalTelefono]    Script Date: 29/05/2017 13:06:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SucursalTelefono](
	[UidSucursal] [uniqueidentifier] NOT NULL,
	[UidTelefono] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_SucursalTelefono] PRIMARY KEY CLUSTERED 
(
	[UidSucursal] ASC,
	[UidTelefono] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Telefono]    Script Date: 29/05/2017 13:06:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Telefono](
	[UidTelefono] [uniqueidentifier] NOT NULL,
	[VchTelefono] [nvarchar](20) NOT NULL,
 CONSTRAINT [PK_Telefono] PRIMARY KEY CLUSTERED 
(
	[UidTelefono] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Usuario]    Script Date: 29/05/2017 13:06:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuario](
	[UidUsuario] [uniqueidentifier] NOT NULL,
	[VchNombre] [nvarchar](50) NOT NULL,
	[VchApellidoPaterno] [nvarchar](50) NOT NULL,
	[VchApellidoMaterno] [nvarchar](50) NOT NULL,
	[DtFechaDeNacimiento] [date] NOT NULL,
	[VchCorreo] [nvarchar](50) NULL,
	[VchCurp] [nvarchar](50) NOT NULL,
	[VchTelefono] [nvarchar](50) NULL,
	[DtFechaDeInicio] [date] NOT NULL,
	[DtFechaDeSalida] [date] NULL,
	[VchUsuario] [nvarchar](50) NOT NULL,
	[VchPassword] [nvarchar](50) NOT NULL,
	[PidPerfil] [uniqueidentifier] NULL,
	[SidStatus] [uniqueidentifier] NULL,
 CONSTRAINT [PK_Usuario] PRIMARY KEY CLUSTERED 
(
	[UidUsuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[AppUser] ADD  CONSTRAINT [DF_Users_uid]  DEFAULT (newid()) FOR [uid]
GO
ALTER TABLE [dbo].[Estado] ADD  CONSTRAINT [DF_Estado_UidEstado]  DEFAULT (newid()) FOR [UidEstado]
GO
ALTER TABLE [dbo].[Estatus] ADD  CONSTRAINT [DF_Estatus_SidStatus]  DEFAULT (newid()) FOR [SidStatus]
GO
ALTER TABLE [dbo].[Modulo] ADD  CONSTRAINT [DF_Modulo_MidModulo]  DEFAULT (newid()) FOR [MidModulo]
GO
ALTER TABLE [dbo].[Pais] ADD  CONSTRAINT [DF_Pais_UidPais]  DEFAULT (newid()) FOR [UidPais]
GO
ALTER TABLE [dbo].[Perfil] ADD  CONSTRAINT [DF_Perfil_PidPerfil]  DEFAULT (newid()) FOR [PidPerfil]
GO
ALTER TABLE [dbo].[Permisos] ADD  CONSTRAINT [DF_Permisos_PidPermiso]  DEFAULT (newid()) FOR [PidPermiso]
GO
ALTER TABLE [dbo].[Usuario] ADD  CONSTRAINT [DF_Usuario_UidUsuario]  DEFAULT (newid()) FOR [UidUsuario]
GO
ALTER TABLE [dbo].[Area]  WITH CHECK ADD  CONSTRAINT [FK_Area_Departamento] FOREIGN KEY([UidDepartamento])
REFERENCES [dbo].[Departamento] ([UidDepartamento])
GO
ALTER TABLE [dbo].[Area] CHECK CONSTRAINT [FK_Area_Departamento]
GO
ALTER TABLE [dbo].[Direccion]  WITH CHECK ADD  CONSTRAINT [FK_Direccion_Estado] FOREIGN KEY([UidEstado])
REFERENCES [dbo].[Estado] ([UidEstado])
GO
ALTER TABLE [dbo].[Direccion] CHECK CONSTRAINT [FK_Direccion_Estado]
GO
ALTER TABLE [dbo].[Direccion]  WITH CHECK ADD  CONSTRAINT [FK_Direccion_Pais] FOREIGN KEY([UidPais])
REFERENCES [dbo].[Pais] ([UidPais])
GO
ALTER TABLE [dbo].[Direccion] CHECK CONSTRAINT [FK_Direccion_Pais]
GO
ALTER TABLE [dbo].[EmpresaDireccion]  WITH CHECK ADD  CONSTRAINT [FK_EmpresaDireccion_Direccion] FOREIGN KEY([UidDireccion])
REFERENCES [dbo].[Direccion] ([UidDireccion])
GO
ALTER TABLE [dbo].[EmpresaDireccion] CHECK CONSTRAINT [FK_EmpresaDireccion_Direccion]
GO
ALTER TABLE [dbo].[EmpresaDireccion]  WITH CHECK ADD  CONSTRAINT [FK_EmpresaDireccion_Empresa] FOREIGN KEY([UidEmpresa])
REFERENCES [dbo].[Empresa] ([UidEmpresa])
GO
ALTER TABLE [dbo].[EmpresaDireccion] CHECK CONSTRAINT [FK_EmpresaDireccion_Empresa]
GO
ALTER TABLE [dbo].[EmpresaTelefono]  WITH CHECK ADD  CONSTRAINT [FK_EmpresaTelefono_Empresa] FOREIGN KEY([UidEmpresa])
REFERENCES [dbo].[Empresa] ([UidEmpresa])
GO
ALTER TABLE [dbo].[EmpresaTelefono] CHECK CONSTRAINT [FK_EmpresaTelefono_Empresa]
GO
ALTER TABLE [dbo].[EmpresaTelefono]  WITH CHECK ADD  CONSTRAINT [FK_EmpresaTelefono_Telefono] FOREIGN KEY([UidTelefono])
REFERENCES [dbo].[Telefono] ([UidTelefono])
GO
ALTER TABLE [dbo].[EmpresaTelefono] CHECK CONSTRAINT [FK_EmpresaTelefono_Telefono]
GO
ALTER TABLE [dbo].[Estado]  WITH CHECK ADD  CONSTRAINT [FK_Estado_Pais] FOREIGN KEY([UidPais])
REFERENCES [dbo].[Pais] ([UidPais])
GO
ALTER TABLE [dbo].[Estado] CHECK CONSTRAINT [FK_Estado_Pais]
GO
ALTER TABLE [dbo].[Permisos]  WITH CHECK ADD  CONSTRAINT [FK_Permisos_Modulo] FOREIGN KEY([MidModulo])
REFERENCES [dbo].[Modulo] ([MidModulo])
GO
ALTER TABLE [dbo].[Permisos] CHECK CONSTRAINT [FK_Permisos_Modulo]
GO
ALTER TABLE [dbo].[Permisos]  WITH CHECK ADD  CONSTRAINT [FK_Permisos_Perfil] FOREIGN KEY([PidPerfil])
REFERENCES [dbo].[Perfil] ([PidPerfil])
GO
ALTER TABLE [dbo].[Permisos] CHECK CONSTRAINT [FK_Permisos_Perfil]
GO
ALTER TABLE [dbo].[Sucursal]  WITH CHECK ADD  CONSTRAINT [FK_Sucursal_Empresa] FOREIGN KEY([UidEmpresa])
REFERENCES [dbo].[Empresa] ([UidEmpresa])
GO
ALTER TABLE [dbo].[Sucursal] CHECK CONSTRAINT [FK_Sucursal_Empresa]
GO
ALTER TABLE [dbo].[SucursalDireccion]  WITH CHECK ADD  CONSTRAINT [FK_SucursalDireccion_Direccion] FOREIGN KEY([UidDireccion])
REFERENCES [dbo].[Direccion] ([UidDireccion])
GO
ALTER TABLE [dbo].[SucursalDireccion] CHECK CONSTRAINT [FK_SucursalDireccion_Direccion]
GO
ALTER TABLE [dbo].[SucursalDireccion]  WITH CHECK ADD  CONSTRAINT [FK_SucursalDireccion_Sucursal] FOREIGN KEY([UidSucursal])
REFERENCES [dbo].[Sucursal] ([UidSucursal])
GO
ALTER TABLE [dbo].[SucursalDireccion] CHECK CONSTRAINT [FK_SucursalDireccion_Sucursal]
GO
ALTER TABLE [dbo].[SucursalTelefono]  WITH CHECK ADD  CONSTRAINT [FK_SucursalTelefono_Sucursal] FOREIGN KEY([UidSucursal])
REFERENCES [dbo].[Sucursal] ([UidSucursal])
GO
ALTER TABLE [dbo].[SucursalTelefono] CHECK CONSTRAINT [FK_SucursalTelefono_Sucursal]
GO
ALTER TABLE [dbo].[SucursalTelefono]  WITH CHECK ADD  CONSTRAINT [FK_SucursalTelefono_Telefono] FOREIGN KEY([UidTelefono])
REFERENCES [dbo].[Telefono] ([UidTelefono])
GO
ALTER TABLE [dbo].[SucursalTelefono] CHECK CONSTRAINT [FK_SucursalTelefono_Telefono]
GO
ALTER TABLE [dbo].[Usuario]  WITH CHECK ADD  CONSTRAINT [FK_Usuario_Estatus] FOREIGN KEY([SidStatus])
REFERENCES [dbo].[Estatus] ([SidStatus])
GO
ALTER TABLE [dbo].[Usuario] CHECK CONSTRAINT [FK_Usuario_Estatus]
GO
ALTER TABLE [dbo].[Usuario]  WITH CHECK ADD  CONSTRAINT [FK_Usuario_Perfil] FOREIGN KEY([PidPerfil])
REFERENCES [dbo].[Perfil] ([PidPerfil])
GO
ALTER TABLE [dbo].[Usuario] CHECK CONSTRAINT [FK_Usuario_Perfil]
GO
/****** Object:  StoredProcedure [dbo].[sp_BuscarUsuario]    Script Date: 29/05/2017 13:06:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Aremy Daniela De Leon Tercero
-- Create date: 2017-05-18
-- Description:	Busqueda de Usuario
-- =============================================
CREATE PROCEDURE [dbo].[sp_BuscarUsuario] 
	-- Add the parameters for the stored procedure here
	@VchNombre nvarchar(50) = null,
	@VchApellidoPaterno nvarchar(50) = null,
	@VchApellidoMaterno nvarchar(50) = null,
	@DtFechaDeNacimiento nvarchar(50) = null,
	@VchCorreo nvarchar(50) = null,
	@VchCurp nvarchar(50) = null,
	@VchTelefono nvarchar(50) = null,
	@DtFechaDeInicio nvarchar(50) = null,
	@DtFechaDeSalida nvarchar(50) = null,
	@VchUsuario nvarchar(50)= null,
	@VchPerfil nvarchar(50)= null,
	@VchStatus nvarchar(50)= null
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	select * from Usuario AS u INNER JOIN Perfil AS p ON u.PidPerfil=p.PidPerfil 
	INNER JOIN Estatus AS es ON u.SidStatus=es.SidStatus
	where
	((@VchNombre is not null and  u.VchNombre like'%' + @VchNombre+ '%')OR (@VchNombre is null))
	 and ((@VchApellidoPaterno is not null and u.VchApellidoPaterno like '%' +  @VchApellidoPaterno+'%')OR(@VchApellidoPaterno is null))
	 and ((@VchApellidoMaterno is not null and u.VchApellidoMaterno like '%' +  @VchApellidoMaterno+'%')OR(@VchApellidoMaterno is null))
	 and ((@DtFechaDeNacimiento is not null and u.DtFechaDeNacimiento like '%' +  @DtFechaDeNacimiento+'%')OR(@DtFechaDeNacimiento is null))
	 and ((@VchCorreo is not null and u.VchCorreo like '%' +  @VchCorreo+'%')OR(@VchCorreo is null))
	 and ((@VchCurp is not null and u.VchCurp like '%' +  @VchCurp+'%')OR(@VchCurp is null))
	 and ((@VchTelefono is not null and u.VchTelefono like '%' +  @VchTelefono+'%')OR(@VchTelefono is null))
	 and ((@DtFechaDeInicio is not null and u.DtFechaDeInicio  like '%' + @DtFechaDeInicio+'%')OR (@DtFechaDeInicio <= u.DtFechaDeInicio)
	 OR(@DtFechaDeInicio is null))
	 and ((@DtFechaDeSalida is not null and u.DtFechaDeSalida like '%' +  @DtFechaDeSalida+'%')OR (@DtFechaDeSalida >= u.DtFechaDeSalida)
	 OR(@DtFechaDeSalida is null))
	 
	 OR ((@DtFechaDeInicio >= u.DtFechaDeInicio) and (@DtFechaDeSalida <= u.DtFechaDeSalida))
	 and ((@VchUsuario is not null and u.VchUsuario like '%' +  @VchUsuario+'%')OR(@VchUsuario is null))
	 and ((@VchPerfil is not null and p.VchPerfil like '%' +  @VchPerfil+'%')OR(@VchPerfil is null))
	 and ((@VchStatus is not null and es.VchStatus like '%' +  @VchStatus+'%')OR(@VchStatus is null))
	 
	 
END


GO
/****** Object:  StoredProcedure [dbo].[sp_ConsultarUSuario]    Script Date: 29/05/2017 13:06:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_ConsultarUSuario]
	-- Add the parameters for the stored procedure here
	@VchUsuario nvarchar(50),
	@VchPassword nvarchar(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	select u.UidUsuario  from Usuario u where
	 u.VchNombre = VchUsuario
	 and u.VchPassword = VchPassword
END


GO
/****** Object:  StoredProcedure [dbo].[sp_InsertarUsuario]    Script Date: 29/05/2017 13:06:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Aremy Daniela De Leon Tercero
-- Create date: 2017-05-17
-- Description:	Insersion de usuarios
-- =============================================
CREATE PROCEDURE [dbo].[sp_InsertarUsuario]
	-- Add the parameters for the stored procedure here
	@VchNombre nvarchar(50),
	@VchApellidoPaterno nvarchar(50),
	@VchApellidoMaterno nvarchar(50),
	@DtFechaDeNacimiento date,
	@VchCorreo nvarchar(50),
	@VchCurp nvarchar(50),
	@VchTelefono nvarchar(50),
	@DtFechaDeInicio nvarchar(50),
	@DtFechaDeSalida nvarchar(50),
	@VchUsuario nvarchar(50),
	@VchPassword nvarchar(50),
	@PidPerfil uniqueidentifier,
	@SidStatus uniqueidentifier
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	insert into Usuario(UidUsuario,VchNombre,VchApellidoPaterno,VchApellidoMaterno,DtFechaDeNacimiento,
	VchCorreo,VchCurp,VchTelefono,DtFechaDeInicio,DtFechaDeSalida,
	VchUsuario,VchPassword,PidPerfil,SidStatus)
	values(newid(),@VchNombre, @VchApellidoPaterno, @VchApellidoMaterno,@DtFechaDeNacimiento,
	@VchCorreo,@VchCurp, @VchTelefono,@DtFechaDeInicio,@DtFechaDeSalida, @VchUsuario,@VchPassword,@PidPerfil,@SidStatus)
END


GO
/****** Object:  StoredProcedure [dbo].[sp_ModificarUsuario]    Script Date: 29/05/2017 13:06:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Aremy Daniela De Leon Tercero
-- Create date: 2017-05-17
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_ModificarUsuario] 
	-- Add the parameters for the stored procedure here
	@UidUsuario uniqueidentifier,
	@VchNombre nvarchar(50),
	@VchApellidoPaterno nvarchar(50),
	@VchApellidoMaterno nvarchar(50),
	@DtFechaDeNacimiento date,
	@VchCorreo nvarchar(50),
	@VchCurp nvarchar(50),
	@VchTelefono nvarchar(50),
	@DtFechaDeInicio nvarchar(50),
	@DtFechaDeSalida nvarchar(50),
	@VchUsuario nvarchar(50),
	@VchPassword nvarchar(50),
	@PidPerfil uniqueidentifier,
	@SidStatus uniqueidentifier
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	update Usuario set VchNombre=@VchNombre, 
	VchApellidoPaterno=@VchApellidoPaterno,
	VchApellidoMaterno=@VchApellidoMaterno,
	DtFechaDeNacimiento=@DtFechaDeNacimiento,
	VchCorreo=@VchCorreo,
	VchCurp=@VchCurp,
	VchTelefono=@VchTelefono,
	DtFechaDeInicio=@DtFechaDeInicio,
	DtFechaDeSalida=@DtFechaDeSalida,
	VchUsuario=@VchUsuario, 
	VchPassword=@VchPassword, 
	PidPerfil=@PidPerfil,
	SidStatus=@SidStatus
	where UidUsuario=@UidUsuario
END


GO
/****** Object:  StoredProcedure [dbo].[sp_Perfil]    Script Date: 29/05/2017 13:06:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_Perfil] 
	-- Add the parameters for the stored procedure here
	@VchNombre nvarchar(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	insert into Perfil(PidPerfil,VchPerfil)
	values(newid(),@VchNombre)
END


GO
/****** Object:  StoredProcedure [dbo].[usp_AppUser_Add]    Script Date: 29/05/2017 13:06:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[usp_AppUser_Add] 
@username nvarchar(50),
@firstname nvarchar(50),
@password nvarchar(50)

AS

SET NOCOUNT ON;

INSERT INTO AppUser (uid, username, firstname, password) VALUES (NEWID(), @username, @firstname, @password)



GO
/****** Object:  StoredProcedure [dbo].[usp_AppUser_Find]    Script Date: 29/05/2017 13:06:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[usp_AppUser_Find]
@uid uniqueidentifier
AS

SET NOCOUNT ON

SELECT TOP 1 * FROM AppUser WHERE uid = @uid


GO
/****** Object:  StoredProcedure [dbo].[usp_AppUser_Search]    Script Date: 29/05/2017 13:06:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[usp_AppUser_Search]

@username nvarchar(50) = NULL,
@firstname nvarchar(50) = NULL


AS

SET NOCOUNT ON;

SELECT * FROM AppUser WHERE
((@username IS NOT NULL AND username LIKE '%' + @username + '%') OR @username IS NULL) AND
((@firstname IS NOT NULL AND firstname LIKE '%' + @firstname + '%') OR @firstname IS NULL)



GO
/****** Object:  StoredProcedure [dbo].[usp_AppUser_Update]    Script Date: 29/05/2017 13:06:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[usp_AppUser_Update]
@uid uniqueidentifier,
@username nvarchar(50),
@firstname nvarchar(50),
@password nvarchar(50)

AS

SET NOCOUNT ON;

UPDATE AppUser SET  username = @username, firstname = @firstname, password = @password WHERE uid = @uid



GO
/****** Object:  StoredProcedure [dbo].[usp_Area_Add]    Script Date: 29/05/2017 13:06:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[usp_Area_Add]
@VchNombre nvarchar(50) ,
@UidDepartamento uniqueidentifier

AS

SET NOCOUNT ON

INSERT INTO Area (UidArea, VchNombre, UidDepartamento) VALUES (NEWID(), @VchNombre, @UidDepartamento)



GO
/****** Object:  StoredProcedure [dbo].[usp_Area_Find]    Script Date: 29/05/2017 13:06:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[usp_Area_Find]
@UidArea uniqueidentifier

AS

SET NOCOUNT ON

SELECT TOP 1 * FROM Area WHERE UidArea = @UidArea

GO
/****** Object:  StoredProcedure [dbo].[usp_Area_Search]    Script Date: 29/05/2017 13:06:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[usp_Area_Search]
@VchNombre nvarchar(50) = null,
@UidDepartamento uniqueidentifier = null

AS

SET NOCOUNT ON

SELECT * FROM Area WHERE
((@VchNombre IS NOT NULL AND VchNombre LIKE '%' + @VchNombre + '%') OR @VchNombre IS NULL) AND
((@UidDepartamento IS NOT NULL AND UidDepartamento = @UidDepartamento) OR @UidDepartamento IS NULL)

GO
/****** Object:  StoredProcedure [dbo].[usp_Area_Update]    Script Date: 29/05/2017 13:06:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[usp_Area_Update]
@UidArea uniqueidentifier,
@VchNombre nvarchar(50),
@UidDepartamento uniqueidentifier

AS

SET NOCOUNT ON

UPDATE Area SET VchNombre = @VchNombre, UidDepartamento = @UidDepartamento WHERE UidArea = @UidArea



GO
/****** Object:  StoredProcedure [dbo].[usp_Departamento_Add]    Script Date: 29/05/2017 13:06:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[usp_Departamento_Add]
@VchNombre nvarchar(50),
@UidDepartamento uniqueidentifier OUTPUT
AS 
SET NOCOUNT ON

SET @UidDepartamento = NEWID()

INSERT INTO Departamento (UidDepartamento, VchNombre) VALUES (@UidDepartamento, @VchNombre)



GO
/****** Object:  StoredProcedure [dbo].[usp_Departamento_Find]    Script Date: 29/05/2017 13:06:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[usp_Departamento_Find]
@UidDepartamento uniqueidentifier

AS 
SET NOCOUNT ON

SELECT TOP (1) * FROM Departamento WHERE UidDepartamento = @UidDepartamento



GO
/****** Object:  StoredProcedure [dbo].[usp_Departamento_Search]    Script Date: 29/05/2017 13:06:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[usp_Departamento_Search]
@VchNombre nvarchar(50) = null
AS 
SET NOCOUNT ON

SELECT * FROM Departamento WHERE
((@VchNombre IS NOT NULL AND VchNombre LIKE '%' + @VchNombre + '%') OR @VchNombre IS NULL) 



GO
/****** Object:  StoredProcedure [dbo].[usp_Departamento_Update]    Script Date: 29/05/2017 13:06:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[usp_Departamento_Update]
@UidDepartamento uniqueidentifier,
@VchNombre nvarchar(50)

AS 
SET NOCOUNT ON

UPDATE Departamento SET VchNombre = @VchNombre WHERE UidDepartamento = @UidDepartamento



GO
/****** Object:  StoredProcedure [dbo].[usp_Direccion_Find]    Script Date: 29/05/2017 13:06:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_Direccion_Find]

@UidDireccion uniqueidentifier

AS

SET NOCOUNT ON

SELECT * FROM Direccion d WHERE UidDireccion = @UidDireccion
GO
/****** Object:  StoredProcedure [dbo].[usp_Direccion_Update]    Script Date: 29/05/2017 13:06:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_Direccion_Update]

@UidDireccion uniqueidentifier,
@UidPais uniqueidentifier,
@UidEstado uniqueidentifier,
@VchMunicipio nvarchar(30),
@VchCiudad nvarchar(30),
@VchColonia nvarchar(20),
@VchCalle nvarchar(20),
@VchConCalle nvarchar(20),
@VchYCalle nvarchar(20),
@VchNoExt nvarchar(20),
@VchNoInt nvarchar(20),
@VchReferencia nvarchar(200)

AS

SET NOCOUNT ON

UPDATE Direccion SET
UidPais = @UidPais,
UidEstado = @UidEstado,
VchMunicipio = @VchMunicipio,
VchCiudad = @VchCiudad,
VchColonia = @VchColonia,
VchCalle = @VchCalle,
VchConCalle = @VchConCalle,
VchYCalle = @VchYCalle,
VchNoExt = @VchNoExt,
VchNoInt = @VchNoInt,
VchReferencia = @VchReferencia

WHERE UidDireccion = @UidDireccion
GO
/****** Object:  StoredProcedure [dbo].[usp_Empresa_Add]    Script Date: 29/05/2017 13:06:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_Empresa_Add]
@VchNombreComercial nvarchar(50),
@VchRazonSocial nvarchar(60),
@VchGiro nvarchar(40),
@ChRFC nchar(13),
@DtFechaRegistro date,
@UidEmpresa uniqueidentifier output
AS

SET NOCOUNT ON

SET @UidEmpresa = NEWID()

INSERT INTO [dbo].[Empresa]
           ([UidEmpresa]
           ,[VchNombreComercial]
           ,[VchRazonSocial]
           ,[VchGiro]
           ,[ChRFC]
           ,[DtFechaRegistro])
     VALUES
           (@UidEmpresa
           ,@VchNombreComercial
           ,@VchRazonSocial
           ,@VchGiro
           ,@ChRFC
           ,@DtFechaRegistro)

GO
/****** Object:  StoredProcedure [dbo].[usp_Empresa_Find]    Script Date: 29/05/2017 13:06:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_Empresa_Find]
@UidEmpresa uniqueidentifier
AS

SET NOCOUNT ON

SELECT * FROM Empresa WHERE UidEmpresa = @UidEmpresa
GO
/****** Object:  StoredProcedure [dbo].[usp_Empresa_Search]    Script Date: 29/05/2017 13:06:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_Empresa_Search]
@VchNombreComercial nvarchar(50) = NULL,
@VchRazonSocial nvarchar(60) = NULL,
@VchGiro nvarchar(40) = NULL,
@ChRFC nchar(13) = NULL,
@DtFechaRegistroInicio date = NULL,
@DtFechaRegistroFin date = NULL
AS

SET NOCOUNT ON

SELECT * FROM Empresa WHERE
((@VchNombreComercial IS NOT NULL AND VchNombreComercial LIKE '%' + @VchNombreComercial  + '%') OR @VchNombreComercial IS NULL) AND
((@VchRazonSocial IS NOT NULL AND VchRazonSocial LIKE '%' + @VchRazonSocial + '%') OR @VchRazonSocial IS NULL) AND
((@VchGiro IS NOT NULL AND VchGiro LIKE '%' + @VchGiro + '%') OR @VchGiro IS NULL) AND
((@ChRFC IS NOT NULL AND ChRFC = @ChRFC) OR @ChRFC IS NULL) AND
((@DtFechaRegistroInicio IS NOT NULL AND DtFechaRegistro >= @DtFechaRegistroInicio) OR @DtFechaRegistroInicio IS NULL) AND
((@DtFechaRegistroFin IS NOT NULL AND DtFechaRegistro <= @DtFechaRegistroFin) OR @DtFechaRegistroFin IS NULL) 


GO
/****** Object:  StoredProcedure [dbo].[usp_Empresa_Update]    Script Date: 29/05/2017 13:06:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_Empresa_Update] 
@UidEmpresa uniqueidentifier,
@VchNombreComercial nvarchar(50),
@VchRazonSocial nvarchar(60),
@VchGiro nvarchar(40),
@ChRFC nchar(13),
@DtFechaRegistro date

AS

SET NOCOUNT ON

UPDATE Empresa SET VchNombreComercial = @VchNombreComercial, VchRazonSocial = @VchRazonSocial, VchGiro = @VchGiro, ChRFC = @ChRFC, DtFechaRegistro = @DtFechaRegistro WHERE UidEmpresa = @UidEmpresa
GO
/****** Object:  StoredProcedure [dbo].[usp_EmpresaDireccion_Add]    Script Date: 29/05/2017 13:06:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_EmpresaDireccion_Add] 

@UidEmpresa uniqueidentifier,
@UidPais uniqueidentifier,
@UidEstado uniqueidentifier,
@VchMunicipio nvarchar(30),
@VchCiudad nvarchar(30),
@VchColonia nvarchar(20),
@VchCalle nvarchar(20),
@VchConCalle nvarchar(20),
@VchYCalle nvarchar(20),
@VchNoExt nvarchar(20),
@VchNoInt nvarchar(20),
@VchReferencia nvarchar(200)

AS

SET NOCOUNT ON

DECLARE @UidDireccion uniqueidentifier

SET @UidDireccion = NEWID()

INSERT INTO Direccion (UidDireccion, UidPais, UidEstado, VchMunicipio, VchCiudad, VchColonia, VchCalle, VchConCalle, VchYCalle, VchNoExt, VchNoInt, VchReferencia) VALUES (@UidDireccion, @UidPais, @UidEstado, @VchMunicipio, @VchCiudad, @VchColonia, @VchCalle, @VchConCalle, @VchYCalle, @VchNoExt, @VchNoInt, @VchReferencia)

INSERT INTO EmpresaDireccion (UidEmpresa, UidDireccion) VALUES (@UidEmpresa, @UidDireccion)

GO
/****** Object:  StoredProcedure [dbo].[usp_EmpresaDireccion_FindAll]    Script Date: 29/05/2017 13:06:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_EmpresaDireccion_FindAll]

@UidEmpresa uniqueidentifier

AS

SET NOCOUNT ON

SELECT Direccion.* FROM Direccion INNER JOIN EmpresaDireccion ON Direccion.UidDireccion = EmpresaDireccion.UidDireccion WHERE EmpresaDireccion.UidEmpresa = @UidEmpresa
GO
/****** Object:  StoredProcedure [dbo].[usp_EmpresaDireccion_Remove]    Script Date: 29/05/2017 13:06:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_EmpresaDireccion_Remove]

@UidDireccion uniqueidentifier

AS

SET NOCOUNT ON

DELETE FROM EmpresaDireccion WHERE UidDireccion = @UidDireccion

DELETE FROM Direccion WHERE UidDireccion = @UidDireccion
GO
/****** Object:  StoredProcedure [dbo].[usp_EmpresaTelefono_Add]    Script Date: 29/05/2017 13:06:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_EmpresaTelefono_Add]
@UidEmpresa uniqueidentifier,
@VchTelefono nvarchar(20)

AS

SET NOCOUNT ON

DECLARE @UidTelefono uniqueidentifier

SET @UidTelefono = NEWID()

INSERT INTO Telefono (UidTelefono, VchTelefono) VALUES (@UidTelefono, @VchTelefono)

INSERT INTO EmpresaTelefono (UidEmpresa, UidTelefono) VALUES (@UidEmpresa, @UidTelefono)
GO
/****** Object:  StoredProcedure [dbo].[usp_EmpresaTelefono_FindAll]    Script Date: 29/05/2017 13:06:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_EmpresaTelefono_FindAll]
@UidEmpresa uniqueidentifier

AS

SET NOCOUNT ON


SELECT Telefono.*
FROM Telefono INNER JOIN EmpresaTelefono
ON Telefono.UidTelefono = EmpresaTelefono.UidTelefono
WHERE EmpresaTelefono.UidEmpresa = @UidEmpresa
GO
/****** Object:  StoredProcedure [dbo].[usp_EmpresaTelefono_Remove]    Script Date: 29/05/2017 13:06:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_EmpresaTelefono_Remove]
@UidTelefono uniqueidentifier

AS

SET NOCOUNT ON


DELETE FROM EmpresaTelefono WHERE UidTelefono = @UidTelefono

DELETE FROM Telefono WHERE UidTelefono = @UidTelefono
GO
/****** Object:  StoredProcedure [dbo].[usp_Estado_FindAll]    Script Date: 29/05/2017 13:06:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_Estado_FindAll]
@UidPais uniqueidentifier
AS

SET NOCOUNT ON

SELECT * FROM Estado WHERE UidPais = @UidPais
GO
/****** Object:  StoredProcedure [dbo].[usp_Pais_FindAll]    Script Date: 29/05/2017 13:06:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_Pais_FindAll]

AS

SET NOCOUNT ON

SELECT * FROM Pais ORDER BY VchNombre
GO
/****** Object:  StoredProcedure [dbo].[usp_Sucursal_Add]    Script Date: 29/05/2017 13:06:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_Sucursal_Add]
@VchNombre nvarchar(30),
@DtFechaRegistro date,
@UidEmpresa uniqueidentifier,
@UidSucursal uniqueidentifier OUTPUT

AS

SET NOCOUNT ON

SET @UidSucursal = NEWID()

INSERT INTO Sucursal (UidSucursal, VchNombre, DtFechaRegistro, UidEmpresa) VALUES (@UidSucursal, @VchNombre, @DtFechaRegistro, @UidEmpresa)
GO
/****** Object:  StoredProcedure [dbo].[usp_Sucursal_Find]    Script Date: 29/05/2017 13:06:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_Sucursal_Find]

@UidSucursal uniqueidentifier

AS

SET NOCOUNT ON

SELECT TOP (1) * FROM Sucursal WHERE UidSucursal = @UidSucursal
GO
/****** Object:  StoredProcedure [dbo].[usp_Sucursal_FindAll]    Script Date: 29/05/2017 13:06:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_Sucursal_FindAll]
@UidEmpresa uniqueidentifier 
AS

SET NOCOUNT ON

SELECT * FROM Sucursal WHERE
(UidEmpresa = @UidEmpresa)
GO
/****** Object:  StoredProcedure [dbo].[usp_Sucursal_Search]    Script Date: 29/05/2017 13:06:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_Sucursal_Search]
@VchNombre nvarchar(50) = null,
@DtFechaRegistroInicio date = null,
@DtFechaRegistroFin date = null,
@UidEmpresa uniqueidentifier = null
AS

SET NOCOUNT ON

SELECT * FROM Sucursal WHERE
(@VchNombre IS NULL OR VchNombre LIKE '%' + @VchNombre + '%') AND
(@DtFechaRegistroInicio IS NULL OR DtFechaRegistro >= @DtFechaRegistroInicio) AND
(@DtFechaRegistroFin IS NULL OR DtFechaRegistro <= @DtFechaRegistroFin) AND
(@UidEmpresa IS NULL OR UidEmpresa = @UidEmpresa)
GO
/****** Object:  StoredProcedure [dbo].[usp_Sucursal_Update]    Script Date: 29/05/2017 13:06:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_Sucursal_Update]
@UidSucursal uniqueidentifier,
@VchNombre nvarchar(50),
@DtFechaRegistro date

AS

SET NOCOUNT ON

UPDATE Sucursal SET
VchNombre = @VchNombre,
DtFechaRegistro = @DtFechaRegistro

WHERE
UidSucursal = @UidSucursal
GO
/****** Object:  StoredProcedure [dbo].[usp_SucursalDireccion_Add]    Script Date: 29/05/2017 13:06:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_SucursalDireccion_Add] 

@UidSucursal uniqueidentifier,
@UidPais uniqueidentifier,
@UidEstado uniqueidentifier,
@VchMunicipio nvarchar(30),
@VchCiudad nvarchar(30),
@VchColonia nvarchar(20),
@VchCalle nvarchar(20),
@VchConCalle nvarchar(20),
@VchYCalle nvarchar(20),
@VchNoExt nvarchar(20),
@VchNoInt nvarchar(20),
@VchReferencia nvarchar(200)

AS

SET NOCOUNT ON

DECLARE @UidDireccion uniqueidentifier

SET @UidDireccion = NEWID()

INSERT INTO Direccion (UidDireccion, UidPais, UidEstado, VchMunicipio, VchCiudad, VchColonia, VchCalle, VchConCalle, VchYCalle, VchNoExt, VchNoInt, VchReferencia) VALUES (@UidDireccion, @UidPais, @UidEstado, @VchMunicipio, @VchCiudad, @VchColonia, @VchCalle, @VchConCalle, @VchYCalle, @VchNoExt, @VchNoInt, @VchReferencia)

INSERT INTO SucursalDireccion (UidSucursal, UidDireccion) VALUES (@UidSucursal, @UidDireccion)

GO
/****** Object:  StoredProcedure [dbo].[usp_SucursalDireccion_FindAll]    Script Date: 29/05/2017 13:06:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[usp_SucursalDireccion_FindAll]

@UidSucursal uniqueidentifier

AS

SET NOCOUNT ON

SELECT Direccion.* FROM Direccion INNER JOIN SucursalDireccion ON Direccion.UidDireccion = SucursalDireccion.UidDireccion WHERE SucursalDireccion.UidSucursal = @UidSucursal

GO
/****** Object:  StoredProcedure [dbo].[usp_SucursalDireccion_Remove]    Script Date: 29/05/2017 13:06:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[usp_SucursalDireccion_Remove]

@UidDireccion uniqueidentifier

AS

SET NOCOUNT ON

DELETE FROM SucursalDireccion WHERE UidDireccion = @UidDireccion

DELETE FROM Direccion WHERE UidDireccion = @UidDireccion

GO
/****** Object:  StoredProcedure [dbo].[usp_SucursalTelefono_Add]    Script Date: 29/05/2017 13:06:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[usp_SucursalTelefono_Add]
@UidSucursal uniqueidentifier,
@VchTelefono nvarchar(20)

AS

SET NOCOUNT ON

DECLARE @UidTelefono uniqueidentifier

SET @UidTelefono = NEWID()

INSERT INTO Telefono (UidTelefono, VchTelefono) VALUES (@UidTelefono, @VchTelefono)

INSERT INTO SucursalTelefono (UidSucursal, UidTelefono) VALUES (@UidSucursal, @UidTelefono)

GO
/****** Object:  StoredProcedure [dbo].[usp_SucursalTelefono_FindAll]    Script Date: 29/05/2017 13:06:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[usp_SucursalTelefono_FindAll]
@UidSucursal uniqueidentifier

AS

SET NOCOUNT ON


SELECT Telefono.*
FROM Telefono INNER JOIN SucursalTelefono
ON Telefono.UidTelefono = SucursalTelefono.UidTelefono
WHERE SucursalTelefono.UidSucursal = @UidSucursal

GO
/****** Object:  StoredProcedure [dbo].[usp_SucursalTelefono_Remove]    Script Date: 29/05/2017 13:06:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[usp_SucursalTelefono_Remove]
@UidTelefono uniqueidentifier

AS

SET NOCOUNT ON


DELETE FROM SucursalTelefono WHERE UidTelefono = @UidTelefono

DELETE FROM Telefono WHERE UidTelefono = @UidTelefono

GO
/****** Object:  StoredProcedure [dbo].[usp_Telefono_Find]    Script Date: 29/05/2017 13:06:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_Telefono_Find]
@UidTelefono uniqueidentifier

AS

SET NOCOUNT ON

SELECT TOP (1) * FROM Telefono WHERE UidTelefono = @UidTelefono

GO
/****** Object:  StoredProcedure [dbo].[usp_Telefono_Update]    Script Date: 29/05/2017 13:06:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_Telefono_Update]
@UidTelefono uniqueidentifier,
@VchTelefono nvarchar(20)

AS

SET NOCOUNT ON

UPDATE Telefono SET VchTelefono = @VchTelefono WHERE UidTelefono = @UidTelefono
GO
USE [master]
GO
ALTER DATABASE [CodorniX] SET  READ_WRITE 
GO
