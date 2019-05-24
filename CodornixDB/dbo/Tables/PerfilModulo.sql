CREATE TABLE [dbo].[PerfilModulo] (
    [UidPerfil]  UNIQUEIDENTIFIER NULL,
    [UidModuloT] UNIQUEIDENTIFIER NULL,
    CONSTRAINT [FK_PerfilModulo_ModuloTemporal] FOREIGN KEY ([UidModuloT]) REFERENCES [dbo].[ModuloTemporal] ([UidModuloT]),
    CONSTRAINT [FK_PerfilModulo_Perfil] FOREIGN KEY ([UidPerfil]) REFERENCES [dbo].[Perfil] ([UidPerfil])
);

