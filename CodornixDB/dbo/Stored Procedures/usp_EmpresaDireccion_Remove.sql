CREATE PROCEDURE [dbo].[usp_EmpresaDireccion_Remove]

@UidDireccion uniqueidentifier

AS

SET NOCOUNT ON

DELETE FROM EmpresaDireccion WHERE UidDireccion = @UidDireccion

DELETE FROM Direccion WHERE UidDireccion = @UidDireccion
