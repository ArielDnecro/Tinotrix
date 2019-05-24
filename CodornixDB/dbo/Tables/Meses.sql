CREATE TABLE [dbo].[Meses] (
    [UidMes]    UNIQUEIDENTIFIER CONSTRAINT [DF_Meses_UidMes] DEFAULT (newid()) NOT NULL,
    [VchMes]    NVARCHAR (50)    NULL,
    [IntNumero] INT              NULL,
    CONSTRAINT [PK_Meses] PRIMARY KEY CLUSTERED ([UidMes] ASC)
);

