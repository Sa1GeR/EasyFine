CREATE PROCEDURE [dbo].[sp_Forum_MessageAppRepository_DeleteMessage]
	@messageId			INT,
	
	@Modified			DATETIME,
	@ModifiedBy			NVARCHAR(50)
AS
	DECLARE @success BIT = 1

	UPDATE [dbo].[forum_Messages]
	SET [IsDeleted] = 1
		  ,[Modified] = @Modified
		  ,[ModifiedBy] = @ModifiedBy
	OUTPUT	@success
	WHERE Id = @messageId
RETURN 0
