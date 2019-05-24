CREATE TABLE [dbo].[Estatus] (
    [UidStatus] UNIQUEIDENTIFIER CONSTRAINT [DF_Estatus_SidStatus] DEFAULT (newid()) NOT NULL,
    [VchStatus] NVARCHAR (50)    NOT NULL,
    CONSTRAINT [PK_Estatus] PRIMARY KEY CLUSTERED ([UidStatus] ASC)
);

