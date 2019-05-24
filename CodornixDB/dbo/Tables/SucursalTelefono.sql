CREATE TABLE [dbo].[SucursalTelefono] (
    [UidSucursal] UNIQUEIDENTIFIER NOT NULL,
    [UidTelefono] UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_SucursalTelefono] PRIMARY KEY CLUSTERED ([UidSucursal] ASC, [UidTelefono] ASC),
    CONSTRAINT [FK_SucursalTelefono_Sucursal] FOREIGN KEY ([UidSucursal]) REFERENCES [dbo].[Sucursal] ([UidSucursal]),
    CONSTRAINT [FK_SucursalTelefono_Telefono] FOREIGN KEY ([UidTelefono]) REFERENCES [dbo].[Telefono] ([UidTelefono])
);

