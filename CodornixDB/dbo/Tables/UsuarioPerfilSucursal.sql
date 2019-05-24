CREATE TABLE [dbo].[UsuarioPerfilSucursal] (
    [UidUsuario]  UNIQUEIDENTIFIER NOT NULL,
    [UidPerfil]   UNIQUEIDENTIFIER NOT NULL,
    [UidSucursal] UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_UsuarioPerfilSucursal] PRIMARY KEY CLUSTERED ([UidUsuario] ASC, [UidPerfil] ASC, [UidSucursal] ASC),
    CONSTRAINT [FK_UsuarioPerfilSucursal_Perfil] FOREIGN KEY ([UidPerfil]) REFERENCES [dbo].[Perfil] ([UidPerfil]),
    CONSTRAINT [FK_UsuarioPerfilSucursal_Sucursal] FOREIGN KEY ([UidSucursal]) REFERENCES [dbo].[Sucursal] ([UidSucursal]),
    CONSTRAINT [FK_UsuarioPerfilSucursal_Usuario] FOREIGN KEY ([UidUsuario]) REFERENCES [dbo].[Usuario] ([UidUsuario])
);

