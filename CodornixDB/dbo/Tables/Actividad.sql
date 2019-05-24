CREATE TABLE [dbo].[Actividad] (
    [UidActividad] UNIQUEIDENTIFIER NOT NULL,
    [Descripcion]  NVARCHAR (50)    NULL,
    [Orden]        INT              NULL,
    [UidTarea]     UNIQUEIDENTIFIER NULL,
    CONSTRAINT [PK_Actividad] PRIMARY KEY CLUSTERED ([UidActividad] ASC),
    CONSTRAINT [FK_Actividad_Tarea] FOREIGN KEY ([UidTarea]) REFERENCES [dbo].[Tarea] ([UidTarea])
);

