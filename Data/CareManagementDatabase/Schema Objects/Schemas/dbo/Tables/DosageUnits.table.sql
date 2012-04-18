CREATE TABLE [dbo].[DosageUnits]
(
	[DosageUnitId]				[int]				NOT NULL,
	[Unit]						[nvarchar](100)		NOT NULL,
	[CurrentVersion]			[rowversion]		NOT NULL
)
