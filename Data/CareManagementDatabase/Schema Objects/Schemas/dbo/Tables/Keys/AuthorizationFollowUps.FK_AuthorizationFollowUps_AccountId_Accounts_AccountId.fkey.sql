ALTER TABLE [dbo].[AuthorizationFollowUps]
	ADD CONSTRAINT [FK_AuthorizationFollowUps_AccountId_Accounts_AccountId] 
	FOREIGN KEY ([AccountId])
	REFERENCES [dbo].[Accounts] ([AccountId])	

