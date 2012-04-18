PRINT 'Starting [dbo].[ScheduleUnits] Synchronization'

GO

IF EXISTS	(
				SELECT	1
				FROM	[INFORMATION_SCHEMA].[TABLES] AS tbls
				WHERE	tbls.[TABLE_NAME]		=	N'SynchScheduleUnits'
					AND	tbls.[TABLE_SCHEMA]		=	N'dbo'
			)
BEGIN
	DROP TABLE [dbo].[SynchScheduleUnits]
END

GO

CREATE TABLE [dbo].[SynchScheduleUnits]
(
	[ScheduleUnitId]		[int]			NOT NULL,
	[Description]			[nvarchar](100)	NOT NULL
)

GO

SET NOCOUNT ON

INSERT	[dbo].[SynchScheduleUnits] ([ScheduleUnitId], [Description])
VALUES	(1, 'Minutes'),
		(2, 'Hours')
									   
SET NOCOUNT OFF

GO

MERGE	[dbo].[ScheduleUnits] AS Target
USING	[dbo].[SynchScheduleUnits] AS Source 
		ON	Target.[ScheduleUnitId]	=	Source.[ScheduleUnitId]
WHEN	MATCHED
	AND	Target.[Description]	<>	Source.[Description]   
		THEN UPDATE     
			SET	Target.[Description]		=	Source.[Description]
WHEN NOT MATCHED BY TARGET
		THEN
			INSERT      ([ScheduleUnitId], [Description])
			VALUES      ([ScheduleUnitId], [Description])
WHEN NOT MATCHED BY SOURCE 
		THEN DELETE;
					  
GO

DROP TABLE [dbo].[SynchScheduleUnits];

GO

PRINT 'Done Synchronizing [dbo].[SynchScheduleUnits]'

GO