CREATE TABLE [dbo].[EmpresaPerfil] (
    [UidPerfil]      UNIQUEIDENTIFIER NOT NULL,
    [VchPerfil]      NVARCHAR (50)    NOT NULL,
    [IntJerarquia]   INT              NOT NULL,
    [UidNivelAcceso] UNIQUEIDENTIFIER NOT NULL,
    [UidHome]        UNIQUEIDENTIFIER NOT NULL,
    [UidEmpresa]     UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_EmpresaPerfil] PRIMARY KEY CLUSTERED ([UidPerfil] ASC),
    CONSTRAINT [FK_EmpresaPerfil_Empresa] FOREIGN KEY ([UidEmpresa]) REFERENCES [dbo].[Empresa] ([UidEmpresa]),
    CONSTRAINT [FK_EmpresaPerfil_Modulo] FOREIGN KEY ([UidHome]) REFERENCES [dbo].[Modulo] ([UidModulo]),
    CONSTRAINT [FK_EmpresaPerfil_NivelAcceso] FOREIGN KEY ([UidNivelAcceso]) REFERENCES [dbo].[NivelAcceso] ([UidNivelAcceso])
);

