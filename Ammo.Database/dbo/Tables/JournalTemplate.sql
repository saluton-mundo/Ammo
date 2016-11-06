CREATE TABLE [dbo].[JournalTemplate]
(
	[TemplateId] INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    [Name] NVARCHAR(255) NOT NULL, 
    [TemplateUri] NVARCHAR(255) NOT NULL, 
    [IsPremium] BIT NOT NULL, 
	[Price] FLOAT NOT NULL DEFAULT 0,
	[Deleted] BIT NOT NULL,
    [CreateDate] DATETIME NOT NULL, 
    [LastActionDate] DATETIME NOT NULL, 
    [CreateUserId] UNIQUEIDENTIFIER NOT NULL, 
    [LastActionUserId] UNIQUEIDENTIFIER NOT NULL
)
