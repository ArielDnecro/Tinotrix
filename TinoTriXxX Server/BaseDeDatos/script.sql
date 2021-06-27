USE [master]
GO
/****** Object:  Database [TinotrixCliente]    Script Date: 25/06/2019 14:20:54 ******/
IF  EXISTS (SELECT * FROM sysdatabases WHERE (name = 'TinotrixServer')) 
BEGIN
	DROP DATABASE TinotrixServer;
END
go
CREATE DATABASE [TinotrixServer]
GO
USE [TinotrixServer]
GO
/****** Object:  Table [dbo].[_Empresa]    Script Date: 25/06/2019 14:20:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[_Empresa](
	[UidEmpresa] [uniqueidentifier] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[_Encargado]    Script Date: 08/08/2019 14:59:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[_Encargado](
	[UidEncargado] [uniqueidentifier] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[_Impresora]    Script Date: 08/08/2019 14:59:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[_Impresora](
	[VchDescripcion] [varchar](150) NOT NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[_IPServidor]    Script Date: 08/08/2019 14:59:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[_IPServidor](
	[VchIpServidor] [varchar](150) NOT NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[_Licencia]    Script Date: 08/08/2019 14:59:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[_Licencia](
	[UidLicencia] [uniqueidentifier] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[_Puerto]    Script Date: 08/08/2019 14:59:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[_Puerto](
	[VchPuerto] [varchar](150) NOT NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[_Turno]    Script Date: 08/08/2019 14:59:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[_Turno](
	[IntNoFolio] [int] NOT NULL,
	[UidFolio] [uniqueidentifier] NOT NULL,
	[DtHrEntrada] [time](0) NOT NULL,
	[DtFhEntrada] [date] NOT NULL,
	[IntTFotos] [int] NOT NULL,
	[IntTCosto] [int] NOT NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[_Usuario]    Script Date: 08/08/2019 14:59:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[_Usuario](
	[UidUsuario] [uniqueidentifier] NULL
) ON [PRIMARY]

GO
--INSERT [dbo].[_Empresa] ([UidEmpresa]) VALUES (N'66e48284-328f-42d5-914d-da4b8b6c4229')
--INSERT [dbo].[_Encargado] ([UidEncargado]) VALUES (N'7b44c844-5aac-4689-b964-f2f7306bb0f4')
--INSERT [dbo].[_Impresora] ([VchDescripcion]) VALUES (N'OneNote')
--INSERT [dbo].[_Licencia] ([UidLicencia]) VALUES (N'3ed3c4bd-18a6-48cc-9a5c-cfccf9a576a4')
--INSERT [dbo].[_Puerto] ([VchPuerto]) VALUES (N'8000')
--INSERT [dbo].[_Turno] ([IntNoFolio], [UidFolio], [DtHrEntrada], [DtFhEntrada], [IntTFotos], [IntTCosto]) VALUES (24, N'd51a472c-e241-4158-acf2-e56a7ab7eb19', CAST(N'17:10:02' AS Time), CAST(N'2019-08-07' AS Date), 0, 0)
--INSERT [dbo].[_Usuario] ([UidUsuario]) VALUES (N'0232c3fb-1984-4253-bd43-e4a76db21267')
/****** Object:  StoredProcedure [dbo].[Wpf_ActualizarEmpresa]    Script Date: 08/08/2019 14:59:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Wpf_ActualizarEmpresa]
	@UidEmpresa uniqueidentifier
AS
	delete from _Empresa
	insert into _Empresa (UidEmpresa) values(@UidEmpresa)
RETURN 0

GO
/****** Object:  StoredProcedure [dbo].[Wpf_ActualizarEncargado]    Script Date: 08/08/2019 14:59:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Wpf_ActualizarEncargado]
	@UidEncargado uniqueidentifier
AS
	delete from _Encargado
	insert into _Encargado (UidEncargado) values(@UidEncargado)
RETURN 0
GO
/****** Object:  StoredProcedure [dbo].[Wpf_ActualizarImpresora]    Script Date: 08/08/2019 14:59:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[Wpf_ActualizarImpresora]
	@VchDescripcion Varchar(150)
AS
	delete from _Impresora
	insert into _Impresora (VchDescripcion) values(@VchDescripcion)
RETURN 0
GO
/****** Object:  StoredProcedure [dbo].[Wpf_ActualizarIpServidor]    Script Date: 08/08/2019 14:59:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
create PROCEDURE [dbo].[Wpf_ActualizarIpServidor]
	-- Add the parameters for the stored procedure here
	@VchIpServidor Varchar(150)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	delete from _IPServidor
	Insert _IPServidor ( VchIpServidor) values(@VchIpServidor)
END

GO
/****** Object:  StoredProcedure [dbo].[Wpf_ActualizarLicencia]    Script Date: 08/08/2019 14:59:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Wpf_ActualizarLicencia]
	@UidLicencia uniqueidentifier
AS
	delete from _Licencia
	insert into _Licencia (UidLicencia) values(@UidLicencia)
RETURN 0
GO
/****** Object:  StoredProcedure [dbo].[Wpf_ActualizarPuerto]    Script Date: 08/08/2019 14:59:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
create PROCEDURE [dbo].[Wpf_ActualizarPuerto]
	-- Add the parameters for the stored procedure here
	@VchPuerto Varchar(150)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	delete from _Puerto
	Insert _Puerto ( VchPuerto) values(@VchPuerto)
END

GO
/****** Object:  StoredProcedure [dbo].[Wpf_ActualizarTurno]    Script Date: 08/08/2019 14:59:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Wpf_ActualizarTurno]
	@IntTFotos int,
	@IntTCosto int
AS ------aqui agrego la cantidad resultante
	update _Turno set IntTFotos =IntTFotos + @IntTFotos, IntTCosto = IntTCosto+@IntTCosto
RETURN 0
GO
/****** Object:  StoredProcedure [dbo].[Wpf_ActualizarUsuario]    Script Date: 08/08/2019 14:59:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Wpf_ActualizarUsuario]
	@UidUsuario uniqueidentifier
AS
	delete from _Usuario
	insert into _Usuario (UidUsuario) values(@UidUsuario)
RETURN 0

GO
/****** Object:  StoredProcedure [dbo].[Wpf_CerrarTurno]    Script Date: 08/08/2019 14:59:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Wpf_CerrarTurno]
AS
	delete from _Turno
RETURN 0


GO
/****** Object:  StoredProcedure [dbo].[Wpf_Empresa_Find]    Script Date: 08/08/2019 14:59:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Wpf_Empresa_Find]
AS
 if((SELECT count(UidEmpresa) from _Empresa)=1)
  begin
	SELECT UidEmpresa from _Empresa
  end

GO
/****** Object:  StoredProcedure [dbo].[Wpf_Encargado_Find]    Script Date: 08/08/2019 14:59:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 CREATE PROCEDURE [dbo].[Wpf_Encargado_Find]
AS
 if((SELECT count(UidEncargado) from _Encargado)=1)
  begin
	SELECT UidEncargado from _Encargado
  end

GO
/****** Object:  StoredProcedure [dbo].[Wpf_Impresora_Find]    Script Date: 08/08/2019 14:59:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[Wpf_Impresora_Find]
	
AS
 if((SELECT count(VchDescripcion) from _Impresora)=1)
  begin
	SELECT VchDescripcion from _Impresora
  end

GO
/****** Object:  StoredProcedure [dbo].[Wpf_IniciarTurno]    Script Date: 08/08/2019 14:59:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 CREATE PROCEDURE [dbo].[Wpf_IniciarTurno]
	@IntNoFolio int,
	@UidFolio uniqueidentifier,
	@DtHrEntrada time,
	@DtFhEntrada date,
	@IntTFotos int,
	@IntTCosto int
AS

--21 de ago 19 inicia turno desde que abre nuevo turno y como se abre la aplicacion
  --por eso es el delete, ya que obtiene el turno de la web
  delete from _Turno
  
	Insert into _Turno(IntNoFolio, UidFolio,DtHrEntrada,DtFhEntrada,IntTFotos,IntTCosto)values(@IntNoFolio,
	@UidFolio ,
	@DtHrEntrada,
	@DtFhEntrada,
	@IntTFotos ,
	@IntTCosto )
RETURN 0

GO
/****** Object:  StoredProcedure [dbo].[Wpf_IPServidor_Find]    Script Date: 08/08/2019 14:59:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
create PROCEDURE [dbo].[Wpf_IPServidor_Find]
	-- Add the parameters for the stored procedure here
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	select VchIpServidor from _IPServidor
END

GO
/****** Object:  StoredProcedure [dbo].[Wpf_Licencia_Find]    Script Date: 08/08/2019 14:59:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Wpf_Licencia_Find]
	
AS
 if((SELECT count(UidLicencia) from _Licencia)=1)
  begin
	SELECT UidLicencia from _Licencia
  end

GO
/****** Object:  StoredProcedure [dbo].[Wpf_Puerto_Find]    Script Date: 08/08/2019 14:59:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
create PROCEDURE [dbo].[Wpf_Puerto_Find]
	-- Add the parameters for the stored procedure here
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	select   VchPuerto from _Puerto
END

GO
/****** Object:  StoredProcedure [dbo].[Wpf_RevocarEmpresa]    Script Date: 08/08/2019 14:59:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Wpf_RevocarEmpresa]
AS
	delete from _Empresa
	
RETURN 0

GO
/****** Object:  StoredProcedure [dbo].[Wpf_RevocarEncargado]    Script Date: 08/08/2019 14:59:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Wpf_RevocarEncargado]
AS
	delete from _Encargado
	
RETURN 0
GO
/****** Object:  StoredProcedure [dbo].[Wpf_RevocarLicencia]    Script Date: 08/08/2019 14:59:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Wpf_RevocarLicencia]
AS
	delete from _Licencia
	
RETURN 0

GO
/****** Object:  StoredProcedure [dbo].[Wpf_RevocarUsuario]    Script Date: 08/08/2019 14:59:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Wpf_RevocarUsuario]
AS
	delete from _Usuario
	
RETURN 0

GO
/****** Object:  StoredProcedure [dbo].[Wpf_Turno_Find]    Script Date: 08/08/2019 14:59:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Wpf_Turno_Find]
	
AS
 if((SELECT count(*) from _turno)=1)
  begin
	SELECT * from _Turno
  end

GO
/****** Object:  StoredProcedure [dbo].[Wpf_Usuario_Find]    Script Date: 08/08/2019 14:59:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

  CREATE PROCEDURE [dbo].[Wpf_Usuario_Find]
	
AS
 if((SELECT count(UidUsuario) from _Usuario)=1)
  begin
	SELECT UidUsuario from _Usuario
  end

GO
/****** Object:  StoredProcedure [dbo].[Wpf_VerificarExistencia_IPServidor]    Script Date: 08/08/2019 14:59:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
create PROCEDURE [dbo].[Wpf_VerificarExistencia_IPServidor]
	-- Add the parameters for the stored procedure here
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	DECLARE @IsBTech BIT
    -- Insert statements for procedure here
	if(( select count(*) from _IPServidor)=1)
		begin
		 set @IsBTech= 1
		end
	else
		begin
		set  @IsBTech= 0
		end
SELECT @IsBTech AS 'IsBTech'
END

GO
/****** Object:  StoredProcedure [dbo].[Wpf_VerificarExistencia_Puerto]    Script Date: 08/08/2019 14:59:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
create PROCEDURE [dbo].[Wpf_VerificarExistencia_Puerto]
	-- Add the parameters for the stored procedure here
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	DECLARE @IsBTech BIT
    -- Insert statements for procedure here
	if(( select count(*) from _Puerto)=1)
		begin
		 set @IsBTech= 1
		end
	else
		begin
		set  @IsBTech= 0
		end
SELECT @IsBTech AS 'IsBTech'
END

GO
/****** Object:  StoredProcedure [dbo].[Wpf_VerificarExistenciaEmpresa]    Script Date: 08/08/2019 14:59:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 CREATE PROCEDURE [dbo].[Wpf_VerificarExistenciaEmpresa]
	
AS
	select count(*) AS IntNoEmpresas from _Empresa

GO
/****** Object:  StoredProcedure [dbo].[Wpf_VerificarExistenciaEncargado]    Script Date: 08/08/2019 14:59:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Wpf_VerificarExistenciaEncargado]
	
AS
	select count(*) AS IntNoEncargados from _Encargado

GO
/****** Object:  StoredProcedure [dbo].[Wpf_VerificarExistenciaImpresora]    Script Date: 08/08/2019 14:59:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create PROCEDURE [dbo].[Wpf_VerificarExistenciaImpresora]
	
AS
	select count(*) AS IntNoImpresoras from _Impresora

GO
/****** Object:  StoredProcedure [dbo].[Wpf_VerificarExistenciaLicencia]    Script Date: 08/08/2019 14:59:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Wpf_VerificarExistenciaLicencia]
	
AS
	select count(*) AS IntNoLicencias from _Licencia

GO
/****** Object:  StoredProcedure [dbo].[Wpf_VerificarExistenciaUsuario]    Script Date: 08/08/2019 14:59:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Wpf_VerificarExistenciaUsuario]
	
AS
	select count(*) AS IntNoUsuarios from _Usuario
GO
