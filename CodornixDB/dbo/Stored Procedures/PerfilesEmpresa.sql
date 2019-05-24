-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[PerfilesEmpresa] 
	-- Add the parameters for the stored procedure here
	@VchPerfil nvarchar(50)= null,
	@UidEmpresa uniqueidentifier
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	select * from Perfil as p 
	INNER JOIN NivelAcceso AS na ON p.UidNivelAcceso = na.UidNivelAcceso
	where
	na.VchNivelAcceso LIKE 'Frontend'
	and ((@UidEmpresa IS NULL OR UidEmpresa = @UidEmpresa) OR UidEmpresa IS NULL)
	and ((@VchPerfil IS NOT NULL AND VchPerfil LIKE '%' + @VchPerfil  + '%') OR @VchPerfil IS NULL)
	
	
END
