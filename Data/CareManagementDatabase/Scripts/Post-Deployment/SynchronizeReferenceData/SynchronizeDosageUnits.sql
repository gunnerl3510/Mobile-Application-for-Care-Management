PRINT 'Starting [dbo].[DosageUnits] Synchronization'

GO

IF EXISTS	(
				SELECT	1
				FROM	[INFORMATION_SCHEMA].[TABLES] AS tbls
				WHERE	tbls.[TABLE_NAME]		=	N'SynchDosageUnits'
					AND	tbls.[TABLE_SCHEMA]		=	N'dbo'
			)
BEGIN
	DROP TABLE [dbo].[SynchDosageUnits]
END

GO

CREATE TABLE [dbo].[SynchDosageUnits]
(
	[DosageUnitId]			[int]			NOT NULL,
	[Unit]					[nvarchar](100)	NOT NULL
)

GO

SET NOCOUNT ON

INSERT	[dbo].[SynchDosageUnits] ([DosageUnitId], [Unit])
VALUES	(1, 'Tablet'),
		(2, 'Milliliter'),
		(3, 'Milligram'),
		(4, 'Teaspoon'),
		(5, 'Tablespoon')
									   
SET NOCOUNT OFF

GO

MERGE	[dbo].[DosageUnits] AS Target
USING	[dbo].[SynchDosageUnits] AS Source 
		ON	Target.[DosageUnitId]	=	Source.[DosageUnitId]
WHEN	MATCHED
	AND	Target.[Unit]	<>	Source.[Unit]   
		THEN UPDATE     
			SET	Target.[Unit]		=	Source.[Unit]
WHEN NOT MATCHED BY TARGET
		THEN
			INSERT      ([DosageUnitId], [Unit])
			VALUES      ([DosageUnitId], [Unit])
WHEN NOT MATCHED BY SOURCE 
		THEN DELETE;
					  
GO

DROP TABLE [dbo].[SynchDosageUnits];

GO

PRINT 'Done Synchronizing [dbo].[SynchDosageUnits]'

GO