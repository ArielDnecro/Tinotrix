
CREATE PROCEDURE [dbo].[usp_SucursalTelefono_Remove]
@UidTelefono uniqueidentifier

AS

SET NOCOUNT ON


DELETE FROM SucursalTelefono WHERE UidTelefono = @UidTelefono

DELETE FROM Telefono WHERE UidTelefono = @UidTelefono
