CREATE TABLE [dbo].[EmpresaDireccion] (
    [UidEmpresa]   UNIQUEIDENTIFIER NOT NULL,
    [UidDireccion] UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_EmpresaDireccion] PRIMARY KEY CLUSTERED ([UidEmpresa] ASC, [UidDireccion] ASC),
    CONSTRAINT [FK_EmpresaDireccion_Direccion] FOREIGN KEY ([UidDireccion]) REFERENCES [dbo].[Direccion] ([UidDireccion]),
    CONSTRAINT [FK_EmpresaDireccion_Empresa] FOREIGN KEY ([UidEmpresa]) REFERENCES [dbo].[Empresa] ([UidEmpresa])
);

