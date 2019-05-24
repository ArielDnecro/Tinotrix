CREATE TABLE [dbo].[Acceso] (
    [UidPerfil] UNIQUEIDENTIFIER NOT NULL,
    [UidModulo] UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_Acceso] PRIMARY KEY CLUSTERED ([UidPerfil] ASC, [UidModulo] ASC),
    CONSTRAINT [FK_Acceso_Modulo] FOREIGN KEY ([UidModulo]) REFERENCES [dbo].[Modulo] ([UidModulo]),
    CONSTRAINT [FK_Acceso_Perfil] FOREIGN KEY ([UidPerfil]) REFERENCES [dbo].[Perfil] ([UidPerfil])
);

