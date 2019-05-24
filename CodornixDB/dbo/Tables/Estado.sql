CREATE TABLE [dbo].[Estado] (
    [UidEstado] UNIQUEIDENTIFIER CONSTRAINT [DF_Estado_UidEstado] DEFAULT (newid()) NOT NULL,
    [UidPais]   UNIQUEIDENTIFIER NOT NULL,
    [VchNombre] NVARCHAR (20)    NOT NULL,
    CONSTRAINT [PK_Estado] PRIMARY KEY CLUSTERED ([UidEstado] ASC),
    CONSTRAINT [FK_Estado_Pais] FOREIGN KEY ([UidPais]) REFERENCES [dbo].[Pais] ([UidPais])
);

