
CREATE PROCEDURE [dbo].[usp_AccesoUsuario] 
 @modulo nvarchar(100),
 @uidUsuario uniqueidentifier
AS

SELECT 1 FROM Usuario 
JOIN AccesoUsuario ON Usuario.UidUsuario = AccesoUsuario.UidUsuario
JOIN Modulo ON AccesoUsuario.UidModulo = Modulo.UidModulo
WHERE Usuario.UidUsuario = @uidUsuario AND Modulo.VchModulo = @modulo;
