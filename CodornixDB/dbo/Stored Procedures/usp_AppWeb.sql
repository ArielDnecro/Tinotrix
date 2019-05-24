CREATE PROCEDURE [dbo].[usp_AppWeb]
@UidPerfil uniqueidentifier
AS

SET NOCOUNT ON

SELECT VchNivelAcceso FROM NivelAcceso JOIN Perfil ON NivelAcceso.UidNivelAcceso = Perfil.UidNivelAcceso WHERE Perfil.UidPerfil = @UidPerfil
