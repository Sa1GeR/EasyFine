CREATE PROCEDURE [dbo].[sp_Forum_ForumAppRepository_DeleteForum]
	@forumId			int,

	@Modified			DATETIME,
	@ModifiedBy			NVARCHAR(50)	
AS
	DECLARE @success BIT = 1

	UPDATE [dbo].[forum_Folders]
	SET [IsDeleted] = 1
		,[Modified] = @Modified
		,[ModifiedBy] = @ModifiedBy
	OUTPUT	@success
	WHERE Id = @forumId
RETURN 0
