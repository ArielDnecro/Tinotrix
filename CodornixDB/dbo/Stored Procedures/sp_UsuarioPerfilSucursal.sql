-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_UsuarioPerfilSucursal] 
	-- Add the parameters for the stored procedure here
	@UidUsuario uniqueidentifier
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT Sucursal.*, Perfil.* FROM Sucursal INNER JOIN UsuarioPerfilSucursal ON Sucursal.UidSucursal = UsuarioPerfilSucursal.UidSucursal INNER JOIN Perfil ON Perfil.UidPerfil = UsuarioPerfilSucursal.UidPerfil WHERE UsuarioPerfilSucursal.UidUsuario = @UidUsuario;
END
