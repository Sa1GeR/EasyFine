CREATE PROCEDURE [dbo].[sp_Forum_TopicAppRepository_UpdateTopicHeadId]
	@topicId			INT,
	@headId				INT
AS
	DECLARE @success BIT = 1

	UPDATE [dbo].[forum_Topics]
	SET [HeadId] = @headId
	OUTPUT	@success
	WHERE Id = @topicId
RETURN 0
