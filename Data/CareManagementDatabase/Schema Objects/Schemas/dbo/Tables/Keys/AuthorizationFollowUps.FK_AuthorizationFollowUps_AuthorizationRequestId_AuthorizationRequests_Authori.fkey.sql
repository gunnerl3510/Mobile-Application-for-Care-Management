ALTER TABLE [dbo].[AuthorizationFollowUps]
	ADD CONSTRAINT [FK_AuthorizationFollowUps_AuthorizationRequestId_AuthorizationRequests_AuthorizationRequestId] 
	FOREIGN KEY ([AuthorizationRequestId])
	REFERENCES [dbo].[AuthorizationRequests] ([AuthorizationRequestId])	
	ON DELETE CASCADE