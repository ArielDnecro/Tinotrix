-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_ModuloPorAcceso] 
	-- Add the parameters for the stored procedure here
	
	@VchNivelAcceso nvarchar(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	select Modulo.* from Modulo
	INNER JOIN NivelAcceso as NA on NA.UidNivelAcceso= Modulo.UidNivelAcceso
	where VchNivelAcceso=@VchNivelAcceso

END
