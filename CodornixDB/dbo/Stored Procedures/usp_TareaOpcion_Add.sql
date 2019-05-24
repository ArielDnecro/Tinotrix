﻿-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[usp_TareaOpcion_Add]
	-- Add the parameters for the stored procedure here
	@UidTarea uniqueidentifier,
	@VchOpciones nvarchar(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	DECLARE @UidOpciones uniqueidentifier
	set @UidOpciones = NEWID()
	INSERT INTO Opciones(UidOpciones, VchOpciones, UidTarea) VALUES (@UidOpciones, @VchOpciones,@UidTarea)
END