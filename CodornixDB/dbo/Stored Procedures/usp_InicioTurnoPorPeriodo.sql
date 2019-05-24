-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[usp_InicioTurnoPorPeriodo]
	-- Add the parameters for the stored procedure here
	@UidPeriodo uniqueidentifier,
	@UidUsuario uniqueidentifier,
	@DtFecha date
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	select * from InicioTurno where UidPeriodo=@UidPeriodo and UidUsuario=@UidUsuario 
	and CAST( DtFechaHoraInicio as date)=@DtFecha and DtFechaHoraFin is null 
END