
CREATE PROCEDURE [dbo].[usp_AppUser_Add] 
@username nvarchar(50),
@firstname nvarchar(50),
@password nvarchar(50)

AS

SET NOCOUNT ON;

INSERT INTO AppUser (uid, username, firstname, password) VALUES (NEWID(), @username, @firstname, @password)


