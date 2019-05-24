CREATE PROCEDURE [dbo].[usp_Empresa_Find]
@UidEmpresa uniqueidentifier
AS

SET NOCOUNT ON

SELECT * FROM Empresa WHERE UidEmpresa = @UidEmpresa
