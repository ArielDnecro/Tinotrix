CREATE PROCEDURE [dbo].[usp_Sucursal_Update]
@UidSucursal uniqueidentifier,
@VchNombre nvarchar(50),
@UidTipoSucursal uniqueidentifier,
@DtFechaRegistro date,
@VchRutaImagen nvarchar(200)

AS

SET NOCOUNT ON

UPDATE Sucursal SET
VchNombre = @VchNombre,
DtFechaRegistro = @DtFechaRegistro,
UidTipoSucursal = @UidTipoSucursal,
VchRutaImagen=@VchRutaImagen
WHERE
UidSucursal = @UidSucursal
