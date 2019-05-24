CREATE TABLE [dbo].[TareaArea] (
    [UidTarea] UNIQUEIDENTIFIER NOT NULL,
    [UidArea]  UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_TareaArea] PRIMARY KEY CLUSTERED ([UidTarea] ASC, [UidArea] ASC),
    CONSTRAINT [FK_TareaArea_Area] FOREIGN KEY ([UidArea]) REFERENCES [dbo].[Area] ([UidArea]),
    CONSTRAINT [FK_TareaArea_Tarea] FOREIGN KEY ([UidTarea]) REFERENCES [dbo].[Tarea] ([UidTarea])
);

