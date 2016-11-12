CREATE TABLE [dbo].[ActivityLog]
(
	[ActivityLogId] INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    [JournalId] INT NOT NULL, 
    [OwnerId] UNIQUEIDENTIFIER NOT NULL, 
    [JournalPageNo] INT NULL DEFAULT 0, 
    [LogMonth] DATETIME NOT NULL, 
    [Deleted] BIT NOT NULL DEFAULT 'False',
    [CreateDate] DATETIME NOT NULL, 
    [LastActionDate] DATETIME NOT NULL, 
    [CreateUserId] UNIQUEIDENTIFIER NOT NULL, 
    [LastActionUserId] UNIQUEIDENTIFIER NOT NULL, 
    CONSTRAINT [FK_ActivityLog_Journal] FOREIGN KEY ([JournalId]) REFERENCES [Journal]([JournalId]), 
    CONSTRAINT [FK_ActivityLog_Owner] FOREIGN KEY ([OwnerId]) REFERENCES [IdentityUser]([UserId])
)
