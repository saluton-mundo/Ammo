CREATE TABLE [dbo].[ActivityLogEntry]
(
	[ActivityLogEntryId] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	[ActivityLogId] INT NOT NULL,
	[ActivityLogEntryMarkId] INT NOT NULL,
	[ActivityId] INT NOT NULL,
	[EntryDate] DATETIME NOT NULL,
	[Deleted] BIT NOT NULL DEFAULT 'False',
	[CreateDate] DATETIME NOT NULL,
	[LastActionDate] DATETIME NOT NULL,
	[CreateUserId] UNIQUEIDENTIFIER NOT NULL,
	[LastActionUserId] UNIQUEIDENTIFIER NOT NULL, 
    CONSTRAINT [FK_ActivityLogEntry_ActivityLog] FOREIGN KEY ([ActivityLogId]) REFERENCES [ActivityLog]([ActivityLogId]),
    CONSTRAINT [FK_ActivityLogEntry_Activity] FOREIGN KEY ([ActivityId]) REFERENCES [ActivityLogActivity]([ActivityId]), 
    CONSTRAINT [FK_ActivityLogEntry_EntryMark] FOREIGN KEY ([ActivityLogEntryMarkId]) REFERENCES [ActivityLogEntryMark]([ActivityLogEntryMarkId])
)
