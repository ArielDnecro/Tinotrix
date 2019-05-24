-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[usp_ConsultarInicioTurno]
	-- Add the parameters for the stored procedure here
	@DtFecha date,
	@UidUsuario uniqueidentifier,
	@UidPeriodo uniqueidentifier
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	select * from InicioTurno where cast(DtFechaHoraInicio as date )=@DtFecha and UidUsuario=@UidUsuario and UidPeriodo=@UidPeriodo
END