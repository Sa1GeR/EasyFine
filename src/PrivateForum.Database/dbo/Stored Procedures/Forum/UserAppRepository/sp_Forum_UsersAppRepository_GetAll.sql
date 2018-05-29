CREATE PROCEDURE [dbo].[sp_Forum_UsersAppRepository_GetAll]
AS
	SELECT p.[Id], u.[FirstName], u.[LastName], u.[Email], p.[Address] 
	FROM [dbo].[forum_UserProfiles] p
	INNER JOIN [dbo].[identity_Users] u ON p.[Id] = u.[Id]
RETURN 0
