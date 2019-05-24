CREATE PROCEDURE [dbo].[usp_ObtenerHomePerfil] 
	@UidPerfil uniqueidentifier

AS

SET NOCOUNT ON;

SELECT VchURL FROM Modulo INNER JOIN Perfil ON Modulo.UidModulo = Perfil.UidHome WHERE Perfil.UidPerfil = @UidPerfil
