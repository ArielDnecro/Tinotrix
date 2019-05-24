CREATE PROCEDURE [dbo].[usp_Empresa_Update] 
@UidEmpresa uniqueidentifier,
@VchNombreComercial nvarchar(50),
@VchRazonSocial nvarchar(60),
@VchGiro nvarchar(40),
@ChRFC nchar(13),
@VchRutaImagen nvarchar(200)

AS

SET NOCOUNT ON

UPDATE Empresa SET VchNombreComercial = @VchNombreComercial, VchRazonSocial = @VchRazonSocial, VchGiro = @VchGiro, ChRFC = @ChRFC, VchRutaImagen=@VchRutaImagen WHERE UidEmpresa = @UidEmpresa
