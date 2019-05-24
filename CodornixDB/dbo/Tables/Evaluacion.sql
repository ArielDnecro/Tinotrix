CREATE TABLE [dbo].[Evaluacion] (
    [UidEvaluacion]     UNIQUEIDENTIFIER NOT NULL,
    [VchTipoEvaluacion] NVARCHAR (50)    NULL,
    CONSTRAINT [PK_Evaluacion] PRIMARY KEY CLUSTERED ([UidEvaluacion] ASC)
);

