CREATE PROCEDURE [dbo].[usp_Empresa_Add]
@VchNombreComercial nvarchar(50),
@VchRazonSocial nvarchar(60),
@VchGiro nvarchar(40),
@ChRFC nchar(13),
@VchRutaImagen nvarchar(200),
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
           ,[DtFechaRegistro]
		   ,[VchRutaImagen])
     VALUES
           (@UidEmpresa
           ,@VchNombreComercial
           ,@VchRazonSocial
           ,@VchGiro
           ,@ChRFC
           ,GETDATE()
		   ,@VchRutaImagen)
