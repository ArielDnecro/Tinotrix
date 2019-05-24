-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_ConsultarPerfil]
	-- Add the parameters for the stored procedure here
	@UidPerfil uniqueidentifier
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	select Perfil.*, mo.VchURL as VchHome, NivelAcceso.VchNivelAcceso FROM Perfil 
	INNER JOIN NivelAcceso On Perfil.UidNivelAcceso = NivelAcceso.UidNivelAcceso
	LEFT JOIN Modulo mo on mo.UidModulo =Perfil.UidHome
 WHERE UidPerfil = @UidPerfil


END
