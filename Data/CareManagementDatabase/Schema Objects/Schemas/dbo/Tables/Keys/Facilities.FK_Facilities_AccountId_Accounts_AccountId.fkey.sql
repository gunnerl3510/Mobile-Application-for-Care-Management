ALTER TABLE [dbo].[Facilities]
	ADD CONSTRAINT [FK_Facilities_AccountId_Accounts_AccountId] 
	FOREIGN KEY ([AccountId])
	REFERENCES [dbo].[Accounts] ([AccountId])
	ON DELETE CASCADE