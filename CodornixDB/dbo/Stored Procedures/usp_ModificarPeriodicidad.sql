-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE usp_ModificarPeriodicidad
	-- Add the parameters for the stored procedure here
	@UidPeriodicidad uniqueidentifier,
	@DtFechaFin date= null
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	update Periodicidad set 
	DtFechaFin=@DtFechaFin
	where UidPeriodicidad=@UidPeriodicidad
END