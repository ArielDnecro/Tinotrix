CREATE PROCEDURE [dbo].[usp_Acceso] 
 @modulo nvarchar(100),
 @uidPerfil uniqueidentifier
AS

DECLARE @IntNivelPerfil int, @IntNivelModulo int;

SELECT @IntNivelPerfil = IntJerarquiaAcceso
FROM Perfil p JOIN NivelAcceso n ON p.UidNivelAcceso = n.UidNivelAcceso
WHERE p.UidPerfil = @uidPerfil

SELECT @IntNivelModulo = IntJerarquiaAcceso
FROM Modulo m JOIN NivelAcceso n ON m.UidNivelAcceso = n.UidNivelAcceso
WHERE m.VchModulo = @modulo

IF @IntNivelModulo < @IntNivelPerfil
BEGIN
	SELECT 1
	RETURN
END

SELECT 1 FROM Perfil 
JOIN Acceso ON Perfil.UidPerfil = Acceso.UidPerfil
JOIN Modulo ON Acceso.UidModulo = Modulo.UidModulo
WHERE Perfil.UidPerfil = @uidPerfil AND Modulo.VchModulo = @modulo;
