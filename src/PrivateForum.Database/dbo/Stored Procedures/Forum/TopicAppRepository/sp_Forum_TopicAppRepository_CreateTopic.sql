CREATE PROCEDURE [dbo].[sp_Forum_TopicAppRepository_CreateTopic]
	@headId				INT,
	@header				NVARCHAR(MAX),
	@subtitle			NVARCHAR(MAX),
	@folderId			INT,
	
	@Created			DATETIME,
	@Modified			DATETIME,
	@CreatedBy			NVARCHAR(50),
	@ModifiedBy			NVARCHAR(50)
AS
	INSERT INTO [dbo].[forum_Topics]
           ([HeadId]
           ,[Header]
           ,[Subtitle]
           ,[Created]
           ,[Modified]
           ,[CreatedBy]
           ,[ModifiedBy]
           ,[FolderId])
	OUTPUT Inserted.Id
     VALUES
           (@headId
           ,@header
           ,@subtitle
           ,@Created
           ,@Modified
           ,@CreatedBy
           ,@ModifiedBy
           ,@folderId)

RETURN 0
