CREATE PROCEDURE [dbo].[usp_Revision_Do]
@UidCumplimiento uniqueidentifier,
@UidUsuario uniqueidentifier,
@BitCorrecto bit,
@BitValor bit = null,
@DcValor1 decimal(18, 4) = null,
@DcValor2 decimal(18, 4) = null,
@UidOpcion uniqueidentifier = null, 
@VchNotas nvarchar(200),
@DtFechaHora datetime
AS

SET NOCOUNT ON

INSERT INTO Revision (UidRevision, UidCumplimiento, UidUsuario, BitCorrecto, BitValor, DcValor1, DcValor2, UidOpcion, VchNotas, DtFechaHora)
	VALUES (NEWID(), @UidCumplimiento, @UidUsuario, @BitCorrecto, @BitValor, @DcValor1, @DcValor2, @UidOpcion, @VchNotas, @DtFechaHora)