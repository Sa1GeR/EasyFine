CREATE TABLE [dbo].[identity_UserRoles] (
    [RoleId] INT NOT NULL,
    [UserId] INT NOT NULL,
    CONSTRAINT [PK_dbo.identity_UserRoles] PRIMARY KEY CLUSTERED ([RoleId] ASC, [UserId] ASC),
    CONSTRAINT [FK_dbo.identity_UserRoles_dbo.identity_Roles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[identity_Roles] ([Id]),
    CONSTRAINT [FK_dbo.identity_UserRoles_dbo.identity_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES   [dbo].[identity_Users]   ([Id])
);

