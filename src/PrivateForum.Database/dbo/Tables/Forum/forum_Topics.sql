CREATE TABLE [dbo].[forum_Topics]
(
	[Id]                         INT             NOT NULL IDENTITY(1,1),
	[IsDeleted]                  BIT             NOT NULL DEFAULT 0,
    [HeadId]                     INT			 NULL,
	[FolderId]                   INT			 NOT NULL,
	[Header]                     NVARCHAR (MAX)  NOT NULL,
	[Subtitle]                   NVARCHAR (MAX)  NOT NULL,
	[Created]                    DATETIME2 (7)   NOT NULL,
    [Modified]                   DATETIME2 (7)   NOT NULL,
    [CreatedBy]                  NVARCHAR (100)  NOT NULL,
    [ModifiedBy]                 NVARCHAR (100)  NOT NULL,
	CONSTRAINT [PK_dbo.forum_Topics] PRIMARY KEY CLUSTERED ([Id])
)
