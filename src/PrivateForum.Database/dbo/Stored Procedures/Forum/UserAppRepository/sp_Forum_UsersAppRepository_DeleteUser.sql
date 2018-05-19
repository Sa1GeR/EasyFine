CREATE PROCEDURE [dbo].[sp_Forum_UsersAppRepository_DeleteUser]
	@userId				INT,					
	@Modified			DATETIME,
	@ModifiedBy			NVARCHAR(50)
AS
	DECLARE @success BIT = 1

	UPDATE [dbo].[forum_UserProfiles]
	SET [IsDeleted] = 1
		,[Modified] = @Modified
		,[ModifiedBy] = @ModifiedBy
	OUTPUT	@success
	WHERE Id = @userId
RETURN 0
