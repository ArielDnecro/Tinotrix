
-- =============================================
-- Author:		Aremy Daniela De Leon Tercero
-- Create date: 2017-05-17
-- Description:	Insersion de Usuarios
-- =============================================
CREATE PROCEDURE [dbo].[sp_InsertarUsuario]
	-- Add the parameters for the stored procedure here
	@VchNombre nvarchar(50),
	@VchApellidoPaterno nvarchar(50),
	@VchApellidoMaterno nvarchar(50),
	@DtFechaNacimiento date,
	@VchCorreo nvarchar(50),
	@DtFechaInicio date,
	@DtFechaFin date = null,
	@VchUsuario nvarchar(50),
	@VchPassword nvarchar(50),
	@UidStatus uniqueidentifier,
	@UidUsuario uniqueidentifier OUTPUT,
	@VchRutaImagen nvarchar(200)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	SET @UidUsuario = newid();

    -- Insert statements for procedure here
	insert into Usuario(UidUsuario, VchNombre, VchApellidoPaterno,VchApellidoMaterno,
	DtFechaNacimiento, VchCorreo, DtFechaInicio,DtFechaFin, VchUsuario,VchPassword, UidStatus,VchRutaImagen)
	values(@UidUsuario, @VchNombre,@VchApellidoPaterno,@VchApellidoMaterno,@DtFechaNacimiento,
	@VchCorreo,@DtFechaInicio,@DtFechaFin,@VchUsuario,@VchPassword,
	@UidStatus,@VchRutaImagen)

END

