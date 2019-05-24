
CREATE PROCEDURE [dbo].[usp_UsuarioTelefono_Remove]
@UidTelefono uniqueidentifier

AS

SET NOCOUNT ON


DELETE FROM UsuarioTelefono WHERE UidTelefono = @UidTelefono

DELETE FROM Telefono WHERE UidTelefono = @UidTelefono
