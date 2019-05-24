-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[usp_IniciarTurno_Add]
	-- Add the parameters for the stored procedure here
	@UidInicioTurno uniqueidentifier OUTPUT,
	@UidUsuario uniqueidentifier,
	@DtFechaHoraInicio datetime, 
	@UidPeriodo uniqueidentifier
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	set @UidInicioTurno = NEWID();
	insert into InicioTurno(UidInicioTurno,UidUsuario, DtFechaHoraInicio, UidPeriodo)values(@UidInicioTurno,@UidUsuario,@DtFechaHoraInicio,@UidPeriodo)
END