CREATE TABLE [dbo].[SucursalDireccion] (
    [UidSucursal]  UNIQUEIDENTIFIER NOT NULL,
    [UidDireccion] UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_SucursalDireccion] PRIMARY KEY CLUSTERED ([UidSucursal] ASC, [UidDireccion] ASC),
    CONSTRAINT [FK_SucursalDireccion_Direccion] FOREIGN KEY ([UidDireccion]) REFERENCES [dbo].[Direccion] ([UidDireccion]),
    CONSTRAINT [FK_SucursalDireccion_Sucursal] FOREIGN KEY ([UidSucursal]) REFERENCES [dbo].[Sucursal] ([UidSucursal])
);

