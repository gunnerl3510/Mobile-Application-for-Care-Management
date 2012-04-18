ALTER TABLE [dbo].[Accounts]
    ADD CONSTRAINT [UQ_Accounts_UserId]
    UNIQUE NONCLUSTERED ([UserId])