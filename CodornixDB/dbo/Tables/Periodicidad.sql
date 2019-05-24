CREATE TABLE [dbo].[Periodicidad] (
    [UidPeriodicidad]   UNIQUEIDENTIFIER CONSTRAINT [DF_Periodicidad_UidPeriodicidad] DEFAULT (newid()) NOT NULL,
    [IntFrecuencia]     INT              NULL,
    [UidTipoFrecuencia] UNIQUEIDENTIFIER NULL,
    [DtFechaInicio]     DATE             NULL,
    [DtFechaFin]        DATE             NULL,
    CONSTRAINT [PK_Periodicidad] PRIMARY KEY CLUSTERED ([UidPeriodicidad] ASC),
    CONSTRAINT [FK_Periodicidad_TipoFrecuencia] FOREIGN KEY ([UidTipoFrecuencia]) REFERENCES [dbo].[TipoFrecuencia] ([UidTipoFrecuencia])
);





