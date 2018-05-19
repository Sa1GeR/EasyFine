CREATE PROCEDURE [dbo].[sp_Forum_ForumAppRepository_EditForum]
	@forumId			int,
	@name				NVARCHAR(MAX),

	@Modified			DATETIME,
	@ModifiedBy			NVARCHAR(50)	
AS
	DECLARE @success BIT = 1

	UPDATE [dbo].[forum_Folders]
	SET  [Name] = @name
		,[Modified] = @Modified
		,[ModifiedBy] = @ModifiedBy
	OUTPUT	@success
	WHERE Id = @forumId
RETURN 0
