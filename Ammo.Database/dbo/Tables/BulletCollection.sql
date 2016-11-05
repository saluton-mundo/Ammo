CREATE TABLE [dbo].[BulletCollection] (
    [BulletCollectionId] INT              IDENTITY (1, 1) NOT NULL,
    [Name]               NVARCHAR (255)   NOT NULL,
    [Deleted]            BIT              CONSTRAINT [DF_BulletCollection_Deleted] DEFAULT ('False') NOT NULL,
    [CreateDate]         DATETIME         NOT NULL,
    [LastActionDate]     DATETIME         NOT NULL,
    [CreateUserId]       UNIQUEIDENTIFIER NOT NULL,
    [LastActionUserId]   UNIQUEIDENTIFIER NOT NULL,
    [IsAmmoDefault] BIT NOT NULL DEFAULT 'False', 
    CONSTRAINT [PK_BulletCollection] PRIMARY KEY CLUSTERED ([BulletCollectionId] ASC)
);

