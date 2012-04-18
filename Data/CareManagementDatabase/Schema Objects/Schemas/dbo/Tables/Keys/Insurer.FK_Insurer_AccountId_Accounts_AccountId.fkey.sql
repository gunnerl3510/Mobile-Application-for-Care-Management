ALTER TABLE [dbo].[Insurers]
	ADD CONSTRAINT [FK_Insurer_AccountId_Accounts_AccountId] 
	FOREIGN KEY ([AccountId])
	REFERENCES [dbo].[Accounts] ([AccountId])
	ON DELETE CASCADE