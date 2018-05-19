CREATE PROCEDURE [dbo].[sp_Forum_TopicAppRepository_GetTopicById]
	@topicId			INT
AS
	SELECT T.*
	FROM [dbo].[forum_Topics] T
	WHERE Id = @topicId AND [IsDeleted] != 1

	SELECT M.*, UP.AvatarUrl,U.FirstName,U.LastName
	FROM [dbo].[forum_Messages] M
	INNER JOIN [dbo].[forum_UserProfiles] UP ON UP.Id = M.AuthorId
	INNER JOIN [dbo].[identity_Users] U ON U.Id = M.AuthorId
	WHERE M.TopicId = @topicId AND M.[IsDeleted] != 1
RETURN 0
