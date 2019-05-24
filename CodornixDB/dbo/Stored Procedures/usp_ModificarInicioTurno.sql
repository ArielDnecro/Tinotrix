-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[usp_ModificarInicioTurno]
	-- Add the parameters for the stored procedure here
	@UidInicioTurno  uniqueidentifier,
	@DtFechaHoraFin datetime,
	@IntNoCumplido int = null
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	update InicioTurno set 
	DtFechaHoraFin = @DtFechaHoraFin,
	IntNoCompletado = @IntNoCumplido
	where UidInicioTurno=@UidInicioTurno
END