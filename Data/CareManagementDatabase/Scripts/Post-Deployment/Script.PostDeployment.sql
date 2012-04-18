/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

EXEC [dbo].aspnet_RegisterSchemaVersion N'Common', N'1', 1, 1

GO

EXEC [dbo].aspnet_RegisterSchemaVersion N'Membership', N'1', 1, 1

GO

EXEC [dbo].aspnet_RegisterSchemaVersion N'Role Manager', N'1', 1, 1

GO

EXEC sp_tableoption N'aspnet_Membership', 'text in row', 3000

GO

:r .\SynchronizeReferenceData\SynchronizeDosageUnits.sql

GO

:r .\SynchronizeReferenceData\SynchronizeScheduleUnits.sql

GO