
CREATE PROCEDURE [dbo].[usp_Area_Search]
@VchNombre nvarchar(50) = null,
@VchDescripcion nvarchar(200) = null,
@UidDepartamento uniqueidentifier = null

AS

SET NOCOUNT ON

SELECT * FROM Area WHERE
((@VchNombre IS NOT NULL AND VchNombre LIKE '%' + @VchNombre + '%') OR @VchNombre IS NULL) AND
((@VchDescripcion IS NOT NULL AND VchDescripcion LIKE '%' + @VchDescripcion + '%') OR @VchDescripcion IS NULL) AND
((@UidDepartamento IS NOT NULL AND UidDepartamento = @UidDepartamento) OR @UidDepartamento IS NULL)
