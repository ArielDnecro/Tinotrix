


-- =============================================
-- Author:		Aremy Daniela De Leon Tercero
-- Create date: 2017-05-18
-- Description:	Busqueda de Usuario
-- =============================================
CREATE PROCEDURE [dbo].[sp_BuscarUsuario] 
	-- Add the parameters for the stored procedure here
	@VchNombre nvarchar(50) = null,
	@VchApellidoPaterno nvarchar(50) = null,
	@VchApellidoMaterno nvarchar(50) = null,
	@DtFechaNacimiento nvarchar(50) = null,
	@DtFechaNacimiento2  nvarchar(50)= null,
	@VchCorreo nvarchar(50) = null,
	@DtFechaInicio nvarchar(50) = null,
	@DtFechaInicio2 nvarchar(50) = null,
	@DtFechaFin nvarchar(50) = null,
	@DtFechaFin2 nvarchar(50) = null,
	@VchUsuario nvarchar(50)= null,
	@UidPerfil nvarchar(4000)=NULL,
	@VchPerfil nvarchar(500)= null,
	@UidStatus nvarchar(4000)=null,
	@VchStatus nvarchar(50)= null,
	@UidEmpresas nvarchar(4000) = null
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	select * from Usuario AS u 
	INNER JOIN UsuarioPerfilEmpresa as upe on upe.UidUsuario=u.UidUsuario
	INNER JOIN Perfil AS p ON upe.UidPerfil=p.UidPerfil 
	INNER JOIN Estatus AS es ON u.UidStatus=es.UidStatus
	INNER JOIN NivelAcceso AS na ON NA.UidNivelAcceso= P.UidNivelAcceso	
	where
	((na.VchNivelAcceso like 'Backend') or (na.VchNivelAcceso like 'Backsite'))
	and (p.VchPerfil not like 'SuperAdministrador')
	and ((@VchNombre is not null and  u.VchNombre like '%' + @VchNombre+ '%')OR (@VchNombre is null))
	 and ((@VchApellidoPaterno is not null and u.VchApellidoPaterno like '%' +  @VchApellidoPaterno+'%')OR(@VchApellidoPaterno is null))
	 and ((@VchApellidoMaterno is not null and u.VchApellidoMaterno like '%' +  @VchApellidoMaterno+'%')OR(@VchApellidoMaterno is null))
	 and ((@VchCorreo is not null and u.VchCorreo like '%' +  @VchCorreo+'%')OR(@VchCorreo is null))
	 and ((@VchUsuario is not null and u.VchUsuario like '%' +  @VchUsuario+'%')OR(@VchUsuario is null))
	 and ((@UidPerfil is not null and upe.UidPerfil in( select * from CSVtoTable(@UidPerfil, ',')))OR(@UidPerfil is null))
	-- and ((@VchPerfil is not null and p.VchPerfil like'%'+ @VchPerfil+'%')OR(@VchPerfil is null))
	 and ((@UidStatus is not null and u.UidStatus in(select * from CSVtoTable(@UidStatus,',')))OR(@UidStatus is null))
	 
	 and ((@DtFechaNacimiento is not null and u.DtFechaNacimiento >=  @DtFechaNacimiento)OR(@DtFechaNacimiento is null))
	 and ((@DtFechaNacimiento2 is not null and u.DtFechaNacimiento <=  @DtFechaNacimiento2)OR(@DtFechaNacimiento2 is null))
	 --and ((@DtFechaNacimiento is not null and @DtFechaNacimiento2 is not null and u.DtFechaNacimiento >=   @DtFechaNacimiento and u.DtFechaNacimiento <= @DtFechaNacimiento2)OR(@DtFechaNacimiento2 is null or @DtFechaNacimiento is null))
	 
	 and ((@DtFechaInicio is not null and u.DtFechaInicio  >= @DtFechaInicio)OR (@DtFechaInicio is null))
	 and ((@DtFechaInicio2 is not null and u.DtFechaInicio <= @DtFechaInicio2) OR (@DtFechaInicio2 is null)) 
	 --and ((@DtFechaInicio is null and @DtFechaInicio2 is not null and u.DtFechaInicio>= @DtFechaInicio and u.DtFechaInicio <= @DtFechaInicio2) OR (@DtFechaInicio2 is not null or @DtFechaInicio is null))
	 
	 and ((@DtFechaFin is not null and u.DtFechaFin >=  @DtFechaFin )OR (@DtFechaFin is null))
	 and ((@DtFechaFin2 is not null and u.DtFechaFin<= @DtFechaFin2) OR (@DtFechaFin2 is null)) 
	 --and ((@DtFechaFin is null and @DtFechaFin2 is not null and u.DtFechaFin>= @DtFechaFin and u.DtFechaFin <=  @DtFechaFin2) OR (@DtFechaFin2 is not null or @DtFechaFin is null))
	 and ((@UidEmpresas is not null and upe.UidEmpresa in(select * from CSVtoTable(@UidEmpresas,',')))OR(@UidEmpresas is null))
	 
	 
	
	 
	 
END



