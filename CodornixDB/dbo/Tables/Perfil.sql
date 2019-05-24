CREATE TABLE [dbo].[Perfil] (
    [UidPerfil]      UNIQUEIDENTIFIER CONSTRAINT [DF_Perfil_PidPerfil] DEFAULT (newid()) NOT NULL,
    [VchPerfil]      NVARCHAR (50)    NOT NULL,
    [IntJerarquia]   TINYINT          NULL,
    [UidNivelAcceso] UNIQUEIDENTIFIER NULL,
    [UidEmpresa]     UNIQUEIDENTIFIER NULL,
    [UidHome]        UNIQUEIDENTIFIER NULL,
    CONSTRAINT [PK_Perfil] PRIMARY KEY CLUSTERED ([UidPerfil] ASC),
    CONSTRAINT [FK_Perfil_Empresa] FOREIGN KEY ([UidEmpresa]) REFERENCES [dbo].[Empresa] ([UidEmpresa]),
    CONSTRAINT [FK_Perfil_Modulo] FOREIGN KEY ([UidHome]) REFERENCES [dbo].[Modulo] ([UidModulo])
);



