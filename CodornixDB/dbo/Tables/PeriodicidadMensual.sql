CREATE TABLE [dbo].[PeriodicidadMensual] (
    [UidPeriodicidad] UNIQUEIDENTIFIER NOT NULL,
    [IntDiasMes]      INT              NOT NULL,
    [IntDiasSemana]   INT              NULL,
    CONSTRAINT [PK_PeriodicidadMensual] PRIMARY KEY CLUSTERED ([UidPeriodicidad] ASC),
    CONSTRAINT [FK_PeriodicidadMensual_Periodicidad1] FOREIGN KEY ([UidPeriodicidad]) REFERENCES [dbo].[Periodicidad] ([UidPeriodicidad])
);





