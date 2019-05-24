-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[usp_BuscarCumplimiento]
	-- Add the parameters for the stored procedure here
	@DtFecha date = null,
	@DtFecha2 date = null,
	@UidUsuario uniqueidentifier
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	select * from Cumplimiento as c 
	inner join Departamento as d on c.UidDepartamento=d.UidDepartamento
	inner join Periodo as p on p.UidDepartamento= d.UidDepartamento
	 where 
	 c.UidUsuario=@UidUsuario and
	  ((@DtFecha is not null and p.DtFechaInicio >=  @DtFecha)OR(@DtFecha is null))
	 and ((@DtFecha2 is not null and p.DtFechaFin <=  @DtFecha2)OR(@DtFecha2 is null))
	 
END