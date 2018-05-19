CREATE PROCEDURE [dbo].[sp_Forum_ForumAppRepository_GetForumById]
	@forumId			INT,
	@userId             INT
AS
    DECLARE @courseId INT;
    SELECT @courseId = U.CourseId FROM dbo.identity_Users U WHERE u.id = @userId;

	SELECT F.*
	FROM [dbo].[forum_Folders] F
	WHERE Id = @forumId AND [IsDeleted] != 1 AND CourseId = @courseId

	SELECT F.*
	FROM [dbo].[forum_Folders] F
	WHERE ParentId = @forumId AND [IsDeleted] != 1 AND CourseId = @courseId

	SELECT T.*
	FROM [dbo].[forum_Topics] T
	WHERE FolderId = @forumId AND [IsDeleted] != 1
RETURN 0
