CREATE TABLE [dbo].[AuthorizationNotes]
(
	[AuthorizationNoteId]		[int]				IDENTITY(1, 1),
	[AccountId]					[int]				NOT NULL,
	[AuthorizationRequestId]	[int]				NOT NULL,
	[Created]					[datetimeoffset]	NOT NULL,
	[Note]						[nvarchar](512)		NOT NULL,
	[CurrentVersion]			[rowversion]		NOT NULL
)