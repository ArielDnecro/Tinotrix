-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[usp_ActualizarCumplimiento] 
	-- Add the parameters for the stored procedure here
	@UidTarea uniqueidentifier, 
	@UidTareaAnterior uniqueidentifier
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	update Cumplimiento set UidTarea= @UidTarea
	from Cumplimiento c join EstadoCumplimiento ec on c.UidEstadoCumplimiento=ec.UidEstadoCumplimiento
	where UidTarea=@UidTareaAnterior and ec.VchTipoCumplimiento<>'Completo'
END