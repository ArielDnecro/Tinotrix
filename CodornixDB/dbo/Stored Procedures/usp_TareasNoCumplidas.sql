-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[usp_TareasNoCumplidas]
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
	pe.*,e.*,tf.*, tt.*
FROM Tarea t
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
INNER JOIN TipoTarea as tt on t.UidTipoTarea= tt.UidTipoTarea
WHERE
	t.BitCaducado = 0 AND
	e.VchStatus = 'Activo' AND
	u.UidUsuario = @UidUsuario AND
	d.UidSucursal = @UidSucursal AND
	d.UidDepartamento=@UidDepartamento and 
	(p.DtFechaInicio <= @DtFecha AND p.DtFechaFin >= @DtFecha) AND
	(c.UidCumplimiento IS NULL OR (c.DtFechaProgramada = @DtFecha AND ec.VchTipoCumplimiento <> 'Completo') OR 
	(c.DtFechaProgramada = @DtFecha AND CAST(c.DtFechaHora as DATE) > @DtFecha AND ec.VchTipoCumplimiento = 'Completo'))

	--(c.UidCumplimiento IS NULL OR c.DtFechaProgramada = @DtFecha OR
		--(c.DtFechaProgramada < @DtFecha AND ec.VchTipoCumplimiento <> 'Completo') OR
		--(CAST(c.DtFechaHora AS date) = @DtFecha))
		
END