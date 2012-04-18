CREATE TABLE [dbo].[Insurers]
(
	[InsurerId]					[int]				IDENTITY(1, 1),
	[AccountId]					[int]				NOT NULL,
	[Name]						[nvarchar](256)		NOT NULL,
	[CurrentVersion]			[rowversion]		NOT NULL
)