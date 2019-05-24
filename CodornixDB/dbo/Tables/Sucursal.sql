CREATE TABLE [dbo].[Sucursal] (
    [UidSucursal]     UNIQUEIDENTIFIER NOT NULL,
    [VchNombre]       NVARCHAR (50)    NOT NULL,
    [DtFechaRegistro] DATE             NOT NULL,
    [UidEmpresa]      UNIQUEIDENTIFIER NOT NULL,
    [UidTipoSucursal] UNIQUEIDENTIFIER NOT NULL,
    [VchRutaImagen]   NVARCHAR (200)   NULL,
    CONSTRAINT [PK_Sucursal] PRIMARY KEY CLUSTERED ([UidSucursal] ASC),
    CONSTRAINT [FK_Sucursal_Empresa] FOREIGN KEY ([UidEmpresa]) REFERENCES [dbo].[Empresa] ([UidEmpresa]),
    CONSTRAINT [FK_Sucursal_TipoSucursal] FOREIGN KEY ([UidTipoSucursal]) REFERENCES [dbo].[TipoSucursal] ([UidTipoSucursal])
);

