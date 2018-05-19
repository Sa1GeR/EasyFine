CREATE TABLE   [dbo].[identity_Users]   (
    [Id]                           INT              IDENTITY (1, 1) NOT NULL,
    [FirstName]                    NVARCHAR (40)    NOT NULL,
    [LastName]                     NVARCHAR (40)    NOT NULL,
	[MiddleName]                   NVARCHAR (40)    NULL,
    [ContactNumber]                NVARCHAR (20)    NOT NULL,
    [RegistrationStateCode]        NVARCHAR (40)    NOT NULL,
    [Email]                        NVARCHAR (MAX)   NULL,
    [EmailConfirmed]               BIT              NOT NULL,
    [PasswordHash]                 NVARCHAR (MAX)   NULL,
    [SecurityStamp]                NVARCHAR (MAX)   NULL,
    [PhoneNumber]                  NVARCHAR (MAX)   NULL,
    [PhoneNumberConfirmed]         BIT              NOT NULL,
    [TwoFactorEnabled]             BIT              NOT NULL,
    [LockoutEndDateUtc]            DATETIME         NULL,
    [LockoutEnabled]               BIT              NOT NULL,
    [AccessFailedCount]            INT              NOT NULL,
    [UserName]                     NVARCHAR (MAX)   NULL,
    [DateCreated]                  DATETIME         DEFAULT ('2016-07-12T16:56:54.180Z') NOT NULL,
    [Status]                       INT    NOT NULL DEFAULT ((0)),
    CONSTRAINT [PK_dbo.identity_Users] PRIMARY KEY CLUSTERED ([Id] ASC)
);

