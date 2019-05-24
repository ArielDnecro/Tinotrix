
CREATE PROCEDURE [dbo].[usp_SucursalDireccion_Remove]

@UidDireccion uniqueidentifier

AS

SET NOCOUNT ON

DELETE FROM SucursalDireccion WHERE UidDireccion = @UidDireccion

DELETE FROM Direccion WHERE UidDireccion = @UidDireccion
