CREATE PROCEDURE [dbo].[sp_Forum_ForumAppRepository_GetRootForum]
	@userId             INT
AS
	DECLARE @courseId INT;
	DECLARE @lookupForumId INT

	SELECT @lookupForumId = F.Id
	FROM [dbo].[forum_Folders] F
	WHERE ParentId IS NULL AND [IsDeleted] != 1

	SELECT * FROM [dbo].[forum_Folders] F WHERE F.Id = @lookupForumId

	SELECT F.*
	FROM [dbo].[forum_Folders] F
	WHERE ParentId = @lookupForumId AND [IsDeleted] != 1  AND CourseId = @courseId

	SELECT T.*
	FROM [dbo].[forum_Topics] T
	WHERE FolderId = @lookupForumId AND [IsDeleted] != 1
RETURN 0
