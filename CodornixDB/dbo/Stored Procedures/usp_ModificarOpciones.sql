-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE usp_ModificarOpciones
	-- Add the parameters for the stored procedure here
	@UidOpciones uniqueidentifier,
	@VchOpciones nvarchar(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	UPDATE Opciones SET VchOpciones = @VchOpciones WHERE UidOpciones = @UidOpciones
END