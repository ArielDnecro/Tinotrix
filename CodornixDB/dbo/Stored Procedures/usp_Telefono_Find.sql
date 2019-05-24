CREATE PROCEDURE [dbo].[usp_Telefono_Find]
@UidTelefono uniqueidentifier

AS

SET NOCOUNT ON

SELECT TOP (1) Telefono.*, TipoTelefono.VchTipoTelefono FROM Telefono INNER JOIN TipoTelefono
On Telefono.UidTipoTelefono = TipoTelefono.UidTipoTelefono WHERE UidTelefono = @UidTelefono
