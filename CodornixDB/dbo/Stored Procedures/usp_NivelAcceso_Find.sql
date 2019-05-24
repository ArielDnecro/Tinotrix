CREATE PROCEDURE [dbo].[usp_NivelAcceso_Find]
@UidNivelAcceso uniqueidentifier
AS

SET NOCOUNT ON

SELECT * FROM NivelAcceso WHERE UidNivelAcceso = @UidNivelAcceso