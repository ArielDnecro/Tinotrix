
CREATE PROCEDURE [dbo].[usp_Area_Update]
@UidArea uniqueidentifier,
@VchNombre nvarchar(50),
@VchDescripcion nvarchar(200),
@VchURL nvarchar(255),
@UidStatus uniqueidentifier,
@UidDepartamento uniqueidentifier

AS

SET NOCOUNT ON


UPDATE Area SET

VchNombre = @VchNombre,
VchDescripcion = @VchDescripcion,
VchURL = @VchURL,
UidStatus = @UidStatus,
UidDepartamento = @UidDepartamento

WHERE UidArea = @UidArea


