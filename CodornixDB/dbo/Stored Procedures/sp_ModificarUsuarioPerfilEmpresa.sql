-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_ModificarUsuarioPerfilEmpresa] 
	-- Add the parameters for the stored procedure here
	@UidUSuario uniqueidentifier,
	@UidPerfil	uniqueidentifier,
	@UidEmpresa	uniqueidentifier

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	update UsuarioPerfilEmpresa set 
	UidPerfil=@UidPerfil,
	UidEmpresa=@UidEmpresa
	where UidUsuario=@UidUsuario
END
