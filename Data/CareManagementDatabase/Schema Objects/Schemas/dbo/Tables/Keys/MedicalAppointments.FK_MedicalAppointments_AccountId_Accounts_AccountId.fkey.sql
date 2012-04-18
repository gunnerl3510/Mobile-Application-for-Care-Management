ALTER TABLE [dbo].[MedicalAppointments]
	ADD CONSTRAINT [FK_MedicalAppointments_AccountId_Accounts_AccountId] 
	FOREIGN KEY ([AccountId])
	REFERENCES [dbo].[Accounts] ([AccountId])