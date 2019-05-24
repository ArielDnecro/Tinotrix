-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[usp_TareaArea_Add]
	-- Add the parameters for the stored procedure here
	@UidTarea uniqueidentifier,
	@UidArea uniqueidentifier
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	insert into TareaArea( UidArea, UidTarea) values(@UidArea, @UidTarea)
END