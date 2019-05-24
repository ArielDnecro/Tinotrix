-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[usp_DepartamentoSeleccionado]
	-- Add the parameters for the stored procedure here
	@UidDepartamento uniqueidentifier,
	@UidUsuario uniqueidentifier,
	@UidSucursal uniqueidentifier
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	select * from Departamento as d
	INNER JOIN Cumplimiento as c on d.UidDepartamento=c.UidDepartamento 
	INNER JOIN Periodo p ON d.UidDepartamento = p.UidDepartamento
INNER JOIN Usuario u ON u.UidUsuario = p.UidUsuario
	where d.UidDepartamento=@UidDepartamento and
	u.UidUsuario = @UidUsuario AND
	d.UidSucursal = @UidSucursal 
END