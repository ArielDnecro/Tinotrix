﻿-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE usp_BuscarPeriodicidadAnual
	-- Add the parameters for the stored procedure here
	@UidPeriodicidad uniqueidentifier
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	select * from PeriodicidadAnual where UidPeriodicidad=@UidPeriodicidad
END