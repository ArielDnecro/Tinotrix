CREATE   PROCEDURE [dbo].[usp_AsignacionSupervision_Last]
@UidDepartamento uniqueidentifier,
@UidTurno uniqueidentifier
AS

SET NOCOUNT ON

SELECT TOP(1) * FROM AsignacionSupervision WHERE
UidDepartamento = @UidDepartamento AND
UidTurno = @UidTurno
ORDER BY DtFechaFin DESC