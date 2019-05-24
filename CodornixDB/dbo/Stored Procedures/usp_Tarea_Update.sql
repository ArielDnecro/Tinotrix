-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE usp_Tarea_Update 
	-- Add the parameters for the stored procedure here
	@UidTarea uniqueidentifier,
	@VchNombre nvarchar(50),
	@VchDescripcion nvarchar(50),
	@UidAntecesorTarea uniqueidentifier,
	@UidUnidadMedida uniqueidentifier,
	@UidPeriodicidad uniqueidentifier
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	update Tarea set VchNombre= @VchNombre,
	VchDescripcion= @VchDescripcion,
	UidAntecesorTarea= @UidAntecesorTarea,
	UidUnidadMedida= @UidUnidadMedida,
	UidPeriodicidad= @UidPeriodicidad
	where UidTarea=@UidTarea
END