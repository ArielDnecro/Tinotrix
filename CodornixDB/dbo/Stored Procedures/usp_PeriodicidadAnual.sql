-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[usp_PeriodicidadAnual] 
	-- Add the parameters for the stored procedure here
	@UidPeriodicidad uniqueidentifier,
	@IntDiasMes int,
	@IntDiasSemanas int,
	@IntNumero int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT INTO PeriodicidadAnual (UidPeriodicidad, IntDiasMes, IntDiasSemanas, IntNumero)
	Values (@UidPeriodicidad, @IntDiasMes,@IntDiasSemanas, @IntNumero)
END