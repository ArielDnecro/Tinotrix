-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[usp_Periodicidad_Add]
	-- Add the parameters for the stored procedure here
	@UidPeriodicidad uniqueidentifier output,
	@IntFrecuencia int,
	@UidTipoFrecuencia uniqueidentifier,
	@DtFechaInicio Date = null,
	@DtFechaFin Date= null
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	SET @UidPeriodicidad = NEWID();
	INSERT INTO Periodicidad (UidPeriodicidad, IntFrecuencia,UidTipoFrecuencia,DtFechaInicio,DtFechaFin)
	VALUES (@UidPeriodicidad, @IntFrecuencia,@UidTipoFrecuencia,@DtFechaInicio,@DtFechaFin)
END