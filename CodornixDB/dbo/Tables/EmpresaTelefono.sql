CREATE TABLE [dbo].[EmpresaTelefono] (
    [UidEmpresa]  UNIQUEIDENTIFIER NOT NULL,
    [UidTelefono] UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_EmpresaTelefono] PRIMARY KEY CLUSTERED ([UidEmpresa] ASC, [UidTelefono] ASC),
    CONSTRAINT [FK_EmpresaTelefono_Empresa] FOREIGN KEY ([UidEmpresa]) REFERENCES [dbo].[Empresa] ([UidEmpresa]),
    CONSTRAINT [FK_EmpresaTelefono_Telefono] FOREIGN KEY ([UidTelefono]) REFERENCES [dbo].[Telefono] ([UidTelefono])
);

