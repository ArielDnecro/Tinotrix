CREATE TABLE [dbo].[Cumplimiento] (
    [UidCumplimiento]       UNIQUEIDENTIFIER CONSTRAINT [DF_Cumplimiento_UidCumplimiento] DEFAULT (newid()) NOT NULL,
    [UidTarea]              UNIQUEIDENTIFIER NOT NULL,
    [UidDepartamento]       UNIQUEIDENTIFIER NULL,
    [UidArea]               UNIQUEIDENTIFIER NULL,
    [UidUsuario]            UNIQUEIDENTIFIER NULL,
    [DtFechaProgramada]     DATE             NOT NULL,
    [DtFechaHora]           DATETIME         NULL,
    [UrlFoto]               NVARCHAR (50)    NULL,
    [VchObservacion]        NVARCHAR (200)   NULL,
    [UidEstadoCumplimiento] UNIQUEIDENTIFIER NOT NULL,
    [BitValor]              BIT              NULL,
    [DcValor1]              DECIMAL (18, 4)  NULL,
    [DcValor2]              DECIMAL (18, 4)  NULL,
    [UidOpcion]             UNIQUEIDENTIFIER NULL,
    [UidTurno]              UNIQUEIDENTIFIER NULL,
    [BitPuedeCerrar]        BIT              NULL,
    CONSTRAINT [PK_Cumplimiento] PRIMARY KEY CLUSTERED ([UidCumplimiento] ASC),
    CONSTRAINT [FK_Cumplimiento_Area] FOREIGN KEY ([UidArea]) REFERENCES [dbo].[Area] ([UidArea]),
    CONSTRAINT [FK_Cumplimiento_Departamento] FOREIGN KEY ([UidDepartamento]) REFERENCES [dbo].[Departamento] ([UidDepartamento]),
    CONSTRAINT [FK_Cumplimiento_EstadoCumplimiento] FOREIGN KEY ([UidEstadoCumplimiento]) REFERENCES [dbo].[EstadoCumplimiento] ([UidEstadoCumplimiento]),
    CONSTRAINT [FK_Cumplimiento_Opciones] FOREIGN KEY ([UidOpcion]) REFERENCES [dbo].[Opciones] ([UidOpciones]),
    CONSTRAINT [FK_Cumplimiento_Tarea] FOREIGN KEY ([UidTarea]) REFERENCES [dbo].[Tarea] ([UidTarea]),
    CONSTRAINT [FK_Cumplimiento_Turno] FOREIGN KEY ([UidTurno]) REFERENCES [dbo].[Turno] ([UidTurno]),
    CONSTRAINT [FK_Cumplimiento_Usuario] FOREIGN KEY ([UidUsuario]) REFERENCES [dbo].[Usuario] ([UidUsuario])
);













