CREATE PROCEDURE [dbo].[usp_Periodo_Search]
@DtFechaInicioDespuesDe date = null,
@DtFechaInicioAntesDe date = null,
@DtFechaFinDespuesDe date = null,
@DtFechaFinAntesDe date = null,
@UidDepartamento nvarchar(4000) = null,
@UidSucursal uniqueidentifier = null,
@UidUsuario uniqueidentifier = null,
@VchUsuario nvarchar(4000) = null,
@UidTurno nvarchar(4000) = null
AS

SET NOCOUNT ON

SELECT * FROM Periodo p
INNER JOIN Usuario u ON p.UidUsuario = u.UidUsuario
INNER JOIN Departamento d ON p.UidDepartamento = d.UidDepartamento
WHERE
(@DtFechaInicioDespuesDe IS NULL OR p.DtFechaInicio >= @DtFechaInicioDespuesDe) AND
(@DtFechaInicioAntesDe IS NULL OR p.DtFechaInicio <= @DtFechaInicioAntesDe) AND
(@DtFechaFinDespuesDe IS NULL OR p.DtFechaFin >= @DtFechaFinDespuesDe) AND
(@DtFechaFinAntesDe IS NULL OR p.DtFechaFin <= @DtFechaFinAntesDe) AND
(@UidSucursal IS NULL OR d.UidSucursal = @UidSucursal) AND
(@UidDepartamento IS NULL OR p.UidDepartamento IN (SELECT * FROM CSVtoTable(@UidDepartamento, ','))) AND
(@UidUsuario IS NULL OR p.UidUsuario = @UidUsuario) AND
(@VchUsuario IS NULL OR (u.VchNombre LIKE '%' + @VchUsuario + '%' OR
						 u.VchApellidoPaterno LIKE '%' + @VchUsuario + '%' OR
						 u.VchApellidoMaterno LIKE '%' + @VchUsuario + '%')) AND
(@UidTurno IS NULL OR UidTurno IN (SELECT * FROM CSVtoTable(@UidTurno, ',')))
