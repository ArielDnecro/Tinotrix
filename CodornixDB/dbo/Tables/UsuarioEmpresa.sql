CREATE TABLE [dbo].[UsuarioEmpresa] (
    [UidUsuario] UNIQUEIDENTIFIER NOT NULL,
    [UidEmpresa] UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_UsuarioEmpresa] PRIMARY KEY CLUSTERED ([UidUsuario] ASC, [UidEmpresa] ASC),
    CONSTRAINT [FK_UsuarioEmpresa_Empresa] FOREIGN KEY ([UidEmpresa]) REFERENCES [dbo].[Empresa] ([UidEmpresa]),
    CONSTRAINT [FK_UsuarioEmpresa_Usuario] FOREIGN KEY ([UidUsuario]) REFERENCES [dbo].[Usuario] ([UidUsuario])
);

