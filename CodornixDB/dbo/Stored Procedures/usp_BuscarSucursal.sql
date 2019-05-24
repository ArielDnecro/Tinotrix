-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[usp_BuscarSucursal]
	-- Add the parameters for the stored procedure here
	@UidEmpresa uniqueidentifier,
	@VchNombre nvarchar(50)=null
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	select Sucursal.*, TipoSucursal.VchTipoSucursal from Sucursal 
	JOIN TipoSucursal ON Sucursal.UidTipoSucursal = TipoSucursal.UidTipoSucursal
	where UidEmpresa = @UidEmpresa and
	((@VchNombre is not null and  VchNombre like '%' + @VchNombre+ '%')OR (@VchNombre is null))
END