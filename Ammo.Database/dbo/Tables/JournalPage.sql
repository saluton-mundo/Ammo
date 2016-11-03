CREATE TABLE [dbo].[JournalPage]
(
	[JouranlPageId] INT NOT NULL  IDENTITY(1,1), 
    [JournalId] INT NOT NULL, 
    [JournalTemplateId] INT NOT NULL, 
    [Deleted] BIT NOT NULL DEFAULT 'false', 
    [CreateDate] DATETIME NOT NULL, 
    [LastActionDate] DATETIME NOT NULL, 
    [CreateUserId] UNIQUEIDENTIFIER NOT NULL, 
    [LastActionUserID] UNIQUEIDENTIFIER NOT NULL, 
    PRIMARY KEY ([JouranlPageId], [JournalId])
)
