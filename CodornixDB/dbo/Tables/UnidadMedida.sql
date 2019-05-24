CREATE TABLE [dbo].[UnidadMedida] (
    [UidUnidadMedida] UNIQUEIDENTIFIER CONSTRAINT [DF_UnidadMedida_UidUnidadMedida] DEFAULT (newid()) NOT NULL,
    [VchTipoUnidad]   NVARCHAR (10)    NULL,
    CONSTRAINT [PK_UnidadMedida] PRIMARY KEY CLUSTERED ([UidUnidadMedida] ASC)
);



