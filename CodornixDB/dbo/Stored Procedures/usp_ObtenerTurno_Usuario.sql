-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[usp_ObtenerTurno_Usuario] 
	-- Add the parameters for the stored procedure here
	@UidUsuario uniqueidentifier, 
	@UidTurno uniqueidentifier,
	@DtFecha	date
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	select it.*, p.UidTurno, t.* from InicioTurno as it
	INNER JOIN Periodo as p on it.UidPeriodo=p.UidPeriodo
	INNER JOIN Turno as t on p.UidTurno = t.UidTurno
	where it.UidUsuario=@UidUsuario and p.UidTurno= @UidTurno and CAST(DtFechaHoraInicio as date)=@DtFecha
END