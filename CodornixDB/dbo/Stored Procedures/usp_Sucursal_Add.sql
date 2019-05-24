CREATE PROCEDURE [dbo].[usp_Sucursal_Add]
@VchNombre nvarchar(30),
@DtFechaRegistro date,
@UidEmpresa uniqueidentifier,
@UidTipoSucursal uniqueidentifier,
@UidSucursal uniqueidentifier OUTPUT, 
@VchRutaImagen	nvarchar(200) = null

AS

SET NOCOUNT ON

SET @UidSucursal = NEWID()

INSERT INTO Sucursal (UidSucursal, VchNombre, DtFechaRegistro, UidEmpresa, UidTipoSucursal, VchRutaImagen) VALUES (@UidSucursal, @VchNombre, GETDATE(), @UidEmpresa, @UidTipoSucursal, @VchRutaImagen)
