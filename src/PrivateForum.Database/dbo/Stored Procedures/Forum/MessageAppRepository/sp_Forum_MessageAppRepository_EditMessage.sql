CREATE PROCEDURE [dbo].[sp_Forum_MessageAppRepository_EditMessage]
	@messageId			INT,
	@content			NVARCHAR(MAX),
	
	@Modified			DATETIME,
	@ModifiedBy			NVARCHAR(50)
AS
	DECLARE @success BIT = 1

	UPDATE [dbo].[forum_Messages]
	SET [Content] = @content
		  ,[Modified] = @Modified
		  ,[ModifiedBy] = @ModifiedBy
	OUTPUT	@success
	WHERE Id = @messageId
RETURN 0
