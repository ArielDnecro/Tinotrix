
CREATE PROCEDURE [dbo].[usp_SucursalTelefono_FindAll]
@UidSucursal uniqueidentifier

AS

SET NOCOUNT ON


SELECT Telefono.*, TipoTelefono.VchTipoTelefono
FROM Telefono INNER JOIN SucursalTelefono
ON Telefono.UidTelefono = SucursalTelefono.UidTelefono
INNER JOIN TipoTelefono
On Telefono.UidTipoTelefono = TipoTelefono.UidTipoTelefono
WHERE SucursalTelefono.UidSucursal = @UidSucursal
