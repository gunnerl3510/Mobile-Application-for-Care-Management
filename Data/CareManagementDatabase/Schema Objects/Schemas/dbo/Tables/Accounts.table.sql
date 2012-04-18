CREATE TABLE [dbo].[Accounts]
(
	AccountId					[int]				IDENTITY(1, 1),
	UserId						[uniqueidentifier]	NULL,
	Name						[nvarchar](256)		NOT NULL,
	EmailAddress				[nvarchar](256)		NOT NULL,
	CurrentVersion				[rowversion]		NOT NULL
)