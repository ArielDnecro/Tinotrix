CREATE PROCEDURE [dbo].[usp_Direccion_Update]

@UidDireccion uniqueidentifier,
@UidPais uniqueidentifier,
@UidEstado uniqueidentifier,
@VchMunicipio nvarchar(30),
@VchCiudad nvarchar(30),
@VchColonia nvarchar(20),
@VchCalle nvarchar(20),
@VchConCalle nvarchar(20),
@VchYCalle nvarchar(20),
@VchNoExt nvarchar(20),
@VchNoInt nvarchar(20),
@VchReferencia nvarchar(200)

AS

SET NOCOUNT ON

UPDATE Direccion SET
UidPais = @UidPais,
UidEstado = @UidEstado,
VchMunicipio = @VchMunicipio,
VchCiudad = @VchCiudad,
VchColonia = @VchColonia,
VchCalle = @VchCalle,
VchConCalle = @VchConCalle,
VchYCalle = @VchYCalle,
VchNoExt = @VchNoExt,
VchNoInt = @VchNoInt,
VchReferencia = @VchReferencia

WHERE UidDireccion = @UidDireccion
