
CREATE PROCEDURE [dbo].[usp_AccesoUsuario_FindByUsuario]
@UidUsuario uniqueidentifier,
@UidNivelAcceso uniqueidentifier = null
AS

SET NOCOUNT ON

SELECT * FROM Modulo WHERE
(@UidNivelAcceso IS NULL OR UidNivelAcceso = @UidNivelAcceso) AND
UidModulo IN (SELECT UidModulo FROM AccesoUsuario WHERE UidUsuario = @UidUsuario)
