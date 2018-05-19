CREATE PROCEDURE [dbo].[sp_Forum_TopicAppRepository_DeleteTopic]
	@topicId			INT,

	@Modified			DATETIME,
	@ModifiedBy			NVARCHAR(50)
AS
	DECLARE @success BIT = 1

	UPDATE [dbo].[forum_Topics]
	SET [IsDeleted] = 1
		  ,[Modified] = @Modified
		  ,[ModifiedBy] = @ModifiedBy
	OUTPUT	@success
	WHERE Id = @topicId
RETURN 0
