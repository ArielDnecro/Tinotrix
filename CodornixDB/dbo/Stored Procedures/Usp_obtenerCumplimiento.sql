-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Usp_obtenerCumplimiento]
	-- Add the parameters for the stored procedure here
	@UidTarea uniqueidentifier, 
	@UidUsuario uniqueidentifier
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	select c.*, ec.* from Cumplimiento as c
	JOIN EstadoCumplimiento ec ON c.UidEstadoCumplimiento = ec.UidEstadoCumplimiento
	 where UidTarea=@UidTarea and UidUsuario=@UidUsuario and DtFechaHora  is not null
END