ALTER TABLE [dbo].[Medications]
	ADD CONSTRAINT [FK_Medications_AccountId_Accounts_AccountId] 
	FOREIGN KEY ([AccountId])
	REFERENCES [dbo].[Accounts] ([AccountId])
	ON DELETE CASCADE