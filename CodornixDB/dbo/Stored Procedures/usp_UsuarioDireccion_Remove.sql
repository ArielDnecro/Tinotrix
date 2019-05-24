
CREATE PROCEDURE [dbo].[usp_UsuarioDireccion_Remove]

@UidDireccion uniqueidentifier

AS

SET NOCOUNT ON

DELETE FROM UsuarioDireccion WHERE UidDireccion = @UidDireccion

DELETE FROM Direccion WHERE UidDireccion = @UidDireccion
