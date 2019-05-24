-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_UsuarioPerfilEmpresa]
	-- Add the parameters for the stored procedure here
	@UidUsuario uniqueidentifier
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT Empresa.*, Perfil.* FROM Empresa INNER JOIN UsuarioPerfilEmpresa ON Empresa.UidEmpresa = UsuarioPerfilEmpresa.UidEmpresa INNER JOIN Perfil ON Perfil.UidPerfil = UsuarioPerfilEmpresa.UidPerfil WHERE UsuarioPerfilEmpresa.UidUsuario = @UidUsuario;
END
