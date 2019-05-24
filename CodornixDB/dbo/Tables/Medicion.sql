CREATE TABLE [dbo].[Medicion] (
    [UidTipoMedicion] UNIQUEIDENTIFIER CONSTRAINT [DF_Medicion_UidTipoMedicion] DEFAULT (newid()) NOT NULL,
    [VchTipoMedicion] NVARCHAR (50)    NULL,
    [orden]           INT              NULL,
    CONSTRAINT [PK_Medicion] PRIMARY KEY CLUSTERED ([UidTipoMedicion] ASC)
);



