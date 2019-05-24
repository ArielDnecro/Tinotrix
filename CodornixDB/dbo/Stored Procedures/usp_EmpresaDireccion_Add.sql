CREATE PROCEDURE [dbo].[usp_EmpresaDireccion_Add] 

@UidEmpresa uniqueidentifier,
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

DECLARE @UidDireccion uniqueidentifier

SET @UidDireccion = NEWID()

INSERT INTO Direccion (UidDireccion, UidPais, UidEstado, VchMunicipio, VchCiudad, VchColonia, VchCalle, VchConCalle, VchYCalle, VchNoExt, VchNoInt, VchReferencia) VALUES (@UidDireccion, @UidPais, @UidEstado, @VchMunicipio, @VchCiudad, @VchColonia, @VchCalle, @VchConCalle, @VchYCalle, @VchNoExt, @VchNoInt, @VchReferencia)

INSERT INTO EmpresaDireccion (UidEmpresa, UidDireccion) VALUES (@UidEmpresa, @UidDireccion)
