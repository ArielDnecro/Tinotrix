-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_PerfilBackside]
	-- Add the parameters for the stored procedure here
	--@VchNivelAcceso nvarchar(4000)= null
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	select * from Perfil as p
	INNER JOIN NivelAcceso as NA on NA.UidNivelAcceso = p.UidNivelAcceso
	where
	 p.VchPerfil not like 'SuperAdministrador'
	 and NA.VchNivelAcceso not like 'Frontend'
	
	order by IntJerarquia
END
