-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[usp_TareasRequeridasNoCumplidas]
	-- Add the parameters for the stored procedure here
	@UidDepartamento uniqueidentifier,
	@UidUsuario uniqueidentifier,
	@DtFecha date,
	@UidSucursal uniqueidentifier
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	/*select c.*,ec.*, t.*,e.*,tf.* from Tarea as t
	INNER JOIN Cumplimiento AS c on t.UidTarea=c.UidTarea
	INNER JOIN EstadoCumPlimiento as ec on ec.UidEstadoCumplimiento=c.UidEstadoCumplimiento
	INNER JOIN Estatus as e on e.UidStatus=t.UidStatus
	INNER JOIN Periodicidad as p on p.UidPeriodicidad=t.UidPeriodicidad
	INNER JOIN TipoFrecuencia as tf on tf.UidTipoFrecuencia=p.UidTipoFrecuencia
	INNER JOIN TipoTarea as tt on tt.UidTipoTarea=t.UidTipoTarea
	where c.UidDepartamento=@UidDepartamento
	and c.UidUsuario=@UidUsuario
	and 
	ec.VchTipoCumplimiento not like 'Completo'
	and c.DtFechaProgramada=@DtFecha
	and tt.VchTipoTarea like 'Requerida'*/

	select 
	t.UidTarea,
	d.UidDepartamento,
	a.UidArea,
	c.UidCumplimiento,
	t.VchNombre AS VchTarea,
	d.VchNombre AS VchDepartamento,
	a.VchNombre AS VchArea,
	t.TmHora,
	c.DtFechaHora,
	ec.VchTipoCumplimiento,
	pe.*,e.*,tf.*
FROM Tarea t
INNER JOIN Estatus es ON t.UidStatus = es.UidStatus 
LEFT JOIN TareaArea ta ON t.UidTarea = ta.UidTarea
LEFT JOIN Area a ON ta.UidArea = a.UidArea
LEFT JOIN DepartamentoTarea dt ON t.UidTarea = dt.UidTarea
INNER JOIN Departamento d ON d.UidDepartamento = a.UidDepartamento OR d.UidDepartamento = dt.UidDepartamento
INNER JOIN Periodo p ON d.UidDepartamento = p.UidDepartamento
INNER JOIN Usuario u ON u.UidUsuario = p.UidUsuario
LEFT JOIN Cumplimiento c ON t.UidTarea = c.UidTarea AND (c.UidDepartamento = dt.UidDepartamento OR c.UidArea = ta.UidArea) 
LEFT JOIN EstadoCumplimiento ec ON ec.UidEstadoCumplimiento = c.UidEstadoCumplimiento
INNER JOIN Estatus as e on e.UidStatus=t.UidStatus
INNER JOIN Periodicidad as pe on pe.UidPeriodicidad=t.UidPeriodicidad
INNER JOIN TipoFrecuencia as tf on tf.UidTipoFrecuencia=pe.UidTipoFrecuencia
INNER JOIN TipoTarea as tt on tt.UidTipoTarea=t.UidTipoTarea
WHERE
	t.BitCaducado = 0 AND
	es.VchStatus = 'Activo' AND
	u.UidUsuario = @UidUsuario AND
	d.UidSucursal = @UidSucursal AND
	tt.VchTipoTarea like 'Requerida' and 
	d.UidDepartamento=@UidDepartamento and 
	(p.DtFechaInicio <= @DtFecha AND p.DtFechaFin >= @DtFecha) AND
	(c.UidCumplimiento IS NULL OR c.DtFechaProgramada = @DtFecha OR
		(c.DtFechaProgramada < @DtFecha AND ec.VchTipoCumplimiento <> 'Completo') OR
		(CAST(c.DtFechaHora AS date) = @DtFecha))


END