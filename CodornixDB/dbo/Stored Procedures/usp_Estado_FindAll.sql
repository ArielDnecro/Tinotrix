CREATE PROCEDURE [dbo].[usp_Estado_FindAll]
@UidPais uniqueidentifier
AS

SET NOCOUNT ON

SELECT * FROM Estado WHERE UidPais = @UidPais ORDER BY VchNombre ASC
