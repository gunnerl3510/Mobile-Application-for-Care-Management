ALTER TABLE [dbo].[Insurers]
    ADD CONSTRAINT [UQ_Insurers_AccountId_Name]
    UNIQUE NONCLUSTERED ([AccountId], [Name])