ALTER TABLE [dbo].[Medications]
	ADD CONSTRAINT [FK_Medications_DosageUnitId_DosageUnits_DosageUnitId] 
	FOREIGN KEY ([DosageUnitId])
	REFERENCES [dbo].[DosageUnits] ([DosageUnitId])	
	ON DELETE SET NULL