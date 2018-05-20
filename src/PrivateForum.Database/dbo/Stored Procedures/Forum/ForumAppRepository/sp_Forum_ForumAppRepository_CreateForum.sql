CREATE PROCEDURE [dbo].[sp_Forum_ForumAppRepository_CreateForum]
	@name				NVARCHAR(MAX),
	@parentId           INT,
	@userId             INT,
	@Created			DATETIME,
	@Modified			DATETIME,
	@CreatedBy			NVARCHAR(50),
	@ModifiedBy			NVARCHAR(50)	
AS
	INSERT INTO [dbo].[forum_Folders]
           ([ParentId]
           ,[Name]
           ,[Created]
           ,[Modified]
           ,[CreatedBy]
           ,[ModifiedBy])
     VALUES
           (@parentId
           ,@name
           ,@Created
           ,@Modified
           ,@CreatedBy
           ,@ModifiedBy)

	SELECT CAST(1 AS BIT)
RETURN 0
