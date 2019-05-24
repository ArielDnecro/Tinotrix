CREATE PROCEDURE [dbo].[usp_EmpresaTelefono_Add]
@UidEmpresa uniqueidentifier,
@VchTelefono nvarchar(20),
@UidTipoTelefono uniqueidentifier

AS

SET NOCOUNT ON

DECLARE @UidTelefono uniqueidentifier

SET @UidTelefono = NEWID()

INSERT INTO Telefono (UidTelefono, VchTelefono, UidTipoTelefono) VALUES (@UidTelefono, @VchTelefono, @UidTipoTelefono)

INSERT INTO EmpresaTelefono (UidEmpresa, UidTelefono) VALUES (@UidEmpresa, @UidTelefono)
