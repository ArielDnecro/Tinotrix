CREATE TABLE [dbo].[DepartamentoTarea] (
    [UidDepartamento] UNIQUEIDENTIFIER NOT NULL,
    [UidTarea]        UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_DepartamentoTarea] PRIMARY KEY CLUSTERED ([UidDepartamento] ASC, [UidTarea] ASC),
    CONSTRAINT [FK_DepartamentoTarea_Departamento] FOREIGN KEY ([UidDepartamento]) REFERENCES [dbo].[Departamento] ([UidDepartamento]),
    CONSTRAINT [FK_DepartamentoTarea_Tarea] FOREIGN KEY ([UidTarea]) REFERENCES [dbo].[Tarea] ([UidTarea])
);

