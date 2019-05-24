CREATE TABLE [dbo].[Periodo] (
    [UidPeriodo]      UNIQUEIDENTIFIER CONSTRAINT [DF_Periodo_UidPeriodo] DEFAULT (newid()) NOT NULL,
    [DtFechaInicio]   DATE             NOT NULL,
    [DtFechaFin]      DATE             NULL,
    [UidTurno]        UNIQUEIDENTIFIER NOT NULL,
    [UidUsuario]      UNIQUEIDENTIFIER NOT NULL,
    [UidDepartamento] UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_Periodo] PRIMARY KEY CLUSTERED ([UidPeriodo] ASC),
    CONSTRAINT [FK_Periodo_Departamento] FOREIGN KEY ([UidDepartamento]) REFERENCES [dbo].[Departamento] ([UidDepartamento]),
    CONSTRAINT [FK_Periodo_Turno] FOREIGN KEY ([UidTurno]) REFERENCES [dbo].[Turno] ([UidTurno]),
    CONSTRAINT [FK_Periodo_Usuario] FOREIGN KEY ([UidUsuario]) REFERENCES [dbo].[Usuario] ([UidUsuario])
);



