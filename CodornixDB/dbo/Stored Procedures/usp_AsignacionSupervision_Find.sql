CREATE   PROCEDURE [dbo].[usp_AsignacionSupervision_Find]
	@UidAsignacion uniqueidentifier
AS

	SET NOCOUNT ON

	SELECT TOP (1) * FROM AsignacionSupervision WHERE UidAsignacion = @UidAsignacion