﻿CREATE TABLE [dbo].[Turno] (
    [UidTurno] UNIQUEIDENTIFIER NOT NULL,
    [VchTurno] NVARCHAR (20)    NOT NULL,
    CONSTRAINT [PK_Turno] PRIMARY KEY CLUSTERED ([UidTurno] ASC)
);
