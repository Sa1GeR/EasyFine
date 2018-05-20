CREATE PROCEDURE [dbo].[sp_Forum_UsersAppRepository_GetUserProfile]
	@userId INT
AS
	SELECT UP.*, U.FirstName, U.LastName, U.Email
	FROM [dbo].[forum_UserProfiles] UP
	INNER JOIN [dbo].[identity_Users] U ON UP.Id = U.Id
	WHERE UP.Id = @userId
RETURN 0
