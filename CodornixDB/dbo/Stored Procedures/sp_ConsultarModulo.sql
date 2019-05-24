﻿-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_ConsultarModulo] 
	-- Add the parameters for the stored procedure here
	@UidPerfil uniqueidentifier
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	--select * from Modulo
	SELECT * FROM Modulo WHERE BitDenegable = 1 AND UidModulo IN (SELECT UidModulo FROM Acceso WHERE UidPerfil = @UidPerfil)
END
