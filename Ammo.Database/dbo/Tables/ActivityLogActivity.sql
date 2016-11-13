CREATE TABLE [dbo].[ActivityLogActivity]
(
	[ActivityId] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	[ActivityLogId] INT NOT NULL,
	[Description] NVARCHAR(100) NOT NULL, 
    [Deleted] BIT NOT NULL DEFAULT 'False', 
    [CreateDate] DATETIME NOT NULL, 
    [LastActionDate] DATETIME NOT NULL, 
    [CreateUserId] UNIQUEIDENTIFIER NOT NULL, 
    [LastActionUserId] UNIQUEIDENTIFIER NOT NULL, 
    CONSTRAINT [FK_ActivityLogActivity_ActivityLog] FOREIGN KEY ([ActivityLogId]) REFERENCES [ActivityLog]([ActivityLogId])
)
