CREATE   FUNCTION f_PeriodicidadRango(@UidPeriodicidad uniqueidentifier, @DtFechaInicio date, @DtFechaFin date)
RETURNS bit

BEGIN
	WHILE @DtFechaInicio <= @DtFechaFin
	BEGIN
		IF dbo.f_Periodicidad(@UidPeriodicidad, @DtFechaInicio) = 1
			RETURN 1;
		SET @DtFechaInicio = DATEADD(day, 1, @DtFechaInicio)
	END

	RETURN 0
END