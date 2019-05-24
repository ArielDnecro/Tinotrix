-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE usp_ModificarCaducado 
	-- Add the parameters for the stored procedure here
	@UidTarea uniqueidentifier,
	@BitCaducado bit
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	UPDATE Tarea SET BitCaducado = @BitCaducado WHERE UidTarea = @UidTarea
END