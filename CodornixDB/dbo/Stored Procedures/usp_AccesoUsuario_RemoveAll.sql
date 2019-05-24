
CREATE PROCEDURE [dbo].[usp_AccesoUsuario_RemoveAll]
@UidUsuario uniqueidentifier
AS
SET NOCOUNT ON

DELETE FROM AccesoUsuario WHERE
UidUsuario = @UidUsuario