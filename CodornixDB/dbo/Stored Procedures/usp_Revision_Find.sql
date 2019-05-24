CREATE PROCEDURE usp_Revision_Find
@UidRevision uniqueidentifier
AS

SET NOCOUNT ON

SELECT TOP(1) * FROM Revision WHERE UidRevision = @UidRevision