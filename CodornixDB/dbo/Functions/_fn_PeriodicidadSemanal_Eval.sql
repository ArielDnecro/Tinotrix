CREATE   FUNCTION _fn_PeriodicidadSemanal_Eval(@UidPeriodicidad uniqueidentifier, @DtFecha date)
RETURNS bit
AS
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
		
	-- Check if the date's day of week with the selected day.
	IF (@IntDayofWeek = 1 AND @BitDomingo = 1) OR (@IntDayofWeek = 2 AND @BitLunes = 1) OR
		(@IntDayofWeek = 3 AND @BitMartes = 1) OR (@IntDayofWeek = 4 AND @BitMiercoles = 1) OR
		(@IntDayofWeek = 5 AND @BitJueves = 1) OR (@IntDayofWeek = 6 AND @BitViernes = 1) OR
		(@IntDayofWeek = 7 AND @BitSabado = 1)
			
		RETURN 1
		
	RETURN 0
END