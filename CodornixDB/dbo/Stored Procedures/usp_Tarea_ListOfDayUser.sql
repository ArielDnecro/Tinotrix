CREATE   PROCEDURE [dbo].[usp_Tarea_ListOfDayUser]
	@UidSucursal uniqueidentifier,
	@DtFecha date,
	@UidUsuario uniqueidentifier
AS

SET NOCOUNT ON

SELECT t.UidTarea,
	t.VchNombre,
	t.VchDescripcion,
	d.UidDepartamento,
	d.VchNombre AS VchDepartamento,
	c.DtFechaHora AS DtFechaRealizacion
FROM Tarea AS t
INNER JOIN DepartamentoTarea AS dt ON t.UidTarea = dt.UidTarea
INNER JOIN Departamento AS d ON dt.UidDepartamento = d.UidDepartamento
INNER JOIN Periodo AS p ON d.UidDepartamento = p.UidDepartamento
INNER JOIN Cumplimiento AS c ON c.UidTarea = t.UidTarea

WHERE
	c.DtFechaProgramada = @DtFecha AND
	d.UidSucursal = @UidSucursal AND
	p.UidUsuario = @UidUsuario