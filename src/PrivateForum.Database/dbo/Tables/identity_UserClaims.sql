CREATE TABLE [dbo].[identity_UserClaims] (
    [Id]         INT            IDENTITY (1, 1) NOT NULL,
    [UserId]     INT            NOT NULL,
    [ClaimType]  NVARCHAR (MAX) NULL,
    [ClaimValue] NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_dbo.identity_UserClaims] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.identity_UserClaims_dbo.identity_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES   [dbo].[identity_Users]   ([Id])
);

