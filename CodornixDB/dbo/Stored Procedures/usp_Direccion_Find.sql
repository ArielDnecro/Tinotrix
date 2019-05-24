CREATE PROCEDURE [dbo].[usp_Direccion_Find]

@UidDireccion uniqueidentifier

AS

SET NOCOUNT ON

SELECT * FROM Direccion d WHERE UidDireccion = @UidDireccion
