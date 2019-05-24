-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE usp_ConsultarAreaTarea 
	-- Add the parameters for the stored procedure here
	@UidTarea uniqueidentifier
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	select Area.* from Area
	 INNER JOIN TareaArea ON Area.UidArea =TareaArea.UidArea WHERE TareaArea.UidTarea = @UidTarea
END