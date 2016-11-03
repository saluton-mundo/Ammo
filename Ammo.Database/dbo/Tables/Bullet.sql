CREATE TABLE [dbo].[Bullet] (
    [BulletId]         INT              IDENTITY (1, 1) NOT NULL,
    [Name]             NVARCHAR (255)   NOT NULL,
    [ImageUri]         NVARCHAR (255)   NOT NULL,
    [Deleted]          BIT              CONSTRAINT [DF_Bullet_Deleted] DEFAULT ('False') NOT NULL,
    [CreateDate]       DATETIME         NOT NULL,
    [LastActionDate]   DATETIME         NOT NULL,
    [CreateUserId]     UNIQUEIDENTIFIER NOT NULL,
    [LastActionUserId] UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_Bullet] PRIMARY KEY CLUSTERED ([BulletId] ASC)
);

