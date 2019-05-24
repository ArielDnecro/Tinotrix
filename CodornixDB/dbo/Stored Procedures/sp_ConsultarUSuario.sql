-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_ConsultarUSuario]
	-- Add the parameters for the stored procedure here
	@VchUsuario nvarchar(50),
	@VchPassword nvarchar(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	select u.UidUsuario  from Usuario u where
	 u.VchNombre = VchUsuario
	 and u.VchPassword = VchPassword
END

