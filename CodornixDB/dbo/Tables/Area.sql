CREATE TABLE [dbo].[Area] (
    [UidArea]         UNIQUEIDENTIFIER NOT NULL,
    [VchNombre]       NVARCHAR (50)    NOT NULL,
    [VchDescripcion]  NVARCHAR (200)   NULL,
    [VchURL]          NVARCHAR (255)   NULL,
    [UidStatus]       UNIQUEIDENTIFIER NOT NULL,
    [UidDepartamento] UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_Area] PRIMARY KEY CLUSTERED ([UidArea] ASC),
    CONSTRAINT [FK_Area_Departamento] FOREIGN KEY ([UidDepartamento]) REFERENCES [dbo].[Departamento] ([UidDepartamento]),
    CONSTRAINT [FK_Area_Estatus] FOREIGN KEY ([UidStatus]) REFERENCES [dbo].[Estatus] ([UidStatus])
);

