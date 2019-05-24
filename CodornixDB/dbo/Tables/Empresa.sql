CREATE TABLE [dbo].[Empresa] (
    [UidEmpresa]         UNIQUEIDENTIFIER NOT NULL,
    [VchNombreComercial] NVARCHAR (50)    NOT NULL,
    [VchRazonSocial]     NVARCHAR (60)    NOT NULL,
    [VchGiro]            NVARCHAR (40)    NOT NULL,
    [ChRFC]              NCHAR (13)       NOT NULL,
    [DtFechaRegistro]    DATE             NOT NULL,
    [VchRutaImagen]      NVARCHAR (200)   NULL,
    CONSTRAINT [PK_Empresa] PRIMARY KEY CLUSTERED ([UidEmpresa] ASC)
);

