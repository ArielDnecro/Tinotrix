CREATE TABLE [dbo].[AppUser] (
    [uid]       UNIQUEIDENTIFIER CONSTRAINT [DF_Users_uid] DEFAULT (newid()) NOT NULL,
    [username]  NVARCHAR (50)    NOT NULL,
    [firstname] NVARCHAR (50)    NOT NULL,
    [password]  NVARCHAR (50)    NOT NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED ([uid] ASC)
);

