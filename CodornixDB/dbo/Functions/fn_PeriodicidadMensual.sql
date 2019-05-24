CREATE   FUNCTION [dbo].[fn_PeriodicidadMensual] (@UidPeriodicidad uniqueidentifier, @DtFecha date)
RETURNS date
AS
BEGIN

	DECLARE @diff int,
			@freq int,
			@DtStart date,
			@DtEnd date,
			@IntDiasSemana int,
			@IntDiasMes int,
			@DtTmp date;

	SELECT
		@freq = IntFrecuencia,
		@DtStart = DtFechaInicio
	FROM Periodicidad WHERE UidPeriodicidad = @UidPeriodicidad

	SELECT
		@IntDiasMes = IntDiasMes,
		@IntDiasSemana = IntDiasSemana
	FROM PeriodicidadMensual WHERE UidPeriodicidad = @UidPeriodicidad

	SET @diff = DATEDIFF(month, @DtStart, @DtFecha) % @freq

	IF @diff = 0
	BEGIN
		IF (@IntDiasSemana = 0 OR @IntDiasSemana IS NULL) AND DATEPART(day, @DtFecha) < @IntDiasMes
		BEGIN
			IF @IntDiasMes > DATEPART(day, EOMONTH(@DtFecha))
				SET @IntDiasMes = DATEPART(day, EOMONTH(@DtFecha))
			RETURN CAST(DATEPART(year, @DtFecha) AS nvarchar(4)) + '-' + CAST(DATEPART(month, @DtFecha) AS nvarchar(2)) + '-' + CAST(@IntDiasMes AS nvarchar(2))
		END
		ELSE
		BEGIN
			IF @IntDiasMes = -1
			BEGIN
				SET @DtTmp = EOMONTH(@DtFecha)
				IF @DtFecha < @DtTmp
				BEGIN
					SET @DtEnd = @DtTmp
					SET @DtTmp = DATEADD(day, -7, @DtEnd)
					IF (@DtTmp <= @DtFecha)
						SET @DtTmp = DATEADD(day, 1, @DtFecha)
					WHILE @DtTmp <= @DtEnd
					BEGIN
						IF (@IntDiasSemana = DATEPART(weekday, @DtTmp))
							RETURN @DtTmp
						SET @DtTmp = DATEADD(day, 1, @DtTmp)
					END
				END
			END
			IF (@IntDiasMes * 7) > DATEPART(day, @DtFecha)
			BEGIN
				SET @DtEnd = DATEADD(day, (@IntDiasMes * 7) + 1, EOMONTH(@DtFecha, -1))
				SET @DtTmp = DATEADD(day, -7, @DtEnd)
				IF @DtTmp <= @DtFecha
					SET @DtTmp = DATEADD(day, 1, @DtFecha)
				WHILE @DtTmp <= @DtEnd
				BEGIN
					IF (@IntDiasSemana = DATEPART(weekday, @DtTmp))
						RETURN @DtTmp
					SET @DtTmp = DATEADD(day, 1, @DtTmp)
				END
			END
		END
	END

	SET @DtStart = DATEADD(day, 1, EOMONTH(DATEADD(month, (@freq - @diff), @DtFecha), -1))
	IF (@IntDiasSemana = 0 OR @IntDiasSemana IS NULL)
	BEGIN
		IF @IntDiasMes > DATEPART(day, EOMONTH(@DtStart))
			SET @IntDiasMes = DATEPART(day, EOMONTH(@DtStart))
		RETURN CAST(DATEPART(year, @DtStart) AS nvarchar(4)) + '-' + CAST(DATEPART(month, @DtStart) AS nvarchar(2)) + '-' + CAST(@IntDiasMes AS nvarchar(2))
	END
	ELSE
	BEGIN
		IF @IntDiasMes = -1
		BEGIN
			SET @DtTmp = EOMONTH(@DtStart)
			SET @DtEnd = @DtTmp
			SET @DtTmp = DATEADD(day, -7, @DtEnd)
			IF (@DtTmp <= @DtFecha)
				SET @DtTmp = DATEADD(day, 1, @DtStart)
			WHILE @DtTmp <= @DtEnd
			BEGIN
				IF (@IntDiasSemana = DATEPART(weekday, @DtTmp))
					RETURN @DtTmp
				SET @DtTmp = DATEADD(day, 1, @DtTmp)
			END
			
		END
		ELSE
		BEGIN
			SET @DtEnd = DATEADD(day, (@IntDiasMes * 7) + 1, EOMONTH(@DtStart, -1))
			SET @DtTmp = DATEADD(day, -7, @DtEnd)
			IF @DtTmp <= @DtStart
				SET @DtTmp = DATEADD(day, 1, @DtStart)
			WHILE @DtTmp <= @DtEnd
			BEGIN
				IF (@IntDiasSemana = DATEPART(weekday, @DtTmp))
					RETURN @DtTmp
				SET @DtTmp = DATEADD(day, 1, @DtTmp)
			END
		END
	END


	RETURN NULL
END