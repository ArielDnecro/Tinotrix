CREATE   PROCEDURE [dbo].[usp_Cumplimiento_Do]
@UidTarea uniqueidentifier,
@UidDepartamento uniqueidentifier = null,
@UidArea uniqueidentifier = null,
@UidUsuario uniqueidentifier,
@UidCumplimiento uniqueidentifier = null,
@DtFechaHora datetime,
@VchObservacion nvarchar(200) = null,
@UrlFoto nvarchar(50) = null,
@BitValor bit = null,
@DcValor1 decimal(18, 4) = null,
@DcValor2 decimal(18, 4) = null,
@UidOpcion uniqueidentifier = null,
@UidTurno uniqueidentifier
AS

SET NOCOUNT ON

DECLARE @UidEC uniqueidentifier, @UidNO uniqueidentifier, @DtSiguiente date, @UidPeriodicidad uniqueidentifier;

SELECT @UidEC = UidEstadoCumplimiento FROM EstadoCumplimiento WHERE VchTipoCumplimiento = 'Completo';
SELECT @UidNO = UidEstadoCumplimiento FROM EstadoCumplimiento WHERE VchTipoCumplimiento = 'No Realizado';

IF @UidCumplimiento IS NULL
BEGIN
	-- Create the current clumplieminto
	EXECUTE usp_Cumplimiento_Add @UidTarea = @UidTarea, @UidDepartamento = @UidDepartamento,
		@UidArea = @UidArea, @UidUsuario = @UidUsuario, @DtFechaHora = @DtFechaHora,
		@DtFechaProgramada = @DtFechaHora,
		@UidEstadoCumplimiento = @UidEC, @VchObservacion = @VchObservacion,
		@UrlFoto = @UrlFoto, @BitValor = @BitValor, @DcValor1 = @DcValor1, @DcValor2 = @DcValor2,
		@UidOpcion = @UidOpcion, @UidTurno = @UidTurno;

END
ELSE
	UPDATE Cumplimiento
	SET
		UidEstadoCumplimiento = @UidEC,
		UidUsuario = @UidUsuario,
		DtFechaHora = @DtFechaHora,
		VchObservacion = @VchObservacion,
		BitValor = @BitValor,
		DcValor1 = @DcValor1,
		DcValor2 = @DcValor2,
		UidOpcion = @UidOpcion,
		UidTurno = @UidTurno
	WHERE UidCumplimiento = @UidCumplimiento

SELECT @UidPeriodicidad = UidPeriodicidad FROM Tarea WHERE UidTarea = @UidTarea;

-- Create the next
SELECT @DtSiguiente = dbo.f_ObtenerPeriodicidad(@UidPeriodicidad, @DtFechaHora)

EXECUTE usp_Cumplimiento_Add @UidTarea = @UidTarea, @UidDepartamento = @UidDepartamento, @UidArea = @UidArea,
	@UidUsuario = null, @DtFechaHora = null, @DtFechaProgramada = @DtSiguiente, @UidEstadoCumplimiento = @UidNO,
	@VchObservacion = null, @DcValor1 = null, @DcValor2 = null, @UidOpcion = null, @UrlFoto = null, @UidTurno = null;