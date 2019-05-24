CREATE TABLE [dbo].[TipoSucursal] (
    [UidTipoSucursal] UNIQUEIDENTIFIER NOT NULL,
    [VchTipoSucursal] NVARCHAR (50)    NOT NULL,
    CONSTRAINT [PK_TipoSucursal] PRIMARY KEY CLUSTERED ([UidTipoSucursal] ASC)
);

