CREATE PROCEDURE [dbo].[usp_Cumplimiento_NumeroTareas]
@UidUsuario uniqueidentifier,
@UidSucursal uniqueidentifier,
@DtFecha date
AS

SET NOCOUNT ON

SELECT
	d.UidDepartamento,
	d.VchNombre AS VchDepartamento,
	a.UidArea,
	a.VchNombre AS VchArea,
	u.UidUsuario,
	u.VchNombre + ' ' + u.VchApellidoPaterno + ' ' + u.VchApellidoMaterno AS VchUsuario, 
	COUNT(CASE WHEN ec.VchTipoCumplimiento = 'Completo' AND CAST(c.DtFechaHora AS date) = @DtFecha THEN 1 END) AS NumTareasCumplidas,
	COUNT(CASE WHEN ec.VchTipoCumplimiento <> 'Completo' OR ec.UidEstadoCumplimiento IS NULL  THEN 1 END) AS NumTareasNoCumplidas,
	COUNT(CASE WHEN (ec.VchTipoCumplimiento <> 'Completo' OR ec.UidEstadoCumplimiento IS NULL) AND tt.VchTipoTarea = 'Requerida' THEN 1 END) AS NumTareasRequeridasNoCumplidas,
	@DtFecha AS DtFecha,
	tu.VchTurno AS VchTurno
FROM Tarea t
INNER JOIN TipoTarea tt ON t.UidTipoTarea = tt.UidTipoTarea 
INNER JOIN Estatus es ON t.UidStatus = es.UidStatus 
LEFT JOIN TareaArea ta ON t.UidTarea = ta.UidTarea
LEFT JOIN Area a ON ta.UidArea = a.UidArea
LEFT JOIN DepartamentoTarea dt ON t.UidTarea = dt.UidTarea
INNER JOIN Departamento d ON d.UidDepartamento = a.UidDepartamento OR d.UidDepartamento = dt.UidDepartamento
INNER JOIN Periodo p ON d.UidDepartamento = p.UidDepartamento
INNER JOIN Usuario u ON u.UidUsuario = p.UidUsuario
INNER JOIN Turno AS tu ON p.UidTurno = tu.UidTurno
LEFT JOIN Cumplimiento c ON t.UidTarea = c.UidTarea AND (c.UidTurno = tu.UidTurno OR c.UidTurno IS NULL) AND (c.UidDepartamento = dt.UidDepartamento OR c.UidArea = ta.UidArea) 
LEFT JOIN EstadoCumplimiento ec ON ec.UidEstadoCumplimiento = c.UidEstadoCumplimiento
WHERE 
	t.BitCaducado = 0 AND
	es.VchStatus = 'Activo' AND
	u.UidUsuario = @UidUsuario AND
	d.UidSucursal = @UidSucursal AND
	(p.DtFechaInicio <= @DtFecha AND p.DtFechaFin >= @DtFecha) AND
	(c.UidCumplimiento IS NULL OR DtFechaProgramada = @DtFecha OR
		(c.DtFechaProgramada <= @DtFecha AND ec.VchTipoCumplimiento <> 'Completo' AND c.UidTurno IS NULL) OR
		(c.DtFechaProgramada =  @DtFecha AND c.DtFechaHora > @DtFecha AND ec.VchTipoCumplimiento = 'Completo' AND c.UidTurno = tu.UidTurno) OR
		(CAST(c.DtFechaHora AS date) = @DtFecha) AND c.UidTurno = tu.UidTurno)
GROUP BY d.UidDepartamento, d.VchNombre, u.UidUsuario, u.VchNombre, u.VchApellidoPaterno, u.VchApellidoMaterno, a.UidArea, a.VchNombre, tu.VchTurno