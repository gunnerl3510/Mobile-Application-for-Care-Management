CREATE TABLE [dbo].[AuthorizationFollowUps]
(
	[AuthorizationFollowUpId]	[int]				IDENTITY(1, 1),
	[AccountId]					[int]				NOT NULL,
	[AuthorizationRequestId]	[int]				NOT NULL,
	[AppointmentDateTime]		[datetimeoffset]	NOT NULL,
	[Description]				[nvarchar](512)		NULL,
	[CurrentVersion]			[rowversion]		NOT NULL
)