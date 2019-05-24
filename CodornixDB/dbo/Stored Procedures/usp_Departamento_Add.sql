
CREATE PROCEDURE [dbo].[usp_Departamento_Add]
@VchNombre nvarchar(50),
@VchDescripcion nvarchar(200),
@VchURL nvarchar(255),
@UidStatus uniqueidentifier,
@UidSucursal uniqueidentifier,
@UidDepartamento uniqueidentifier OUTPUT
AS 
SET NOCOUNT ON

SET @UidDepartamento = NEWID()
SET @VchURL = convert(nvarchar(50), @UidDepartamento) + '_' + @VchURL
INSERT INTO Departamento (UidDepartamento, VchNombre, VchDescripcion, VchURL, UidStatus, UidSucursal)
VALUES (@UidDepartamento, @VchNombre, @VchDescripcion, @VchURL, @UidStatus, @UidSucursal)


