CREATE TABLE [dbo].[Providers]
(
	[ProviderId]				[int]				IDENTITY(1, 1),
	[FacilityId]				[int]				NOT NULL,
	[Name]						[nvarchar](256)		NOT NULL,
	[CurrentVersion]			[rowversion]		NOT NULL
)