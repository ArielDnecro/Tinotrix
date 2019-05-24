CREATE TABLE [dbo].[ModuloTemporal] (
    [UidModuloT] UNIQUEIDENTIFIER NOT NULL,
    [VchModulo]  NVARCHAR (50)    NULL,
    [UidUrl]     UNIQUEIDENTIFIER NULL,
    CONSTRAINT [PK_ModuloTemporal] PRIMARY KEY CLUSTERED ([UidModuloT] ASC)
);



