CREATE PROCEDURE [dbo].[usp_Empresa_Search]
@VchNombreComercial nvarchar(50) = NULL,
@VchRazonSocial nvarchar(60) = NULL,
@VchGiro nvarchar(40) = NULL,
@ChRFC nvarchar(13) = NULL,
@DtFechaRegistroInicio date = NULL,
@DtFechaRegistroFin date = NULL
AS

SET NOCOUNT ON

SELECT * FROM Empresa WHERE
((@VchNombreComercial IS NOT NULL AND VchNombreComercial LIKE '%' + @VchNombreComercial  + '%') OR @VchNombreComercial IS NULL) AND
((@VchRazonSocial IS NOT NULL AND VchRazonSocial LIKE '%' + @VchRazonSocial + '%') OR @VchRazonSocial IS NULL) AND
((@VchGiro IS NOT NULL AND VchGiro LIKE '%' + @VchGiro + '%') OR @VchGiro IS NULL) AND
((@ChRFC IS NOT NULL AND ChRFC LIKE '%' + @ChRFC + '%') OR @ChRFC IS NULL) AND
((@DtFechaRegistroInicio IS NOT NULL AND DtFechaRegistro >= @DtFechaRegistroInicio) OR @DtFechaRegistroInicio IS NULL) AND
((@DtFechaRegistroFin IS NOT NULL AND DtFechaRegistro <= @DtFechaRegistroFin) OR @DtFechaRegistroFin IS NULL) 

