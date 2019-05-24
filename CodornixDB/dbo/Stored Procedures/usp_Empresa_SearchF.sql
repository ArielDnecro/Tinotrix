CREATE   PROCEDURE usp_Empresa_SearchF
	@VchRazonSocial nvarchar(50)= null,
	@VchNombreComercial nvarchar(50)= null,
	@ChRfc nvarchar(50) = null,
	@VchColumn nvarchar(50) = N'UidEmpresa',
	@VchDir nvarchar(4) = N'ASC',
	@IntOffset int = 0,
	@IntElements int = 10,
	@IntTotalResults int OUTPUT

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	IF @VchColumn NOT IN ('UidEmpresa', 'VchRazonSocial', 'VchNombreComercial', 'ChRfc')
	BEGIN
		RAISERROR ('Invalid column name', 11, 1)
		RETURN;
	END;

	IF @VchDir NOT IN ('ASC', 'DESC')
	BEGIN
		RAISERROR ('Invalid direction name', 11, 1)
		RETURN;
	END

	-- Obtener numero de elementos
	select @IntTotalResults = COUNT(*) from Empresa where 
	((@VchNombreComercial is not null and  VchNombreComercial like '%' + @VchNombreComercial+ '%')OR (@VchNombreComercial is null))
	and ((@VchRazonSocial is not null and  VchRazonSocial like '%' + @VchRazonSocial+ '%')OR (@VchRazonSocial is null))
	and ((@ChRfc is not null and  ChRFC like '%' + @ChRfc+ '%')OR (@ChRfc is null))

    -- Insert statements for procedure here
	DECLARE @sql nvarchar(MAX) = '
	select * from Empresa where 
	((@VchNombreComercial is not null and  VchNombreComercial like ''%'' + @VchNombreComercial+ ''%'')OR (@VchNombreComercial is null))
	and ((@VchRazonSocial is not null and  VchRazonSocial like ''%'' + @VchRazonSocial+ ''%'')OR (@VchRazonSocial is null))
	and ((@ChRfc is not null and  ChRFC like ''%'' + @ChRfc+ ''%'')OR (@ChRfc is null))
	ORDER BY ' + @VchColumn + ' ' + @VchDir + ' OFFSET ' + CAST(@IntOffset as nvarchar(MAX)) + ' ROWS FETCH NEXT ' +  CAST(@IntElements as nvarchar(MAX)) + ' ROWS ONLY'

	EXECUTE sp_executesql @sql, N'@VchNombreComercial nvarchar(50), @VchRazonSocial nvarchar(50), @ChRfc nvarchar(50)', @VchNombreComercial, @VchRazonSocial, @ChRfc

END