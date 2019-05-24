-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[usp_Tarea_Add] 
	-- Add the parameters for the stored procedure here
	@UidTarea uniqueidentifier OUTPUT,
	@VchNombre nvarchar(50),
	@VchDescripcion nvarchar(200),
	@UidAntecesorTarea uniqueidentifier= null,
	@UidUnidadMedida uniqueidentifier = null,
	@UidPeriodicidad uniqueidentifier,
	@UidTipoMedicion uniqueidentifier= null,
	@TmHora time= null,
	@IntTolerancia int= null,
	@UidTipoTarea uniqueidentifier,
	@UidStatus uniqueidentifier,
	@BitFoto bit,
	@BitCaducado  bit
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	set @UidTarea=NEWID();
    -- Insert statements for procedure here
	INSERT INTO Tarea(UidTarea, VchNombre,VchDescripcion, UidAntecesorTarea,UidUnidadMedida,
	UidPeriodicidad,UidTipoMedicion, TmHora, IntTolerancia, UidTipoTarea, UidStatus, BitFoto, BitCaducado)
	Values (@UidTarea, @VchNombre, @VchDescripcion,@UidAntecesorTarea,
	@UidUnidadMedida,@UidPeriodicidad,@UidTipoMedicion,@TmHora,@IntTolerancia, @UidTipoTarea, @UidStatus, @BitFoto,@BitCaducado)
END