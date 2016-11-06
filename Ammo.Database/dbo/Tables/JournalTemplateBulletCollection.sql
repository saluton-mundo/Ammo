CREATE TABLE [dbo].[JournalTemplateBulletCollection]
(
	[TemplateId] INT NOT NULL , 
    [BulletCollectionId] NCHAR(10) NOT NULL, 
    [Deleted] BIT NOT NULL DEFAULT 'False', 
    [CreateDate] DATETIME NOT NULL, 
    [LastActionDate] DATETIME NOT NULL, 
    [CreateUserId] UNIQUEIDENTIFIER NOT NULL, 
    [LastActionUserId] UNIQUEIDENTIFIER NOT NULL, 
    PRIMARY KEY ([TemplateId], [BulletCollectionId])
)
