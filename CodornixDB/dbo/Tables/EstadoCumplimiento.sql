CREATE TABLE [dbo].[EstadoCumplimiento] (
    [UidEstadoCumplimiento] UNIQUEIDENTIFIER CONSTRAINT [DF_EstadoCumplimiento_UidEstadoCumplimiento] DEFAULT (newid()) NOT NULL,
    [VchTipoCumplimiento]   NVARCHAR (50)    NULL,
    CONSTRAINT [PK_EstadoCumplimiento] PRIMARY KEY CLUSTERED ([UidEstadoCumplimiento] ASC)
);



