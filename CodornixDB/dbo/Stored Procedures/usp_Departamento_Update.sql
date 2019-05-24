
CREATE PROCEDURE [dbo].[usp_Departamento_Update]
@UidDepartamento uniqueidentifier,
@VchNombre nvarchar(50),
@VchDescripcion nvarchar(200),
@VchURL nvarchar(255),
@UidStatus uniqueidentifier,
@UidSucursal uniqueidentifier

AS 
SET NOCOUNT ON
UPDATE Departamento SET
VchNombre = @VchNombre,
VchDescripcion = @VchDescripcion,
VchURL = @VchURL,
UidStatus = @UidStatus,
UidSucursal = @UidSucursal
WHERE UidDepartamento = @UidDepartamento


