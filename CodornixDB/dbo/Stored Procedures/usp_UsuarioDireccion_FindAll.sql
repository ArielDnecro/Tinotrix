
CREATE PROCEDURE [dbo].[usp_UsuarioDireccion_FindAll]

@UidUsuario uniqueidentifier

AS

SET NOCOUNT ON

SELECT Direccion.* FROM Direccion INNER JOIN UsuarioDireccion ON Direccion.UidDireccion = UsuarioDireccion.UidDireccion WHERE UsuarioDireccion.UidUsuario = @UidUsuario
