CREATE TABLE [dbo].[TipoFrecuencia] (
    [UidTipoFrecuencia] UNIQUEIDENTIFIER CONSTRAINT [DF_TipoFrecuencia_UidTipoFrecuencia] DEFAULT (newid()) NOT NULL,
    [VchTipoFrecuencia] NVARCHAR (50)    NULL,
    CONSTRAINT [PK_TipoFrecuencia] PRIMARY KEY CLUSTERED ([UidTipoFrecuencia] ASC)
);



