CREATE   PROCEDURE [dbo].[usp_Tarea_ListOfDay]
	@UidSucursal uniqueidentifier,
	@DtFecha date,
	@UidDepartamento uniqueidentifier = null
AS

SET NOCOUNT ON

SELECT
	t.UidTarea,
	t.VchNombre,
	t.VchDescripcion,
	d.UidDepartamento,
	d.VchNombre AS VchDepartamento,
	pd.UidPeriodicidad,
	tf.VchTipoFrecuencia AS VchFrecuencia,
	u.VchNombre AS VchUsuarioNombre,
	u.VchApellidoPaterno,
	pd.DtFechaInicio 
FROM Tarea AS t
INNER JOIN Periodicidad AS pd ON t.UidPeriodicidad = pd.UidPeriodicidad
INNER JOIN TipoFrecuencia AS tf ON pd.UidTipoFrecuencia = tf.UidTipoFrecuencia
INNER JOIN DepartamentoTarea AS dt ON t.UidTarea = dt.UidTarea
INNER JOIN Departamento AS d ON dt.UidDepartamento = d.UidDepartamento
INNER JOIN Cumplimiento AS c ON c.UidTarea = t.UidTarea
LEFT JOIN Periodo AS p ON d.UidDepartamento = p.UidDepartamento AND (
		p.DtFechaInicio <= @DtFecha AND
		p.DtFechaFin >= @DtFecha)
LEFT JOIN Usuario AS u ON p.UidUsuario = u.UidUsuario


WHERE
	pd.DtFechaInicio <= @DtFecha AND
	d.UidSucursal = @UidSucursal AND
	(@UidDepartamento IS NULL OR d.UidDepartamento = @UidDepartamento) AND
	c.DtFechaProgramada = @DtFecha