CREATE TABLE [dbo].[UsuarioTelefono] (
    [UidUsuario]  UNIQUEIDENTIFIER NOT NULL,
    [UidTelefono] UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_UsuarioTelefono] PRIMARY KEY CLUSTERED ([UidUsuario] ASC, [UidTelefono] ASC),
    CONSTRAINT [FK_UsuarioTelefono_Telefono] FOREIGN KEY ([UidTelefono]) REFERENCES [dbo].[Telefono] ([UidTelefono]),
    CONSTRAINT [FK_UsuarioTelefono_Usuario] FOREIGN KEY ([UidUsuario]) REFERENCES [dbo].[Usuario] ([UidUsuario])
);

