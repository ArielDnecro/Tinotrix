
CREATE PROCEDURE [dbo].[usp_AppUser_Update]
@uid uniqueidentifier,
@username nvarchar(50),
@firstname nvarchar(50),
@password nvarchar(50)

AS

SET NOCOUNT ON;

UPDATE AppUser SET  username = @username, firstname = @firstname, password = @password WHERE uid = @uid


