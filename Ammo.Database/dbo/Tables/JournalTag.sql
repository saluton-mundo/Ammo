CREATE TABLE [dbo].[JournalTag]
(
	[TagId] INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    [JournalId] INT NOT NULL, 
    [TagContent] NVARCHAR(100) NOT NULL, 
    [Deleted] BIT NOT NULL DEFAULT 'False', 
    [CreateDate] DATETIME NOT NULL, 
    [LastActionDate] DATETIME NOT NULL, 
    [CreateUserId] UNIQUEIDENTIFIER NOT NULL, 
    [LastActionUserId] UNIQUEIDENTIFIER NOT NULL
)
