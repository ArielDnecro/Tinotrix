-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[usp_TareasCumplidas]
	-- Add the parameters for the stored procedure here
	@UidDepartamento uniqueidentifier,
	@UidUsuario uniqueidentifier,
	@DtFecha date
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	select c.*,ec.*, t.*,e.*,tf.*, tt.* from Tarea as t
	INNER JOIN Cumplimiento AS c on t.UidTarea=c.UidTarea
	INNER JOIN EstadoCumPlimiento as ec on ec.UidEstadoCumplimiento=c.UidEstadoCumplimiento
	INNER JOIN Estatus as e on e.UidStatus=t.UidStatus
	INNER JOIN Periodicidad as p on p.UidPeriodicidad=t.UidPeriodicidad
	INNER JOIN TipoFrecuencia as tf on tf.UidTipoFrecuencia=p.UidTipoFrecuencia
	INNER JOIN TipoTarea as tt on t.UidTipoTarea= tt.UidTipoTarea
	where c.UidDepartamento=@UidDepartamento
	and c.UidUsuario=@UidUsuario
	and 
	ec.vchTipoCumplimiento ='Completo'
	and CAST(c.DtFechaHora as date) = @DtFecha


END