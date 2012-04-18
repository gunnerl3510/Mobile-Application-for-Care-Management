ALTER TABLE [dbo].[MedicalAppointments]
	ADD CONSTRAINT [FK_MedicalAppointments_ProviderId_Providers_ProviderId] 
	FOREIGN KEY ([ProviderId])
	REFERENCES [dbo].[Providers] ([ProviderId])
	ON DELETE CASCADE