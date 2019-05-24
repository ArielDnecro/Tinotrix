CREATE PROCEDURE [dbo].[usp_Periodo_Find]
	@UidPeriodo uniqueidentifier
AS

	SET NOCOUNT ON

	SELECT TOP (1) * FROM Periodo WHERE UidPeriodo = @UidPeriodo
