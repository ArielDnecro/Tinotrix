CREATE TABLE [dbo].[UsuarioPerfilEmpresa] (
    [UidUsuario] UNIQUEIDENTIFIER NOT NULL,
    [UidPerfil]  UNIQUEIDENTIFIER NOT NULL,
    [UidEmpresa] UNIQUEIDENTIFIER NULL,
    CONSTRAINT [PK_UsuarioPerfilEmpresa_1] PRIMARY KEY CLUSTERED ([UidUsuario] ASC, [UidPerfil] ASC),
    CONSTRAINT [FK_UsuarioPerfilEmpresa_Empresa] FOREIGN KEY ([UidEmpresa]) REFERENCES [dbo].[Empresa] ([UidEmpresa]),
    CONSTRAINT [FK_UsuarioPerfilEmpresa_Perfil] FOREIGN KEY ([UidPerfil]) REFERENCES [dbo].[Perfil] ([UidPerfil]),
    CONSTRAINT [FK_UsuarioPerfilEmpresa_Usuario] FOREIGN KEY ([UidUsuario]) REFERENCES [dbo].[Usuario] ([UidUsuario])
);

