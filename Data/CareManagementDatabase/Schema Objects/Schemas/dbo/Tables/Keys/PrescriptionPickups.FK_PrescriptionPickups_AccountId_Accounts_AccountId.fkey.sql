ALTER TABLE [dbo].[PrescriptionPickups]
	ADD CONSTRAINT [FK_PrescriptionPickups_AccountId_Accounts_AccountId] 
	FOREIGN KEY ([AccountId])
	REFERENCES [dbo].[Accounts] ([AccountId])