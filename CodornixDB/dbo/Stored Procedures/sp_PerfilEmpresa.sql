-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_PerfilEmpresa] 
	-- Add the parameters for the stored procedure here
	@UidPerfil uniqueidentifier
	As
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT Empresa.* FROM Empresa INNER JOIN UsuarioPerfilEmpresa ON Empresa.UidEmpresa = UsuarioPerfilEmpresa.UidEmpresa INNER JOIN Perfil ON UsuarioPerfilEmpresa.UidPerfil = @UidPerfil;
END
