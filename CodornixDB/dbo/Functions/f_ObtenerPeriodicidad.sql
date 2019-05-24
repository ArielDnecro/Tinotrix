CREATE  FUNCTION [dbo].[f_ObtenerPeriodicidad] (@UidPeriodicidad uniqueidentifier, @DtFecha date)
RETURNS date
BEGIN
	
	DECLARE @DtFechaInicio date,
			@VchTipoFrecuencia nvarchar(50),
			@DtResult date = null;

	SELECT @VchTipoFrecuencia = t.VchTipoFrecuencia
		FROM TipoFrecuencia t
		INNER JOIN Periodicidad p ON t.UidTipoFrecuencia = p.UidTipoFrecuencia 
		WHERE p.UidPeriodicidad = @UidPeriodicidad

	IF @VchTipoFrecuencia = 'Diaria'
	BEGIN
		SELECT @DtResult = dbo.fn_PeriodicidadDiaria(@UidPeriodicidad, @DtFecha);
	END

	ELSE IF @VchTipoFrecuencia = 'Semanal'
	BEGIN
		SELECT @DtResult = dbo.fn_PeriodicidadSemanal(@UidPeriodicidad, @DtFecha);
	END

	ELSE IF @VchTipoFrecuencia = 'Mensual'
	BEGIN
		SELECT @DtResult = dbo.fn_PeriodicidadMensual(@UidPeriodicidad, @DtFecha);
	END

	RETURN @DtResult;
END