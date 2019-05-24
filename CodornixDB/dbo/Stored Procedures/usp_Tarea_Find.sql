-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[usp_Tarea_Find]
	-- Add the parameters for the stored procedure here
	@UidTarea uniqueidentifier
	AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	select t.*, tt.*, es.*, m.*,u.* from Tarea as t
	inner join TipoTarea as tt on t.UidTipoTarea=tt.UidTipoTarea
	inner join Estatus as es on t.UidStatus= es.UidStatus
	inner join Medicion as m on t.UidTipoMedicion=m.UidTipoMedicion
 	left join UnidadMedida as u on t.UidUnidadMedida =u.UidUnidadMedida
	where UidTarea=@UidTarea;
END