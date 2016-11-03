CREATE TABLE [dbo].[Journal]
(
	[JournalId] INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    [OwnerId] UNIQUEIDENTIFIER NOT NULL, 
    [Title] NVARCHAR(75) NOT NULL DEFAULT 'Give your journal a neat-o name!', 
    [Description] NVARCHAR(1024) NOT NULL DEFAULT 'Describe your goals and ambitions for this journal here..', 
    [CoverUrl] NVARCHAR(255) NULL, 

    [Deleted] BIT NOT NULL DEFAULT 0, 
    [CreateDate] DATETIME NOT NULL, 
    [LastActionDate] DATETIME NOT NULL, 
    [LastActionUserId] UNIQUEIDENTIFIER NOT NULL, 
    CONSTRAINT [FK_Journal_Owner] FOREIGN KEY ([OwnerId]) REFERENCES [IdentityUser]([UserId]), 
    CONSTRAINT [FK_Journal_LastActionUser] FOREIGN KEY ([LastActionUserId]) REFERENCES [IdentityUser]([UserId])
)
