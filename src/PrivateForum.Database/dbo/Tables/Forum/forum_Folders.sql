CREATE TABLE [dbo].[forum_Folders]
(
	[Id]                         INT             NOT NULL IDENTITY(1,1),
	[IsDeleted]                  BIT             NOT NULL DEFAULT 0,
	[CourseId]                   INT             NOT NULL,
    [ParentId]                   INT			 NULL,
	[Name]                       NVARCHAR (MAX)  NOT NULL,
	[Created]                    DATETIME2 (7)   NOT NULL,
    [Modified]                   DATETIME2 (7)   NOT NULL,
    [CreatedBy]                  NVARCHAR (100)  NOT NULL,
    [ModifiedBy]                 NVARCHAR (100)  NOT NULL,
	CONSTRAINT [PK_dbo.forum_Folders] PRIMARY KEY CLUSTERED ([Id])
)