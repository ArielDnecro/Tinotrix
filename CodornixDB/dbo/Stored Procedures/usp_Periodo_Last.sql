CREATE PROCEDURE usp_Periodo_Last
@UidDepartamento uniqueidentifier,
@UidTurno uniqueidentifier
AS

SET NOCOUNT ON

SELECT TOP(1) * FROM Periodo WHERE
UidDepartamento = @UidDepartamento AND
UidTurno = @UidTurno
ORDER BY DtFechaFin DESC