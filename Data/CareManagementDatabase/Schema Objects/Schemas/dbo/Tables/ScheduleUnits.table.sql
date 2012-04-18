CREATE TABLE [dbo].[ScheduleUnits]
(
	[ScheduleUnitId]			[int]				NOT NULL,
	[Description]				[nvarchar](100)		NOT NULL,
	[CurrentVersion]			[rowversion]		NOT NULL
)