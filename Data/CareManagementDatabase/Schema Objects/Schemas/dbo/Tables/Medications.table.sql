CREATE TABLE [dbo].[Medications]
(
	[MedicationId]				[int]				IDENTITY(1, 1),
	[AccountId]					[int]				NOT NULL,
	[Name]						[nvarchar](256)		NOT NULL,
	[DosageUnitId]				[int]				NULL,
	[Quantity]					[decimal]			NULL,
	[CurrentVersion]			[rowversion]		NOT NULL
)