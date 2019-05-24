-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[usp_PeriodicidadMensual]
	-- Add the parameters for the stored procedure here
	@UidPeriodicidad uniqueidentifier,
	@IntDiasMes	INT,
	@IntDiasSemanas INT = null
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT INTO PeriodicidadMensual (UidPeriodicidad, IntDiasMes, IntDiasSemana)
	values (@UidPeriodicidad,@IntDiasMes,@IntDiasSemanas)
END