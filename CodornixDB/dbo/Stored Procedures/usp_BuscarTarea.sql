-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[usp_BuscarTarea] 
	-- Add the parameters for the stored procedure here
	@VchNombre nvarchar(50)=null,
	@VchDescripcion nvarchar(50)=null
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	select T.*,P.*, TF.* from Tarea as T
	INNER JOIN Periodicidad as P on P.UidPeriodicidad=T.UidPeriodicidad
	INNER JOIN TipoFrecuencia as TF on P.UidTipoFrecuencia= TF.UidTipoFrecuencia
END