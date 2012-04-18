ALTER TABLE [dbo].[Providers]
	ADD CONSTRAINT [FK_Providers_FacilityId_Facilities_FacilityId] 
	FOREIGN KEY ([FacilityId])
	REFERENCES [dbo].[Facilities] ([FacilityId])	
	ON DELETE CASCADE