CREATE PROCEDURE [dbo].[usp_Sucursal_FindAll]
@UidEmpresa uniqueidentifier 
AS

SET NOCOUNT ON

SELECT Sucursal.*, TipoSucursal.VchTipoSucursal FROM Sucursal JOIN TipoSucursal ON Sucursal.UidTipoSucursal = TipoSucursal.UidTipoSucursal WHERE
(UidEmpresa = @UidEmpresa)
