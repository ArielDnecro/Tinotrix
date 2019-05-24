CREATE PROCEDURE [dbo].[usp_Cumplimiento_Find]
@UidCumplimiento uniqueidentifier
AS

SET NOCOUNT ON 

SELECT TOP(1) c.*, ec.VchTipoCumplimiento FROM Cumplimiento c JOIN EstadoCumplimiento ec ON c.UidEstadoCumplimiento = ec.UidEstadoCumplimiento WHERE c.UidCumplimiento = @UidCumplimiento