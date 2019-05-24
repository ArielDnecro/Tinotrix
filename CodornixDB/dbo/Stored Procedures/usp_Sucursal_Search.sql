CREATE PROCEDURE [dbo].[usp_Sucursal_Search]
@VchNombre nvarchar(50) = null,
@DtFechaRegistroInicio date = null,
@DtFechaRegistroFin date = null,
@UidEmpresa uniqueidentifier = null,
@UidTipoSucursal nvarchar(2000) = null,
@VchRutaImagen nvarchar(200)= null
AS

SET NOCOUNT ON

SELECT Sucursal.*, TipoSucursal.VchTipoSucursal FROM Sucursal JOIN TipoSucursal ON Sucursal.UidTipoSucursal = TipoSucursal.UidTipoSucursal WHERE
(@VchNombre IS NULL OR VchNombre LIKE '%' + @VchNombre + '%') AND
(@DtFechaRegistroInicio IS NULL OR DtFechaRegistro >= @DtFechaRegistroInicio) AND
(@DtFechaRegistroFin IS NULL OR DtFechaRegistro <= @DtFechaRegistroFin) AND
(@UidEmpresa IS NULL OR UidEmpresa = @UidEmpresa) AND
(@UidTipoSucursal IS NULL OR Sucursal.UidTipoSucursal IN (SELECT * FROM CSVtoTable(@UidTipoSucursal, ','))) 
