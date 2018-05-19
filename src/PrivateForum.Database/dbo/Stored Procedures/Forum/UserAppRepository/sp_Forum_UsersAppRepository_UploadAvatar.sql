CREATE PROCEDURE [dbo].[sp_Forum_UsersAppRepository_UploadAvatar]
	@userId			INT,
	@avatarUrl		NVARCHAR(MAX),

	@Modified			DATETIME,
	@ModifiedBy			NVARCHAR(50)
AS
	DECLARE @success BIT = 1

	UPDATE [dbo].[forum_UserProfiles]
	SET [AvatarURL] = @avatarUrl
		  ,[Modified] = @Modified
		  ,[ModifiedBy] = @ModifiedBy
	OUTPUT	@success
	WHERE Id = @userId
RETURN 0
