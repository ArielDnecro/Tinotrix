CREATE PROCEDURE usp_User_FindByName
@VchUsuario nvarchar(50)
AS
SET NOCOUNT ON
SELECT TOP(1) * FROM Usuario WHERE VchUsuario = @VchUsuario