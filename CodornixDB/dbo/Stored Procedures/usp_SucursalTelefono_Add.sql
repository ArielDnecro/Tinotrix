
CREATE PROCEDURE [dbo].[usp_SucursalTelefono_Add]
@UidSucursal uniqueidentifier,
@UidTipoTelefono uniqueidentifier,
@VchTelefono nvarchar(20)

AS

SET NOCOUNT ON

DECLARE @UidTelefono uniqueidentifier

SET @UidTelefono = NEWID()

INSERT INTO Telefono (UidTelefono, VchTelefono, UidTipoTelefono) VALUES (@UidTelefono, @VchTelefono, @UidTipoTelefono)

INSERT INTO SucursalTelefono (UidSucursal, UidTelefono) VALUES (@UidSucursal, @UidTelefono)
