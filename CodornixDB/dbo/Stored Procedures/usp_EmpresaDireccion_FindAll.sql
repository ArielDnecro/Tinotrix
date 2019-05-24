CREATE PROCEDURE [dbo].[usp_EmpresaDireccion_FindAll]

@UidEmpresa uniqueidentifier

AS

SET NOCOUNT ON

SELECT Direccion.* FROM Direccion INNER JOIN EmpresaDireccion ON Direccion.UidDireccion = EmpresaDireccion.UidDireccion WHERE EmpresaDireccion.UidEmpresa = @UidEmpresa
