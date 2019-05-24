CREATE PROCEDURE usp_NivelAcceso_FindByName
@VchNivelAcceso nvarchar(50)
AS

SET NOCOUNT ON

SELECT TOP(1) * FROM NivelAcceso WHERE VchNivelAcceso LIKE @VchNivelAcceso