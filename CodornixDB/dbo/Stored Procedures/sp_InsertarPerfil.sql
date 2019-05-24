-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_InsertarPerfil]
	-- Add the parameters for the stored procedure here
	@VchPerfil nvarchar(50),
	@UidNivelAcceso uniqueidentifier,
	@UidEmpresa uniqueidentifier,
	@UidHome uniqueidentifier,
	@UidPerfil uniqueidentifier OUTPUT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	SET @UidPerfil = NEWID();
    -- Insert statements for procedure here
	insert into Perfil(UidPerfil, VchPerfil, UidNivelAcceso, UidEmpresa,UidHome)
	values(@UidPerfil, @VchPerfil, @UidNivelAcceso, @UidEmpresa,@UidHome)
END
