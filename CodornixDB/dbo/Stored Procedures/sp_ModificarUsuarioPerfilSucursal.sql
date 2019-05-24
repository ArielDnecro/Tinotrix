-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_ModificarUsuarioPerfilSucursal] 
	-- Add the parameters for the stored procedure here
	@UidUSuario uniqueidentifier,
	@UidPerfil	uniqueidentifier,
	@UidSucursal uniqueidentifier
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	update UsuarioPerfilSucursal set 
	UidPerfil=@UidPerfil,
	@UidSucursal=@UidSucursal
	where UidUsuario=@UidUsuario
END
