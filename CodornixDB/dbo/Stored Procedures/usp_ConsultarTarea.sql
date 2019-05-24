-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[usp_ConsultarTarea]
	-- Add the parameters for the stored procedure here
	@UidSucursal uniqueidentifier,
	@VchNombre nvarchar(50) = null,
	@DtFechainicio nvarchar(50) = null,
	@DtFechainicio2 nvarchar(50)= null,
	@UidDepartamentos nvarchar(4000)= null,
	@UidUsuarios nvarchar(4000) = null
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT DISTINCT
		t.*,
		d.VchNombre AS VchDepartamento,
		tp.VchTipoFrecuencia,
		us.VchNombre + ' ' + us.VchApellidoPaterno AS VchUsuario,
		p.DtFechaInicio
	FROM Tarea AS t
	INNER JOIN Periodicidad AS p ON t.UidPeriodicidad=p.UidPeriodicidad
	INNER JOIN TipoFrecuencia AS tp ON tp.UidTipoFrecuencia=p.UidTipoFrecuencia
	LEFT JOIN DepartamentoTarea AS dt ON t.UidTarea = dt.UidTarea
	LEFT JOIN TareaArea AS ta ON t.UidTarea = ta.UidTarea
	LEFT JOIN Area AS a ON a.UidArea = ta.UidArea
	LEFT JOIN Departamento AS d ON d.UidDepartamento = dt.UidDepartamento OR d.UidDepartamento = a.UidDepartamento
	LEFT JOIN Periodo AS pp ON pp.UidDepartamento = d.UidDepartamento AND pp.DtFechaInicio <= GETDATE() AND pp.DtFechaFin >= GETDATE()
	LEFT JOIN Usuario AS us ON us.UidUsuario = pp.UidUsuario
	where 

	t.BitCaducado=0 and
	((@VchNombre is not null and t.VchNombre like '%' +  @VchNombre+'%')OR(@VchNombre is null))
	and ((@DtFechainicio is not null and p.DtFechaInicio >=  @DtFechainicio)OR(@DtFechainicio is null))
	 and ((@DtFechainicio2 is not null and p.DtFechaInicio <=  @DtFechainicio2)OR(@DtFechainicio2 is null))
	 and ((@UidDepartamentos is not null and d.UidDepartamento in(select * from CSVtoTable(@UidDepartamentos,',')))OR(@UidDepartamentos is null))
	 AND (d.UidSucursal = @UidSucursal)
	 AND (@UidUsuarios IS NULL OR (
		us.UidUsuario IN (SELECT * FROM CSVtoTable(@UidUsuarios, ','))))
END