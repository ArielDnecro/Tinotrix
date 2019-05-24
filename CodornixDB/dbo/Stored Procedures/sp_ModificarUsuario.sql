
-- =============================================
-- Author:		Aremy Daniela De Leon Tercero
-- Create date: 2017-05-17
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_ModificarUsuario] 
	-- Add the parameters for the stored procedure here
	@UidUsuario uniqueidentifier,
	@VchNombre nvarchar(50),
	@VchApellidoPaterno nvarchar(50),
	@VchApellidoMaterno nvarchar(50),
	@DtFechaNacimiento date,
	@VchCorreo nvarchar(50),
	@DtFechaInicio nvarchar(50),
	@DtFechaFin nvarchar(50)= null,
	@VchUsuario nvarchar(50),
	@VchPassword nvarchar(50),
	@UidStatus uniqueidentifier,
	@VchRutaImagen nvarchar(200)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	update Usuario set VchNombre=@VchNombre, 
	VchApellidoPaterno=@VchApellidoPaterno,
	VchApellidoMaterno=@VchApellidoMaterno,
	DtFechaNacimiento=@DtFechaNacimiento,
	VchCorreo=@VchCorreo,
	DtFechaInicio=@DtFechaInicio,
	DtFechaFin=@DtFechaFin,
	VchUsuario=@VchUsuario, 
	VchPassword=@VchPassword, 
	UidStatus=@UidStatus,
	VchRutaImagen= @VchRutaImagen
	where UidUsuario=@UidUsuario

END


