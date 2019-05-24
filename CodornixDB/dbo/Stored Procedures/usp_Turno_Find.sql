CREATE PROCEDURE usp_Turno_Find 
@UidTurno uniqueidentifier
AS

SET NOCOUNT ON

SELECT TOP(1) * FROM Turno