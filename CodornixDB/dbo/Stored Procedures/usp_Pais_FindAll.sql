﻿CREATE PROCEDURE [dbo].[usp_Pais_FindAll]

AS

SET NOCOUNT ON

SELECT * FROM Pais ORDER BY VchNombre
