ALTER TABLE [dbo].[MedicalAppointments]
	ADD CONSTRAINT [FK_MedicalAppointments_ScheduleUnitId_ScheduleUnits_ScheduleUnitId] 
	FOREIGN KEY ([ScheduleUnitId])
	REFERENCES [dbo].[ScheduleUnits] ([ScheduleUnitId])
	ON DELETE SET NULL