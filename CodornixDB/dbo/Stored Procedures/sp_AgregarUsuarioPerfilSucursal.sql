-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_AgregarUsuarioPerfilSucursal] 
	-- Add the parameters for the stored procedure here
	@UidUsuario uniqueidentifier,
	@UidPerfil uniqueidentifier,
	@UidSucursal uniqueidentifier
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT INTO UsuarioPerfilSucursal (UidUsuario, UidPerfil, UidSucursal) Values (@UidUsuario, @UidPerfil, @UidSucursal)
END
