CREATE TABLE [dbo].[JournalBulletCollection]
(
	[JournalId] INT NOT NULL , 
    [BulletCollectionId] INT NOT NULL, 
    [Deleted] BIT NOT NULL DEFAULT 'False', 
    [CreateDate] DATETIME NOT NULL, 
    [LastActionDate] DATETIME NOT NULL , 
    [CreateUserId] UNIQUEIDENTIFIER NOT NULL, 
    [LastActionUserId] UNIQUEIDENTIFIER NOT NULL, 
    PRIMARY KEY ([JournalId], [BulletCollectionId])
) 
