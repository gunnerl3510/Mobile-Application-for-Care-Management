ALTER TABLE [dbo].[AuthorizationRequests]
	ADD CONSTRAINT [FK_AuthorizationRequests_AccountId_Accounts_AccountId] 
	FOREIGN KEY ([AccountId])
	REFERENCES [dbo].[Accounts] ([AccountId])