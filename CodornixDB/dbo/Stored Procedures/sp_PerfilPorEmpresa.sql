-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_PerfilPorEmpresa] 
	-- Add the parameters for the stored procedure here
	@UidEmpresa uniqueidentifier
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	select * from Perfil as p
	INNER JOIN NivelAcceso as NA on NA.UidNivelAcceso = p.UidNivelAcceso
	where NA.VchNivelAcceso='FrontEnd'
	and ((@UidEmpresa IS NOT NULL AND UidEmpresa = @UidEmpresa) OR @UidEmpresa IS NULL)
	 or p.VchPerfil ='Encargado' or p.VchPerfil ='Empleado'
	 or p.VchPerfil ='Supervisor'
	order by IntJerarquia
END
