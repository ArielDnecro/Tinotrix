-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[usp_TareaTelefono_Buscar]
	-- Add the parameters for the stored procedure here
	@UidTarea uniqueidentifier
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT Opciones.* FROM Opciones 
WHERE Opciones.UidTarea = @UidTarea
END