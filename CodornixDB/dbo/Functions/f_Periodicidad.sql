CREATE   FUNCTION [dbo].[f_Periodicidad] (@UidPeriodicidad uniqueidentifier, @DtFecha date)
RETURNS bit
BEGIN
	
	DECLARE @DtFechaInicio date,
			@VchTipoFrecuencia nvarchar(50),
			@IntFrecuencia int,
			@IntDiff int,
			@BitLunes bit,
			@BitMartes bit,
			@BitMiercoles bit,
			@BitJueves bit,
			@BitViernes bit,
			@BitSabado bit,
			@BitDomingo bit,
			@IntDayofWeek int,
			@IntDiasMes int,
			@IntDiasSemana int,
			@DtFechaComputed date,
			@DtFechaInicioComputed date;

	SELECT @VchTipoFrecuencia = t.VchTipoFrecuencia,
		   @IntFrecuencia = IntFrecuencia, 
		   @DtFechaInicio = DtFechaInicio
		FROM TipoFrecuencia t
		INNER JOIN Periodicidad p ON t.UidTipoFrecuencia = p.UidTipoFrecuencia 
		WHERE p.UidPeriodicidad = @UidPeriodicidad

	IF @VchTipoFrecuencia = 'Diaria'
	BEGIN
		IF @IntFrecuencia = 1
			-- Always execute the job, every day.
			RETURN 1	

		-- TODO: Implement non-one version based in the creation date.
		ELSE
		BEGIN
			SET @IntDiff = DATEDIFF(day, @DtFechaInicio, @DtFecha)

			IF (@IntDiff % @IntFrecuencia) = 0
				RETURN 1
		END
	END

	ELSE IF @VchTipoFrecuencia = 'Semanal'
	BEGIN
		SELECT
			@BitLunes = BitLunes, 
			@BitMartes = BitMartes,
			@BitMiercoles = BitMiercoles,
			@BitJueves = BitJueves,
			@BitViernes = BitViernes,
			@BitSabado = BitSabado,
			@BitDomingo = BitDomingo
		FROM PeriodicidadSemanal WHERE UidPeriodicidad = @UidPeriodicidad

		SET @IntDayofWeek = DATEPART(dw, @DtFecha)
		SET @DtFechaComputed = DATEADD(WEEKDAY, 1-DATEPART(WEEKDAY, @DtFecha),@DtFecha)
		SET @DtFechaInicioComputed = DATEADD(WEEKDAY, 1-DATEPART(WEEKDAY, @DtFechaInicio), @DtFechaInicio)

		IF (DATEDIFF(WEEK, @DtFechaInicioComputed, @DtFechaComputed) % @IntFrecuencia) != 0
			RETURN 0

		-- Check if the date's day of week with the selected day.
		IF (@IntDayofWeek = 1 AND @BitDomingo = 1) OR (@IntDayofWeek = 2 AND @BitLunes = 1) OR
			(@IntDayofWeek = 3 AND @BitMartes = 1) OR (@IntDayofWeek = 4 AND @BitMiercoles = 1) OR
			(@IntDayofWeek = 5 AND @BitJueves = 1) OR (@IntDayofWeek = 6 AND @BitViernes = 1) OR
			(@IntDayofWeek = 7 AND @BitSabado = 1)
			
			RETURN 1
	END

	ELSE IF @VchTipoFrecuencia = 'Mensual'
	BEGIN
		SELECT
			@IntDiasMes = IntDiasMes,
			@IntDiasSemana = IntDiasSemana
		FROM PeriodicidadMensual WHERE UidPeriodicidad = @UidPeriodicidad

		SET @DtFechaComputed = DATEADD(day, 1-(DATEPART(day, @DtFecha)), @DtFecha)
		SET @DtFechaInicioComputed = DATEADD(day, 1-(DATEPART(day, @DtFechaInicio)), @DtFechaInicio)
		SET @IntDiff = DATEDIFF(month, @DtFechaInicioComputed, @DtFechaComputed)

		IF @IntDiff % @IntFrecuencia != 0
			RETURN 0

		IF @IntDiasSemana IS NULL
		BEGIN
			IF (DATEPART(day, @DtFecha) = @IntDiasMes)
				RETURN 1
		END
	END

	RETURN 0
END