CREATE TABLE [dbo].aspnet_Users (
    ApplicationId    uniqueidentifier    NOT NULL FOREIGN KEY REFERENCES [dbo].aspnet_Applications(ApplicationId),
    UserId           uniqueidentifier    NOT NULL PRIMARY KEY NONCLUSTERED DEFAULT NEWID(),
    UserName         nvarchar(256)       NOT NULL,
    LoweredUserName  nvarchar(256)	     NOT NULL,
    MobileAlias      nvarchar(16)        DEFAULT NULL,
    IsAnonymous      bit                 NOT NULL DEFAULT 0,
    LastActivityDate DATETIME            NOT NULL)