-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_AgregarUsuarioPerfilEmpresa] 
	-- Add the parameters for the stored procedure here
	@UidUsuario uniqueidentifier,
	@UidPerfil uniqueidentifier,
	@UidEmpresa uniqueidentifier
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

INSERT INTO UsuarioPerfilEmpresa (UidUsuario, UidPerfil, UidEmpresa) Values (@UidUsuario, @UidPerfil, @UidEmpresa)
END
