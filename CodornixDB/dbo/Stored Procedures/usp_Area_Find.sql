
CREATE PROCEDURE [dbo].[usp_Area_Find]
@UidArea uniqueidentifier

AS

SET NOCOUNT ON

SELECT TOP 1 * FROM Area WHERE UidArea = @UidArea
