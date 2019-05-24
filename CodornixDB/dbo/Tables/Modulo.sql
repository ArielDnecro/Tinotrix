CREATE TABLE [dbo].[Modulo] (
    [UidModulo]      UNIQUEIDENTIFIER CONSTRAINT [DF_Modulo_UidModulo] DEFAULT (newid()) NOT NULL,
    [VchModulo]      NVARCHAR (50)    NULL,
    [VchURL]         NVARCHAR (50)    NULL,
    [UidNivelAcceso] UNIQUEIDENTIFIER NULL,
    [BitDenegable]   BIT              NULL,
    CONSTRAINT [PK_Modulo] PRIMARY KEY CLUSTERED ([UidModulo] ASC),
    CONSTRAINT [FK_Modulo_NivelAcceso] FOREIGN KEY ([UidNivelAcceso]) REFERENCES [dbo].[NivelAcceso] ([UidNivelAcceso])
);

