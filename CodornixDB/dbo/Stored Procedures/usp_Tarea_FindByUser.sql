CREATE PROCEDURE [dbo].[usp_Tarea_FindByUser]

@UidUsuario uniqueidentifier,
@UidTurno uniqueidentifier = null,
@UidSucursal uniqueidentifier,
@DtFecha date

AS

SELECT 
	t.*, d.UidDepartamento, d.VchNombre AS VchDepartamento, tt.UidTurno, tt.VchTurno
FROM
	Tarea t
INNER JOIN
	Cumplimiento c ON t.UidTarea = c.UidTarea
INNER JOIN
	DepartamentoTarea dt ON dt.UidTarea = t.UidTarea
INNER JOIN
	Departamento d ON d.UidDepartamento = dt.UidDepartamento
INNER JOIN
	Periodo p ON p.UidDepartamento = d.UidDepartamento
INNER JOIN
	Turno tt ON p.UidTurno = tt.UidTurno
WHERE
	p.UidUsuario = @UidUsuario AND
	(@UidTurno IS NULL OR p.UidTurno = @UidTurno) AND
	d.UidSucursal = @UidSucursal AND
	c.DtFechaProgramada = @DtFecha