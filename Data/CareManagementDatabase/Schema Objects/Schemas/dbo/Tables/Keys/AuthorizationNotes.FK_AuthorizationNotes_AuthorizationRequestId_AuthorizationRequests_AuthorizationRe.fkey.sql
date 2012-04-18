ALTER TABLE [dbo].[AuthorizationNotes]
	ADD CONSTRAINT [FK_AuthorizationNotes_AuthorizationRequestId_AuthorizationRequests_AuthorizationRequestId] 
	FOREIGN KEY ([AuthorizationRequestId])
	REFERENCES [dbo].[AuthorizationRequests] ([AuthorizationRequestId])
	ON DELETE CASCADE