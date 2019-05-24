-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_PerfilSucursal]
	-- Add the parameters for the stored procedure here
	@UidPerfil uniqueidentifier
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT Sucursal.* FROM Sucursal INNER JOIN UsuarioPerfilSucursal ON Sucursal.UidSucursal = UsuarioPerfilSucursal.UidSucursal INNER JOIN Perfil ON UsuarioPerfilSucursal.UidPerfil = @UidPerfil;
END
