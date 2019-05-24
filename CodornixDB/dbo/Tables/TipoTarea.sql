CREATE TABLE [dbo].[TipoTarea] (
    [UidTipoTarea] UNIQUEIDENTIFIER CONSTRAINT [DF_TipoTarea_UidTipoTarea] DEFAULT (newid()) NOT NULL,
    [VchTipoTarea] NVARCHAR (50)    NULL,
    CONSTRAINT [PK_TipoTarea] PRIMARY KEY CLUSTERED ([UidTipoTarea] ASC)
);

