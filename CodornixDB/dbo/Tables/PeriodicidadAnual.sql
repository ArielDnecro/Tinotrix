CREATE TABLE [dbo].[PeriodicidadAnual] (
    [UidPeriodicidad] UNIQUEIDENTIFIER NOT NULL,
    [IntDiasMes]      INT              NULL,
    [IntDiasSemanas]  INT              NULL,
    [IntNumero]       INT              NULL,
    CONSTRAINT [PK_PeriodicidadAnual] PRIMARY KEY CLUSTERED ([UidPeriodicidad] ASC),
    CONSTRAINT [FK_PeriodicidadAnual_Periodicidad] FOREIGN KEY ([UidPeriodicidad]) REFERENCES [dbo].[Periodicidad] ([UidPeriodicidad])
);

