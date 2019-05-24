CREATE TABLE [dbo].[AsignacionSupervision] (
    [UidAsignacion]   UNIQUEIDENTIFIER NOT NULL,
    [UidUsuario]      UNIQUEIDENTIFIER NOT NULL,
    [UidDepartamento] UNIQUEIDENTIFIER NOT NULL,
    [UidTurno]        UNIQUEIDENTIFIER NOT NULL,
    [DtFechaInicio]   DATE             NOT NULL,
    [DtFechaFin]      DATE             NULL,
    CONSTRAINT [PK_AsignacionSupervision] PRIMARY KEY CLUSTERED ([UidAsignacion] ASC),
    CONSTRAINT [FK_AsignacionSupervision_Departamento] FOREIGN KEY ([UidDepartamento]) REFERENCES [dbo].[Departamento] ([UidDepartamento]),
    CONSTRAINT [FK_AsignacionSupervision_Turno] FOREIGN KEY ([UidTurno]) REFERENCES [dbo].[Turno] ([UidTurno]),
    CONSTRAINT [FK_AsignacionSupervision_Usuario] FOREIGN KEY ([UidUsuario]) REFERENCES [dbo].[Usuario] ([UidUsuario])
);

