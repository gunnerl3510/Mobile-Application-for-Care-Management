CREATE TABLE [dbo].[AuthorizationRequests]
(
	[AuthorizationRequestId]	[int]				IDENTITY(1, 1),
	[AccountId]					[int]				NOT NULL,
	[InsurerId]					[int]				NOT NULL,
	[Description]				[nvarchar](512)		NULL,
	[CurrentVersion]			[rowversion]		NOT NULL
)
