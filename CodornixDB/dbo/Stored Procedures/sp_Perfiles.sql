-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_Perfiles]
	-- Add the parameters for the stored procedure here
	@VchPerfil nvarchar(50)= null,
	@UidNivelAcceso nvarchar(4000)=NULL,
	@VchNivelAcceso nvarchar(4000)= null
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	select p.*, na.VchNivelAcceso from Perfil as p 
	INNER JOIN NivelAcceso AS na ON p.UidNivelAcceso = na.UidNivelAcceso
	where
	na.VchNivelAcceso not LIKE 'FrontEnd' and p.VchPerfil not like 'SuperAdministrador' and
	((@VchPerfil IS NOT NULL AND VchPerfil LIKE '%' + @VchPerfil  + '%') OR @VchPerfil IS NULL)
	and ((@UidNivelAcceso is not null and p.UidNivelAcceso in( select * from CSVtoTable(@UidNivelAcceso, ',')))OR(@UidNivelAcceso is null))

END
