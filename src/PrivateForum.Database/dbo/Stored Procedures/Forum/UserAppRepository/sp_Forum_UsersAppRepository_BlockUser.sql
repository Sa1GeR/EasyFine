CREATE PROCEDURE [dbo].[sp_Forum_UsersAppRepository_BlockUser]
	@userId				INT,					
	@Modified			DATETIME,
	@ModifiedBy			NVARCHAR(50)
AS
	DECLARE @success BIT = 1

	UPDATE [dbo].[forum_UserProfiles]
	SET [IsBlocked] = 1
		,[Modified] = @Modified
		,[ModifiedBy] = @ModifiedBy
	OUTPUT	@success
	WHERE Id = @userId
RETURN 0
