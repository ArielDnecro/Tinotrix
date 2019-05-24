CREATE PROCEDURE [dbo].[usp_UsuarioEmpresa_FindAll]
@UidUsuario uniqueidentifier
AS

SET NOCOUNT ON

SELECT Empresa.* FROM Empresa INNER JOIN UsuarioEmpresa ON Empresa.UidEmpresa = UsuarioEmpresa.UidEmpresa INNER JOIN Usuario ON UsuarioEmpresa.UidUsuario = @UidUsuario;
