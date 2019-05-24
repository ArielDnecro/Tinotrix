CREATE TABLE [dbo].[Revision] (
    [UidRevision]     UNIQUEIDENTIFIER NOT NULL,
    [UidCumplimiento] UNIQUEIDENTIFIER NOT NULL,
    [UidUsuario]      UNIQUEIDENTIFIER NOT NULL,
    [DtFechaHora]     DATETIME         NOT NULL,
    [BitCorrecto]     BIT              NOT NULL,
    [BitValor]        BIT              NULL,
    [DcValor1]        DECIMAL (18, 4)  NULL,
    [DcValor2]        DECIMAL (18, 4)  NULL,
    [UidOpcion]       UNIQUEIDENTIFIER NULL,
    [VchNotas]        NVARCHAR (200)   NULL,
    CONSTRAINT [PK_Revision] PRIMARY KEY CLUSTERED ([UidRevision] ASC),
    CONSTRAINT [FK_Revision_Cumplimiento] FOREIGN KEY ([UidCumplimiento]) REFERENCES [dbo].[Cumplimiento] ([UidCumplimiento]),
    CONSTRAINT [FK_Revision_Usuario] FOREIGN KEY ([UidUsuario]) REFERENCES [dbo].[Usuario] ([UidUsuario])
);



