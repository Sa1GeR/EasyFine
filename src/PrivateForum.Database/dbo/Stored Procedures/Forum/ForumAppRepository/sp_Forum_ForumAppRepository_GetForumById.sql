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
RETURN 0
