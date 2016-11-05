CREATE TABLE [dbo].[BulletCollectionBullet] (
    [BulletCollectionId] INT              NOT NULL,
    [BulletId]           INT              NOT NULL,
    [Deleted]            BIT              CONSTRAINT [DF_BulletCollectionBullet_Deleted] DEFAULT ('False') NOT NULL,
    [CreateDate]         DATETIME         NOT NULL,
    [LastActionDate]     DATETIME         NOT NULL,
    [CreateUserId]       UNIQUEIDENTIFIER NOT NULL,
    [LastActionUserId]   UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_BulletCollectionBullet] PRIMARY KEY CLUSTERED ([BulletCollectionId] ASC, [BulletId] ASC)
);

