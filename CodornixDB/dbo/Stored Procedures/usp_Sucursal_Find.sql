CREATE PROCEDURE [dbo].[usp_Sucursal_Find]

@UidSucursal uniqueidentifier

AS

SET NOCOUNT ON

SELECT TOP (1) Sucursal.*, TipoSucursal.VchTipoSucursal FROM Sucursal JOIN TipoSucursal ON Sucursal.UidTipoSucursal = TipoSucursal.UidTipoSucursal WHERE UidSucursal = @UidSucursal
