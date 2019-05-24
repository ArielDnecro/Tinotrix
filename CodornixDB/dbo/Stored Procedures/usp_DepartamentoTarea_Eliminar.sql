-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE usp_DepartamentoTarea_Eliminar
	-- Add the parameters for the stored procedure here
	@UidDepartamento uniqueidentifier
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	delete from DepartamentoTarea where UidDepartamento=@UidDepartamento
END