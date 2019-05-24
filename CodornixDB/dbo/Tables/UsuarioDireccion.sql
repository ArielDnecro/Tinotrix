CREATE TABLE [dbo].[UsuarioDireccion] (
    [UidUsuario]   UNIQUEIDENTIFIER NOT NULL,
    [UidDireccion] UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_UsuarioDireccion] PRIMARY KEY CLUSTERED ([UidUsuario] ASC, [UidDireccion] ASC),
    CONSTRAINT [FK_UsuarioDireccion_Direccion] FOREIGN KEY ([UidDireccion]) REFERENCES [dbo].[Direccion] ([UidDireccion]),
    CONSTRAINT [FK_UsuarioDireccion_Usuario] FOREIGN KEY ([UidUsuario]) REFERENCES [dbo].[Usuario] ([UidUsuario])
);

