CREATE TABLE [dbo].[PrescriptionPickups]
(
	[PrescriptionPickupId]		[int]				IDENTITY(1, 1),
	[AccountId]					[int]				NOT NULL,
	[MedicationId]				[int]				NOT NULL,
	[AppointmentDateTime]		[datetimeoffset]	NOT NULL,
	[CurrentVersion]			[rowversion]		NOT NULL
)