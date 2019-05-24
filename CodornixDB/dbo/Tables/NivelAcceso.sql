CREATE TABLE [dbo].[NivelAcceso] (
    [UidNivelAcceso]     UNIQUEIDENTIFIER NOT NULL,
    [VchNivelAcceso]     NVARCHAR (50)    NOT NULL,
    [IntJerarquiaAcceso] INT              NULL,
    CONSTRAINT [PK_NivelAcceso] PRIMARY KEY CLUSTERED ([UidNivelAcceso] ASC)
);



