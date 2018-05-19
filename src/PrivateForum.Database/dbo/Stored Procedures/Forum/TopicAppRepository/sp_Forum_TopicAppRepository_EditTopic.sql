CREATE PROCEDURE [dbo].[sp_Forum_TopicAppRepository_EditTopic]
	@topicId			INT,
	@header				NVARCHAR(MAX),
	@subtitle			NVARCHAR(MAX),

	@Modified			DATETIME,
	@ModifiedBy			NVARCHAR(50)
AS
	DECLARE @success BIT = 1

	UPDATE [dbo].[forum_Topics]
	SET [Header] = @header
		  ,[Subtitle] = @subtitle
		  ,[Modified] = @Modified
		  ,[ModifiedBy] = @ModifiedBy
	OUTPUT	@success
	WHERE Id = @topicId
RETURN 0
