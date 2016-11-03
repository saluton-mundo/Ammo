CREATE TABLE [dbo].[Subscription]
(
	[SubscriptionId] INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    [Name] NVARCHAR(255) NOT NULL, 
    [Price] FLOAT NOT NULL DEFAULT 0, 
    [Deleted] BIT NOT NULL DEFAULT 'False' 
)
