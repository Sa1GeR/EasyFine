CREATE TABLE [dbo].[forum_UserProfiles]
(
	[Id]                         INT             NOT NULL,
	[IsDeleted]                  BIT             NOT NULL DEFAULT 0,
	[IsBlocked]                  BIT             NOT NULL DEFAULT 0,
	[AvatarURL]                  NVARCHAR (MAX)  NULL,
	[Created]                    DATETIME2 (7)   NOT NULL,
    [Modified]                   DATETIME2 (7)   NOT NULL,
    [CreatedBy]                  NVARCHAR (100)  NOT NULL,
    [ModifiedBy]                 NVARCHAR (100)  NOT NULL,
	[Address] NCHAR(1000) NULL DEFAULT '', 
    CONSTRAINT [PK_dbo.forum_UserProfiles] PRIMARY KEY CLUSTERED ([Id])
)
