CREATE   FUNCTION fn_PeriodicidadDiaria (@UidPeriodicidad uniqueidentifier, @DtFecha date)
RETURNS date
AS
BEGIN

	DECLARE @frecuencia int,
			@diff int,
			@DtFechaInicio date,
			@DtResult date;

	SELECT @frecuencia = IntFrecuencia, @DtFechaInicio = DtFechaInicio FROM Periodicidad WHERE UidPeriodicidad = @UidPeriodicidad

	SET @diff = DATEDIFF(day, @DtFechaInicio, @DtFecha) % @frecuencia

	SET @DtResult = DATEADD(day, (@frecuencia - @diff), @DtFecha)  

	RETURN @DtResult
END