CREATE TABLE [dbo].[TipoTelefono] (
    [UidTipoTelefono] UNIQUEIDENTIFIER NOT NULL,
    [VchTipoTelefono] NVARCHAR (50)    NOT NULL,
    CONSTRAINT [PK_TipoTelefono] PRIMARY KEY CLUSTERED ([UidTipoTelefono] ASC)
);

