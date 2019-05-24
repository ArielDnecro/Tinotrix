CREATE PROCEDURE [dbo].[usp_EmpresaTelefono_FindAll]
@UidEmpresa uniqueidentifier

AS

SET NOCOUNT ON


SELECT Telefono.*, TipoTelefono.VchTipoTelefono
FROM Telefono INNER JOIN EmpresaTelefono
ON Telefono.UidTelefono = EmpresaTelefono.UidTelefono
INNER JOIN TipoTelefono
On Telefono.UidTipoTelefono = TipoTelefono.UidTipoTelefono
WHERE EmpresaTelefono.UidEmpresa = @UidEmpresa
