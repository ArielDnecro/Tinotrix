-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_AgregarNivelAccesoModulo] 
	-- Add the parameters for the stored procedure here
@UidPerfil uniqueidentifier,
@UidModulo uniqueidentifier

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON
INSERT INTO Acceso(UidPerfil, UidModulo) VALUES (@UidPerfil, @UidModulo)
END
