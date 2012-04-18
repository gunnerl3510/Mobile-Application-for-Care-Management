ALTER TABLE [dbo].[PrescriptionPickups]
	ADD CONSTRAINT [FK_PrescriptionPickups_MedicationId] 
	FOREIGN KEY ([MedicationId])
	REFERENCES [dbo].[Medications] ([MedicationId])