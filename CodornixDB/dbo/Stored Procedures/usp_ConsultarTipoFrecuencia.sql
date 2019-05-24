-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[usp_ConsultarTipoFrecuencia]
	-- Add the parameters for the stored procedure here
	@VchTipoFrecuencia nvarchar(50)=null,
	@UidTipoFrecuencia uniqueidentifier = null
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	select * from TipoFrecuencia where VchTipoFrecuencia=@VchTipoFrecuencia or UidTipoFrecuencia=@UidTipoFrecuencia
END