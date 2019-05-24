
CREATE PROCEDURE [dbo].[usp_UsuarioTelefono_FindAll]
@UidUsuario uniqueidentifier

AS

SET NOCOUNT ON


SELECT Telefono.*, TipoTelefono.VchTipoTelefono
FROM Telefono INNER JOIN UsuarioTelefono
ON Telefono.UidTelefono = UsuarioTelefono.UidTelefono
INNER JOIN TipoTelefono
On Telefono.UidTipoTelefono = TipoTelefono.UidTipoTelefono
WHERE UsuarioTelefono.UidUsuario = @UidUsuario
