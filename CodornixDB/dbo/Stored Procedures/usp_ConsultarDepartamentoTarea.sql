-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[usp_ConsultarDepartamentoTarea]
	-- Add the parameters for the stored procedure here
	@UidTarea Uniqueidentifier
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	select Departamento.* from Departamento INNER JOIN DepartamentoTarea ON Departamento.UidDepartamento = DepartamentoTarea.UidDepartamento WHERE DepartamentoTarea.UidTarea = @UidTarea
END