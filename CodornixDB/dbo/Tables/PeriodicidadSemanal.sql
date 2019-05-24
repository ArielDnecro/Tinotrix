CREATE TABLE [dbo].[PeriodicidadSemanal] (
    [UidPeriodicidad] UNIQUEIDENTIFIER NOT NULL,
    [BitLunes]        BIT              NOT NULL,
    [BitMartes]       BIT              NOT NULL,
    [BitMiercoles]    BIT              NOT NULL,
    [BitJueves]       BIT              NOT NULL,
    [BitViernes]      BIT              NOT NULL,
    [BitSabado]       BIT              NOT NULL,
    [BitDomingo]      BIT              NOT NULL,
    CONSTRAINT [PK_PeriodicidadSemanal] PRIMARY KEY CLUSTERED ([UidPeriodicidad] ASC),
    CONSTRAINT [FK_PeriodicidadSemanal_Periodicidad] FOREIGN KEY ([UidPeriodicidad]) REFERENCES [dbo].[Periodicidad] ([UidPeriodicidad])
);

