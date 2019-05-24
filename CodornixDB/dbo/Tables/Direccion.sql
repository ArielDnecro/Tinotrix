CREATE TABLE [dbo].[Direccion] (
    [UidDireccion]  UNIQUEIDENTIFIER NOT NULL,
    [UidPais]       UNIQUEIDENTIFIER NOT NULL,
    [UidEstado]     UNIQUEIDENTIFIER NOT NULL,
    [VchMunicipio]  NVARCHAR (30)    NOT NULL,
    [VchCiudad]     NVARCHAR (30)    NOT NULL,
    [VchColonia]    NVARCHAR (20)    NOT NULL,
    [VchCalle]      NVARCHAR (20)    NOT NULL,
    [VchConCalle]   NVARCHAR (20)    NOT NULL,
    [VchYCalle]     NVARCHAR (20)    NULL,
    [VchNoExt]      NVARCHAR (20)    NOT NULL,
    [VchNoInt]      NVARCHAR (20)    NULL,
    [VchReferencia] NVARCHAR (200)   NULL,
    CONSTRAINT [PK_Direccion] PRIMARY KEY CLUSTERED ([UidDireccion] ASC),
    CONSTRAINT [FK_Direccion_Estado] FOREIGN KEY ([UidEstado]) REFERENCES [dbo].[Estado] ([UidEstado]),
    CONSTRAINT [FK_Direccion_Pais] FOREIGN KEY ([UidPais]) REFERENCES [dbo].[Pais] ([UidPais])
);

