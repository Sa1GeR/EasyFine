CREATE TABLE [dbo].[identity_UserLogins] (
    [UserId]        INT            NOT NULL,
    [LoginProvider] NVARCHAR (MAX) NULL,
    [ProviderKey]   NVARCHAR (MAX) NULL,
    [User_Id]       INT            NULL,
    CONSTRAINT [PK_dbo.identity_UserLogins] PRIMARY KEY CLUSTERED ([UserId] ASC),
    CONSTRAINT [FK_dbo.identity_UserLogins_dbo.identity_Users_User_Id] FOREIGN KEY ([User_Id]) REFERENCES   [dbo].[identity_Users]   ([Id])
);

