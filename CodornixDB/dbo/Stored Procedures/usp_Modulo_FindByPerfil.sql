CREATE PROCEDURE [dbo].[usp_Modulo_FindByPerfil]
@UidPerfil uniqueidentifier,
@UidNivelAcceso uniqueidentifier = null
AS

SET NOCOUNT ON

SELECT * FROM Modulo WHERE
BitDenegable = 1 AND
(@UidNivelAcceso IS NULL OR UidNivelAcceso = @UidNivelAcceso) AND
UidModulo NOT IN (SELECT UidModulo FROM Acceso WHERE UidPerfil = @UidPerfil) 
