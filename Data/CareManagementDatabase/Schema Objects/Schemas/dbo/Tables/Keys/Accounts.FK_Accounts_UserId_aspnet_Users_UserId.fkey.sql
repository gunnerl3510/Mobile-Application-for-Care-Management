ALTER TABLE [dbo].[Accounts]
	ADD CONSTRAINT [FK_Accounts_UserId_aspnet_Users_UserId] 
	FOREIGN KEY ([UserId])
	REFERENCES [dbo].[aspnet_Users] ([UserId])
	ON DELETE SET NULL	