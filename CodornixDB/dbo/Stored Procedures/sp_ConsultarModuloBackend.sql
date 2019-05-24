-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE sp_ConsultarModuloBackend
	-- Add the parameters for the stored procedure here
	@UidPerfil uniqueidentifier
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

   SELECT M.*, Na.* FROM Modulo as M
	INNER JOIN NivelAcceso as Na on na.UidNivelAcceso=M.UidNivelAcceso
	
	WHERE Na.VchNivelAcceso='Backside' and  BitDenegable = 1 AND  UidModulo IN (SELECT UidModulo FROM Acceso WHERE UidPerfil = @UidPerfil)
END