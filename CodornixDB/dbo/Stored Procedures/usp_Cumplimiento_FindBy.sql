CREATE   PROCEDURE usp_Cumplimiento_FindBy
@DtFecha date,
@UidSucursal uniqueidentifier, 
@VchUsuario nvarchar(50) = null,
@VchDepartamentos nvarchar(2000) = null,
@VchTareas nvarchar(2000) = null
AS

SELECT
	t.VchNombre AS VchTarea, 
	d.VchNombre AS VchDepartamento,
	CASE WHEN ec.VchTipoCumplimiento IS NULL THEN 'No realizado' ELSE ec.VchTipoCumplimiento END AS VchTipoCumplimiento,
	c.DtFechaHora,
	CASE WHEN c.UidCumplimiento IS NULL THEN u.VchNombre ELSE uc.VchNombre END AS VchNombreUsuario,
	CASE WHEN c.UidCumplimiento IS NULL THEN u.VchApellidoPaterno ELSE uc.VchApellidoPaterno END AS VchApellidoUsuario
FROM Tarea t
LEFT JOIN Cumplimiento c ON c.UidTarea = t.UidTarea
LEFT JOIN EstadoCumplimiento ec ON c.UidEstadoCumplimiento = ec.UidEstadoCumplimiento
LEFT JOIN Usuario uc ON c.UidUsuario = uc.UidUsuario
INNER JOIN DepartamentoTarea dt ON t.UidTarea = dt.UidTarea
INNER JOIN Departamento d ON dt.UidDepartamento = d.UidDepartamento
INNER JOIN Periodo p ON dt.UidDepartamento = p.UidDepartamento
INNER JOIN Periodicidad pd ON t.UidPeriodicidad = pd.UidPeriodicidad
INNER JOIN Usuario u ON p.UidUsuario = u.UidUsuario

WHERE
	d.UidSucursal = @UidSucursal AND
	(@VchUsuario IS NULL OR (
		u.VchNombre LIKE '%' + @VchUsuario + '%' OR
		u.VchApellidoPaterno LIKE '%' + @VchUsuario + '%' OR
		u.VchApellidoMaterno LIKE '%' + @VchUsuario + '%')) AND
	(@VchDepartamentos IS NULL OR d.UidDepartamento IN (SELECT * FROM CSVtoTable(@VchDepartamentos, ','))) AND
	(@VchTareas IS NULL OR t.VchNombre LIKE '%' + @VchTareas + '%')