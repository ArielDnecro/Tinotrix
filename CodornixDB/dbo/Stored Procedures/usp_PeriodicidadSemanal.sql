-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[usp_PeriodicidadSemanal]
	-- Add the parameters for the stored procedure here
	@UidPeriodicidad uniqueidentifier,
	@BitLunes bit,
	@BitMartes bit,
	@BitMiercoles bit,
	@BitJueves bit,
	@BitViernes bit,
	@BitSabado bit,
	@BitDomingo bit
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT INTO PeriodicidadSemanal (UidPeriodicidad,BitLunes,BitMartes,BitMiercoles,BitJueves,
	BitViernes,BitSabado,BitDomingo) VALUES (@UidPeriodicidad, @BitLunes,@BitMartes,@BitMiercoles, 
	@BitJueves,@BitViernes,@BitSabado,@BitDomingo)
END