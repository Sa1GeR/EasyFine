CREATE PROCEDURE [dbo].[sp_Forum_ForumAppRepository_CreateForum]
	@name				NVARCHAR(MAX),
	@parentId           INT,
	@userId             INT,
	@Created			DATETIME,
	@Modified			DATETIME,
	@CreatedBy			NVARCHAR(50),
	@ModifiedBy			NVARCHAR(50)	
AS
    DECLARE @courseId INT;
    SELECT @courseId = U.CourseId FROM dbo.identity_users U WHERE u.id = @userId;
	
	INSERT INTO [dbo].[forum_Folders]
           ([ParentId]
           ,[Name]
		   ,[CourseId]
           ,[Created]
           ,[Modified]
           ,[CreatedBy]
           ,[ModifiedBy])
     VALUES
           (@parentId
           ,@name
		   ,@courseId
           ,@Created
           ,@Modified
           ,@CreatedBy
           ,@ModifiedBy)

	SELECT CAST(1 AS BIT)
RETURN 0
