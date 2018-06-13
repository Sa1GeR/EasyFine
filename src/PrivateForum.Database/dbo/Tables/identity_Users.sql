CREATE TABLE   [dbo].[identity_Users]   (
    [Id]                           INT              IDENTITY (1, 1) NOT NULL,
    [FirstName]                    NVARCHAR (40)    NOT NULL,
    [LastName]                     NVARCHAR (40)    NOT NULL,
    [Email]                        NVARCHAR (MAX)   NULL,
    [PasswordHash]                 NVARCHAR (MAX)   NULL,
    [UserName]                     NVARCHAR (MAX)   NULL,
    [DateCreated]                  DATETIME         DEFAULT (GETUTCDATE()) NOT NULL,
    [BirthDay]					   DATETIME         DEFAULT (GETUTCDATE()) NOT NULL,
    CONSTRAINT [PK_dbo.identity_Users] PRIMARY KEY CLUSTERED ([Id] ASC)
);

