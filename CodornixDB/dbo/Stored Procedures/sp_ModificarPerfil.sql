-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_ModificarPerfil] 
	-- Add the parameters for the stored procedure here
	@UidPerfil uniqueidentifier,
	@VchPerfil nvarchar(50),
	@UidNivelAcceso uniqueidentifier,
	@UidEmpresa uniqueidentifier,
	@UidHome uniqueidentifier
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	update Perfil set VchPerfil=@VchPerfil,
	UidNivelAcceso=@UidNivelAcceso,
	UidEmpresa = @UidEmpresa,
	UidHome = @UidHome
	where UidPerfil=@UidPerfil
END
