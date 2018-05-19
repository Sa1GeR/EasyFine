CREATE TABLE [dbo].[forum_Messages]
(
	[Id]                         INT             NOT NULL IDENTITY(1,1),
	[IsDeleted]                  BIT             NOT NULL DEFAULT 0,
    [ReplyId]                    INT			 NULL,
    [TopicId]                    INT			 NOT NULL,
	[AuthorId]                   INT             NOT NULL,
	[Content]                    NVARCHAR (MAX)  NOT NULL,
	[Created]                    DATETIME2 (7)   NOT NULL,
    [Modified]                   DATETIME2 (7)   NOT NULL,
    [CreatedBy]                  NVARCHAR (100)  NOT NULL,
    [ModifiedBy]                 NVARCHAR (100)  NOT NULL,
	CONSTRAINT [PK_dbo.forum_Messages] PRIMARY KEY CLUSTERED ([Id])
)
