CREATE TABLE [dbo].[NivelModulo] (
    [UidModuloT]     UNIQUEIDENTIFIER NOT NULL,
    [UidNivelAcceso] UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_NivelModulo] PRIMARY KEY CLUSTERED ([UidModuloT] ASC, [UidNivelAcceso] ASC),
    CONSTRAINT [FK_NivelModulo_ModuloTemporal] FOREIGN KEY ([UidModuloT]) REFERENCES [dbo].[ModuloTemporal] ([UidModuloT]),
    CONSTRAINT [FK_NivelModulo_NivelAcceso] FOREIGN KEY ([UidNivelAcceso]) REFERENCES [dbo].[NivelAcceso] ([UidNivelAcceso])
);

