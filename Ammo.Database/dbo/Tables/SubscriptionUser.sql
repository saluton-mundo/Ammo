CREATE TABLE [dbo].[SubscriptionUser]
(
	[SubscriptionId] INT NOT NULL PRIMARY KEY, 
    [UserId] UNIQUEIDENTIFIER NOT NULL, 
    [Deleted] BIT NOT NULL DEFAULT 'False', 
    [LastPaymentDate] DATETIME NOT NULL, 
    [NextPaymentDate] DATETIME NOT NULL, 
    CONSTRAINT [FK_SubscriptionUser_UserId] FOREIGN KEY ([UserId]) REFERENCES [IdentityUser]([UserId])
)
