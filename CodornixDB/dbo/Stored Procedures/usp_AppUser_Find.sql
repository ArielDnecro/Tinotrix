
CREATE PROCEDURE [dbo].[usp_AppUser_Find]
@uid uniqueidentifier
AS

SET NOCOUNT ON

SELECT TOP 1 * FROM AppUser WHERE uid = @uid

