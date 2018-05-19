CREATE PROCEDURE [dbo].[sp_Forum_MessageAppRepository_CreateMessage]
	@replyId			INT,
	@content			NVARCHAR(MAX),
	@topicId			INT,
	@authorId           INT,
	@Created			DATETIME,
	@Modified			DATETIME,
	@CreatedBy			NVARCHAR(50),
	@ModifiedBy			NVARCHAR(50)
AS
	INSERT INTO [dbo].[forum_Messages]
           ([ReplyId]
           ,[Content]
           ,[Created]
           ,[Modified]
           ,[CreatedBy]
           ,[ModifiedBy]
		   ,[AuthorId]
           ,[TopicId])
	 OUTPUT INSERTed.Id
     VALUES
           (@replyId
           ,@content
           ,@Created
           ,@Modified
           ,@CreatedBy
           ,@ModifiedBy
		   ,@authorId
           ,@topicId)

RETURN 0
