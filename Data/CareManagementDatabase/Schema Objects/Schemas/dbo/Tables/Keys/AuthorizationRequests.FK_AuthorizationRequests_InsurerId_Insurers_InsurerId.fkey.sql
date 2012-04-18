ALTER TABLE [dbo].[AuthorizationRequests]
	ADD CONSTRAINT [FK_AuthorizationRequests_InsurerId_Insurers_InsurerId] 
	FOREIGN KEY ([InsurerId])
	REFERENCES [dbo].[Insurers] ([InsurerId])
	ON DELETE CASCADE