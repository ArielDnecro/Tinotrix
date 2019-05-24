
CREATE PROCEDURE [dbo].[usp_Area_Add]

@VchNombre nvarchar(50) ,
@VchDescripcion nvarchar(200),
@VchURL nvarchar(255) output,
@UidStatus uniqueidentifier,
@UidDepartamento uniqueidentifier,
@UidArea uniqueidentifier OUTPUT
AS

SET NOCOUNT ON

SET @UidArea = NEWID()
SET @VchURL = convert(nvarchar(50), @UidArea) + '_' + @VchURL

INSERT INTO Area (UidArea, VchNombre, VchDescripcion, VchURL, UidStatus, UidDepartamento)
VALUES (@UidArea, @VchNombre, @VchDescripcion, @VchURL, @UidStatus, @UidDepartamento)


