CREATE TABLE [dbo].[Dias] (
    [UidDias] UNIQUEIDENTIFIER CONSTRAINT [DF_Dias_UidDias] DEFAULT (newid()) NOT NULL,
    [VchDias] NVARCHAR (50)    NULL,
    [IntDias] INT              NULL,
    CONSTRAINT [PK_Dias] PRIMARY KEY CLUSTERED ([UidDias] ASC)
);

