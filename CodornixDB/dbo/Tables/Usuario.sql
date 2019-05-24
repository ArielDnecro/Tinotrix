CREATE TABLE [dbo].[Usuario] (
    [UidUsuario]         UNIQUEIDENTIFIER CONSTRAINT [DF_Usuario_UidUsuario] DEFAULT (newid()) NOT NULL,
    [VchNombre]          NVARCHAR (50)    NOT NULL,
    [VchApellidoPaterno] NVARCHAR (50)    NOT NULL,
    [VchApellidoMaterno] NVARCHAR (50)    NOT NULL,
    [DtFechaNacimiento]  DATE             NOT NULL,
    [VchCorreo]          NVARCHAR (50)    NULL,
    [DtFechaInicio]      DATE             NOT NULL,
    [DtFechaFin]         DATE             NULL,
    [VchUsuario]         NVARCHAR (50)    NOT NULL,
    [VchPassword]        NVARCHAR (50)    NOT NULL,
    [UidStatus]          UNIQUEIDENTIFIER NOT NULL,
    [VchRutaImagen]      NVARCHAR (200)   NULL,
    CONSTRAINT [PK_Usuario] PRIMARY KEY CLUSTERED ([UidUsuario] ASC),
    CONSTRAINT [FK_Usuario_Estatus] FOREIGN KEY ([UidStatus]) REFERENCES [dbo].[Estatus] ([UidStatus])
);




GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_Usuario]
    ON [dbo].[Usuario]([VchUsuario] ASC);

