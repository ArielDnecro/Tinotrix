
CREATE PROCEDURE [dbo].[usp_Departamento_Search]
@VchNombre nvarchar(50) = null,
@VchDescripcion nvarchar(200) = null,
@UidSucursal uniqueidentifier = null
AS 
SET NOCOUNT ON

SELECT * FROM Departamento WHERE
((@VchNombre IS NOT NULL AND VchNombre LIKE '%' + @VchNombre + '%') OR @VchNombre IS NULL) AND
((@VchDescripcion IS NOT NULL AND VchDescripcion LIKE '%' + @VchDescripcion + '%') OR @VchDescripcion IS NULL) AND
((@UidSucursal IS NOT NULL AND UidSucursal = @UidSucursal) OR @UidSucursal IS null)


