-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE usp_DepartamentoTarea_Add
	-- Add the parameters for the stored procedure here
	@UidTarea uniqueidentifier,
	@UidDepartamento uniqueidentifier
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	insert into DepartamentoTarea( UidDepartamento, UidTarea) values(@UidDepartamento, @UidTarea)
END