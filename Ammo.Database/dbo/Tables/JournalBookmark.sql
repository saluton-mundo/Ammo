CREATE TABLE [dbo].[JournalBookmark]
(
	[BookmarkId] INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    [JournalId] INT NOT NULL, 
    [PageId] INT NOT NULL, 
    [Name] NVARCHAR(100) NULL, 
    [Deleted] BIT NOT NULL DEFAULT 'False', 
    [CreateDate] DATETIME NOT NULL, 
    [LastActionDate] DATETIME NOT NULL, 
    [CreateUserId] UNIQUEIDENTIFIER NOT NULL, 
    [LastActionUserId] UNIQUEIDENTIFIER NOT NULL
)
