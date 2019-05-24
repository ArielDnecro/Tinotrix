CREATE TABLE [dbo].[AccesoUsuario] (
    [UidUsuario] UNIQUEIDENTIFIER NOT NULL,
    [UidModulo]  UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_AccesoUsuario] PRIMARY KEY CLUSTERED ([UidUsuario] ASC, [UidModulo] ASC),
    CONSTRAINT [FK_AccesoUsuario_Modulo] FOREIGN KEY ([UidModulo]) REFERENCES [dbo].[Modulo] ([UidModulo]),
    CONSTRAINT [FK_AccesoUsuario_Usuario] FOREIGN KEY ([UidUsuario]) REFERENCES [dbo].[Usuario] ([UidUsuario])
);

