CREATE TABLE [dbo].[MedicalAppointments]
(
	[MedicalAppointmentId]		[int]				IDENTITY(1, 1),
	[AccountId]					[int]				NOT NULL,
	[ProviderId]				[int]				NOT NULL,
	[AppointmentDateTime]		[datetimeoffset]	NOT NULL,
	[ScheduleUnitId]			[int]				NULL,
	[Length]					[decimal]			NULL,
	[Description]				[nvarchar](256)		NULL,
	[CurrentVersion]			[rowversion]		NOT NULL
)