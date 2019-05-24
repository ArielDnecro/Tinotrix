CREATE TABLE [dbo].[InicioTurno] (
    [UidInicioTurno]    UNIQUEIDENTIFIER NOT NULL,
    [UidUsuario]        UNIQUEIDENTIFIER NULL,
    [DtFechaHoraInicio] DATETIME         NULL,
    [DtFechaHoraFin]    DATETIME         NULL,
    [IntNoCompletado]   INT              NULL,
    [UidPeriodo]        UNIQUEIDENTIFIER NULL,
    CONSTRAINT [PK_InicioTurno] PRIMARY KEY CLUSTERED ([UidInicioTurno] ASC),
    CONSTRAINT [FK_InicioTurno_Periodo] FOREIGN KEY ([UidPeriodo]) REFERENCES [dbo].[Periodo] ([UidPeriodo]),
    CONSTRAINT [FK_InicioTurno_Usuario] FOREIGN KEY ([UidUsuario]) REFERENCES [dbo].[Usuario] ([UidUsuario])
);





