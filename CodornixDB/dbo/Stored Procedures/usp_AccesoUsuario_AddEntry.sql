
CREATE PROCEDURE [dbo].[usp_AccesoUsuario_AddEntry]
@UidUsuario uniqueidentifier,
@UidModulo uniqueidentifier
AS
SET NOCOUNT ON

INSERT INTO AccesoUsuario (UidUsuario, UidModulo) VALUES (@UidUsuario, @UidModulo)
