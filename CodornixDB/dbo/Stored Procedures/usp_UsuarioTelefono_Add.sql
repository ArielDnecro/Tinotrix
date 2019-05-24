
CREATE PROCEDURE [dbo].[usp_UsuarioTelefono_Add]
@UidUsuario uniqueidentifier,
@UidTipoTelefono uniqueidentifier,
@VchTelefono nvarchar(20)

AS

SET NOCOUNT ON

DECLARE @UidTelefono uniqueidentifier

SET @UidTelefono = NEWID()

INSERT INTO Telefono (UidTelefono, VchTelefono, UidTipoTelefono) VALUES (@UidTelefono, @VchTelefono, @UidTipoTelefono)

INSERT INTO UsuarioTelefono (UidUsuario, UidTelefono) VALUES (@UidUsuario, @UidTelefono)
