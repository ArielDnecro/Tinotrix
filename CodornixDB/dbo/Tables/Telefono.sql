CREATE TABLE [dbo].[Telefono] (
    [UidTelefono]     UNIQUEIDENTIFIER NOT NULL,
    [VchTelefono]     NVARCHAR (20)    NOT NULL,
    [UidTipoTelefono] UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_Telefono] PRIMARY KEY CLUSTERED ([UidTelefono] ASC),
    CONSTRAINT [FK_Telefono_TipoTelefono] FOREIGN KEY ([UidTipoTelefono]) REFERENCES [dbo].[TipoTelefono] ([UidTipoTelefono])
);

