CREATE   FUNCTION fn_PeriodicidadSemanal (@UidPeriodicidad uniqueidentifier, @DtFecha date)
RETURNS date
AS
BEGIN
	DECLARE @frecuencia int,
			@diff int,
			@DtFechaInicio date,
			@DtResult date,
			@BitLunes bit,
			@BitMartes bit,
			@BitMiercoles bit,
			@BitJueves bit,
			@BitViernes bit,
			@BitSabado bit,
			@BitDomingo bit,
			@IntDayofWeek int,
			@EndDate date;

	SELECT
			@BitLunes = BitLunes, 
			@BitMartes = BitMartes,
			@BitMiercoles = BitMiercoles,
			@BitJueves = BitJueves,
			@BitViernes = BitViernes,
			@BitSabado = BitSabado,
			@BitDomingo = BitDomingo
		FROM PeriodicidadSemanal WHERE UidPeriodicidad = @UidPeriodicidad
	SELECT @frecuencia = IntFrecuencia, @DtFechaInicio = DtFechaInicio FROM Periodicidad WHERE UidPeriodicidad = @UidPeriodicidad

	SET @diff = DATEDIFF(day, @DtFechaInicio, @DtFecha) % @frecuencia

	IF @diff = 0
	BEGIN
		SET @EndDate = DATEADD(day, 1, @DtFecha)
		WHILE DATEDIFF(week, @DtFecha, @EndDate) = 0
		BEGIN
			SET @IntDayofWeek = DATEPART(dw, @EndDate)
		
			-- Check if the date's day of week with the selected day.
			IF (@IntDayofWeek = 1 AND @BitDomingo = 1) OR (@IntDayofWeek = 2 AND @BitLunes = 1) OR
				(@IntDayofWeek = 3 AND @BitMartes = 1) OR (@IntDayofWeek = 4 AND @BitMiercoles = 1) OR
				(@IntDayofWeek = 5 AND @BitJueves = 1) OR (@IntDayofWeek = 6 AND @BitViernes = 1) OR
				(@IntDayofWeek = 7 AND @BitSabado = 1)
			
				RETURN @EndDate

			SET @EndDate = DATEADD(day, 1, @EndDate)
		END
	END

	SET @DtFecha = DATEADD(week, (@frecuencia - @diff) , DATEADD(day, 1 - DATEPART(weekday, @DtFecha), @DtFecha)) 
	BEGIN
		SET @EndDate = DATEADD(week, 1, @DtFecha)
		WHILE DATEDIFF(week, @DtFecha, @EndDate) = 1
		BEGIN
			SET @IntDayofWeek = DATEPART(dw, @DtFecha)
		
			-- Check if the date's day of week with the selected day.
			IF (@IntDayofWeek = 1 AND @BitDomingo = 1) OR (@IntDayofWeek = 2 AND @BitLunes = 1) OR
				(@IntDayofWeek = 3 AND @BitMartes = 1) OR (@IntDayofWeek = 4 AND @BitMiercoles = 1) OR
				(@IntDayofWeek = 5 AND @BitJueves = 1) OR (@IntDayofWeek = 6 AND @BitViernes = 1) OR
				(@IntDayofWeek = 7 AND @BitSabado = 1)
			
				RETURN @EndDate
			
			SET @DtFecha = DATEADD(day, 1, @DtFecha)
		END
	END


	RETURN NULL
END