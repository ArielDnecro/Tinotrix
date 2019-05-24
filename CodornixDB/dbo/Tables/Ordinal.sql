CREATE TABLE [dbo].[Ordinal] (
    [UidOrdinal] UNIQUEIDENTIFIER CONSTRAINT [DF_Ordinal_UidOrdinal] DEFAULT (newid()) NOT NULL,
    [VchOrdinal] NVARCHAR (50)    NULL,
    [IntOrden]   INT              NULL,
    CONSTRAINT [PK_Ordinal] PRIMARY KEY CLUSTERED ([UidOrdinal] ASC)
);

