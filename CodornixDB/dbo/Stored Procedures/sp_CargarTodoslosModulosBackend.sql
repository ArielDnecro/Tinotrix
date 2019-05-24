-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE sp_CargarTodoslosModulosBackend
	-- Add the parameters for the stored procedure here
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT M.*, Na.* FROM Modulo as M
	INNER JOIN NivelAcceso as Na on na.UidNivelAcceso=M.UidNivelAcceso
	 WHERE  Na.VchNivelAcceso like'Backend' and BitDenegable = 1
END