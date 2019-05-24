CREATE PROCEDURE [dbo].[usp_EmpresaTelefono_Remove]
@UidTelefono uniqueidentifier

AS

SET NOCOUNT ON


DELETE FROM EmpresaTelefono WHERE UidTelefono = @UidTelefono

DELETE FROM Telefono WHERE UidTelefono = @UidTelefono
