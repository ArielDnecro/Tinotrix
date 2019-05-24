-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[usp_ModificarTarea] 
	-- Add the parameters for the stored procedure here
	@UidTarea uniqueidentifier,
	@VchNombre nvarchar(50),
	@VchDescripcion nvarchar(200),
	@TmHora time = null,
	@IntTolerancia int = null,
	@UidTipoTarea uniqueidentifier,
	@UidStatus uniqueidentifier,
	@BitFoto bit
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	update Tarea set VchNombre= @VchNombre,
	VchDescripcion=@VchDescripcion,
	TmHora=@TmHora,
	IntTolerancia=@IntTolerancia,
	UidTipoTarea = @UidTipoTarea,
	UidStatus= @UidStatus,
	BitFoto= @BitFoto
	where UidTarea=@UidTarea
END