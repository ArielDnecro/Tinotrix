
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_BuscarEmpresa]
	-- Add the parameters for the stored procedure here
	@VchRazonSocial nvarchar(50)= null,
	@VchNombreComercial nvarchar(50)= null,
	@ChRfc nvarchar(50) = null
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	select * from Empresa where 
	((@VchNombreComercial is not null and  VchNombreComercial like '%' + @VchNombreComercial+ '%')OR (@VchNombreComercial is null))
	and ((@VchRazonSocial is not null and  VchRazonSocial like '%' + @VchRazonSocial+ '%')OR (@VchRazonSocial is null))
	and ((@ChRfc is not null and  ChRFC like '%' + @ChRfc+ '%')OR (@ChRfc is null))
END
