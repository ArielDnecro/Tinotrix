CREATE PROCEDURE usp_Modulo_FindByNivel
@UidNivelAcceso uniqueidentifier
AS

SET NOCOUNT ON

SELECT * FROM Modulo WHERE BitDenegable = 1 AND UidNivelAcceso = @UidNivelAcceso