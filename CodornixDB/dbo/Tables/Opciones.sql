CREATE TABLE [dbo].[Opciones] (
    [UidOpciones] UNIQUEIDENTIFIER NOT NULL,
    [VchOpciones] NVARCHAR (50)    NULL,
    [UidTarea]    UNIQUEIDENTIFIER NULL,
    CONSTRAINT [PK_Opciones] PRIMARY KEY CLUSTERED ([UidOpciones] ASC),
    CONSTRAINT [FK_Opciones_Tarea] FOREIGN KEY ([UidTarea]) REFERENCES [dbo].[Tarea] ([UidTarea])
);



