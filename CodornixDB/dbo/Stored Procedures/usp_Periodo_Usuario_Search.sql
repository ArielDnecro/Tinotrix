CREATE PROCEDURE usp_Periodo_Usuario_Search
@VchPattern nvarchar(100)
AS

SET NOCOUNT ON

SELECT * FROM Usuario WHERE
VchNombre LIKE '%' + @VchPattern + '%' OR
VchApellidoPaterno LIKE '%' + @VchPattern + '%' OR
VchApellidoMaterno LIKE '%' + @VchPattern + '%'