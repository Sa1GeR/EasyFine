CREATE PROCEDURE [dbo].[sp_Forum_ForumAppRepository_GetForumById]
	@forumId			INT,
	@userId             INT
AS

	SELECT F.*
	FROM [dbo].[forum_Folders] F
	WHERE Id = @forumId AND [IsDeleted] != 1

	SELECT F.*
	FROM [dbo].[forum_Folders] F
	WHERE ParentId = @forumId AND [IsDeleted] != 1

	SELECT T.*
	FROM [dbo].[forum_Topics] T
	WHERE FolderId = @forumId AND [IsDeleted] != 1

	;WITH CTE
	AS
	(
	
    SELECT
        F.[Id],
		F.[Name],
        F.[ParentId]
    FROM [dbo].[forum_Folders] F
    WHERE f.[Id] = @forumId
	UNION ALL
    SELECT
        F.[Id],
		F.[Name],
		F.[ParentId]
    FROM [dbo].[forum_Folders] F
        INNER JOIN CTE C ON F.[Id] = C.[ParentId]
	)
	SELECT * FROM CTE
	WHERE CTE.[Id] != @forumId
RETURN 0
