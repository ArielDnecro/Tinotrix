CREATE TABLE [dbo].[Encargado] (
    [UidUsuario]         UNIQUEIDENTIFIER NOT NULL,
    [IntMaxAsignaciones] TINYINT          NOT NULL,
    CONSTRAINT [PK_Encargado] PRIMARY KEY CLUSTERED ([UidUsuario] ASC),
    CONSTRAINT [FK_Encargado_Usuario] FOREIGN KEY ([UidUsuario]) REFERENCES [dbo].[Usuario] ([UidUsuario])
);

