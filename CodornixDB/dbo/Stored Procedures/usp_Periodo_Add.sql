CREATE PROCEDURE [dbo].[usp_Periodo_Add]
	@DtFechaInicio date,
	@DtFechaFin date,
	@UidTurno uniqueidentifier,
	@UidUsuario uniqueidentifier,
	@UidDepartamento uniqueidentifier = null,
	@UidArea uniqueidentifier = null,
	@UidPeriodo uniqueidentifier OUTPUT,
	@IntResult int OUTPUT
AS

/*
 * Define result constants:
 *     (0) Successful adding without modyfing anything.
 *     (1) Successful adding with date updates on undefined end asignations.
 *     (2) Successful update of already asignation (same person, same depto. or area, same turn, other end date).
 *    (-1) Failure on adding, same asignation alreayd exists (same all fields).
 *    (-2) Failure on adding, number of max asignations already reached.
 *    (-3) Failure on adding, the start date isn't the next day to last assignement.
 */

DECLARE @maxAsigns tinyint,
		@ExistsEntry uniqueidentifier,
		@Status tinyint,
		@LastDate date,
		@currentAsigns tinyint;

SET NOCOUNT OFF -- Enable counting for this procedure

-- Set Guid to zero
SET @UidPeriodo = '00000000-0000-0000-0000-000000000000'

SELECT @maxAsigns = IntMaxAsignaciones FROM Encargado WHERE UidUsuario = @UidUsuario;

IF @maxAsigns IS NULL -- Only one asignation by default.
	SET @maxAsigns = 1;
	
-- Check overlapping assignations

SELECT @ExistsEntry = UidPeriodo FROM Periodo WHERE UidUsuario = @UidUsuario AND UidDepartamento = @UidDepartamento AND UidTurno = @UidTurno AND DtFechaInicio = DtFechaInicio; 

IF (@ExistsEntry IS NOT NULL)
BEGIN
	SELECT @Status = 1 FROM Periodo WHERE UidPeriodo = @ExistsEntry AND DtFechaFin >= @DtFechaFin; 

	IF (@Status = 1 OR @DtFechaFin IS NULL)
	BEGIN
		SET @IntResult = -1;
		RETURN;
	END;

	ELSE
	BEGIN
		UPDATE Periodo SET DtFechaFin = @DtFechaFin WHERE UidPeriodo = @ExistsEntry;
		SET @UidPeriodo = @ExistsEntry;
		SET @IntResult = 2;
		RETURN;
	END;

END;

SELECT @currentAsigns = COUNT(UidUsuario) FROM Periodo WHERE UidUsuario = @UidUsuario AND (DtFechaFin > GETDATE() OR DtFechaFin IS NULL);

-- They are already four assignations, cancel operation.
IF (@currentAsigns >= @maxAsigns) 
BEGIN
	SET @IntResult = -2;
	RETURN
END;

-- If the new assignation produces leakes between dates, cancel it.
--SELECT TOP(1) @LastDate = DtFechaFin FROM Periodo WHERE UidDepartamento = @UidDepartamento AND UidTurno = @UidTurno AND 

ELSE
BEGIN
	SET @UidPeriodo = NEWID();
	INSERT INTO Periodo (UidPeriodo, DtFechaInicio, DtFechaFin, UidUsuario, UidDepartamento, UidTurno)
		VALUES  (@UidPeriodo, @DtFechaInicio, @DtFechaFin, @UidUsuario, @UidDepartamento, @UidTurno);
	SET @IntResult = 0;
	RETURN
END