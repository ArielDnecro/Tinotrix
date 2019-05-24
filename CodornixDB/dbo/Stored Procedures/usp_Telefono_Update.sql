CREATE PROCEDURE [dbo].[usp_Telefono_Update]
@UidTelefono uniqueidentifier,
@VchTelefono nvarchar(20),
@UidTipoTelefono uniqueidentifier

AS

SET NOCOUNT ON

UPDATE Telefono SET VchTelefono = @VchTelefono, UidTipoTelefono = @UidTipoTelefono WHERE UidTelefono = @UidTelefono
