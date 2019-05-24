
CREATE PROCEDURE [dbo].[usp_SucursalDireccion_FindAll]

@UidSucursal uniqueidentifier

AS

SET NOCOUNT ON

SELECT Direccion.* FROM Direccion INNER JOIN SucursalDireccion ON Direccion.UidDireccion = SucursalDireccion.UidDireccion WHERE SucursalDireccion.UidSucursal = @UidSucursal
