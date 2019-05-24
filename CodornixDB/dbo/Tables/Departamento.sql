CREATE TABLE [dbo].[Departamento] (
    [UidDepartamento] UNIQUEIDENTIFIER NOT NULL,
    [VchNombre]       NVARCHAR (50)    NOT NULL,
    [VchDescripcion]  NVARCHAR (200)   NULL,
    [VchURL]          NVARCHAR (255)   NULL,
    [UidStatus]       UNIQUEIDENTIFIER NOT NULL,
    [UidSucursal]     UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_Departamento] PRIMARY KEY CLUSTERED ([UidDepartamento] ASC),
    CONSTRAINT [FK_Departamento_Estatus] FOREIGN KEY ([UidStatus]) REFERENCES [dbo].[Estatus] ([UidStatus]),
    CONSTRAINT [FK_Departamento_Sucursal] FOREIGN KEY ([UidSucursal]) REFERENCES [dbo].[Sucursal] ([UidSucursal])
);

