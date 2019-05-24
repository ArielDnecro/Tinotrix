CREATE PROCEDURE [dbo].[usp_Revision_ListByUser]
@UidUsuario uniqueidentifier,
@UidSucursal uniqueidentifier,
@DtFecha date
AS

SET NOCOUNT ON

SELECT
	r.UidRevision,
	c.UidCumplimiento,
	t.VchNombre AS VchTarea,
	d.VchNombre AS VchDepartamento,
	a.VchNombre AS VchArea,
	c.DtFechaHora AS DtFechaCumplimiento,
	CASE WHEN r.UidRevision IS NULL THEN 'No Revisado' ELSE 'Revisado' END AS VchEstado
FROM Cumplimiento c
INNER JOIN Tarea t ON c.UidTarea = t.UidTarea
LEFT JOIN Area a ON c.UidArea = c.UidArea
INNER JOIN Departamento d ON  (d.UidDepartamento = a.UidDepartamento OR d.UidDepartamento = c.UidDepartamento)
INNER JOIN AsignacionSupervision ap ON ap.UidDepartamento = d.UidDepartamento
LEFT JOIN Revision r ON r.UidCumplimiento = c.UidCumplimiento
WHERE
	ap.UidUsuario = @UidUsuario AND
	d.UidSucursal = @UidSucursal AND
	CAST(c.DtFechaHora AS DATE) = @DtFecha