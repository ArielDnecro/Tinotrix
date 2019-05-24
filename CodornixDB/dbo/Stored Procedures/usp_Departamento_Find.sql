
CREATE PROCEDURE [dbo].[usp_Departamento_Find]
@UidDepartamento uniqueidentifier

AS 
SET NOCOUNT ON

SELECT TOP (1) * FROM Departamento WHERE UidDepartamento = @UidDepartamento


