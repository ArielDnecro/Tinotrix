CREATE TABLE [dbo].[Tarea] (
    [UidTarea]          UNIQUEIDENTIFIER CONSTRAINT [DF_Tarea_UidTarea] DEFAULT (newid()) NOT NULL,
    [VchNombre]         NVARCHAR (50)    NULL,
    [VchDescripcion]    NVARCHAR (200)   NULL,
    [UidAntecesorTarea] UNIQUEIDENTIFIER NULL,
    [UidUnidadMedida]   UNIQUEIDENTIFIER NULL,
    [UidPeriodicidad]   UNIQUEIDENTIFIER NULL,
    [UidTipoMedicion]   UNIQUEIDENTIFIER NULL,
    [TmHora]            TIME (2)         NULL,
    [IntTolerancia]     INT              NULL,
    [UidTipoTarea]      UNIQUEIDENTIFIER NULL,
    [UidStatus]         UNIQUEIDENTIFIER NULL,
    [BitFoto]           BIT              NULL,
    [BitCaducado]       BIT              NULL,
    CONSTRAINT [PK_Tarea] PRIMARY KEY CLUSTERED ([UidTarea] ASC),
    CONSTRAINT [FK_Tarea_Estatus] FOREIGN KEY ([UidStatus]) REFERENCES [dbo].[Estatus] ([UidStatus]),
    CONSTRAINT [FK_Tarea_Medicion] FOREIGN KEY ([UidTipoMedicion]) REFERENCES [dbo].[Medicion] ([UidTipoMedicion]),
    CONSTRAINT [FK_Tarea_Periodicidad] FOREIGN KEY ([UidPeriodicidad]) REFERENCES [dbo].[Periodicidad] ([UidPeriodicidad]),
    CONSTRAINT [FK_Tarea_Tarea] FOREIGN KEY ([UidAntecesorTarea]) REFERENCES [dbo].[Tarea] ([UidTarea]),
    CONSTRAINT [FK_Tarea_TipoTarea] FOREIGN KEY ([UidTipoTarea]) REFERENCES [dbo].[TipoTarea] ([UidTipoTarea]),
    CONSTRAINT [FK_Tarea_UnidadMedida] FOREIGN KEY ([UidUnidadMedida]) REFERENCES [dbo].[UnidadMedida] ([UidUnidadMedida])
);













