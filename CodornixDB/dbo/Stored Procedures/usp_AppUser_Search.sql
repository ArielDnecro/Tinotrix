
CREATE PROCEDURE [dbo].[usp_AppUser_Search]

@username nvarchar(50) = NULL,
@firstname nvarchar(50) = NULL


AS

SET NOCOUNT ON;

SELECT * FROM AppUser WHERE
((@username IS NOT NULL AND username LIKE '%' + @username + '%') OR @username IS NULL) AND
((@firstname IS NOT NULL AND firstname LIKE '%' + @firstname + '%') OR @firstname IS NULL)


