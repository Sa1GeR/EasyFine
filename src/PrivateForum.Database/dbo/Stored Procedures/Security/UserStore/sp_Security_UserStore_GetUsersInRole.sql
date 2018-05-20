CREATE PROCEDURE [dbo].[sp_Security_UserStore_GetUsersInRole]
	@roleName NVARCHAR(MAX)
AS
	DECLARE @userIds AS TABLE ([Id] INT)

	INSERT INTO @userIds
	SELECT DISTINCT U.Id FROM [dbo].[identity_Roles] R
	INNER JOIN [dbo].[identity_UserRoles] UR ON UR.RoleId = R.Id AND R.[Name] = @roleName
	INNER JOIN [dbo].[identity_Users] U ON U.Id = UR.UserId

	SELECT * FROM [identity_Users] U
	INNER JOIN @userIds UI ON UI.Id = U.Id

    SELECT r.Name AS Item1, ur.UserId AS Item2 FROM [dbo].[identity_Roles] r 
	INNER JOIN [dbo].[identity_UserRoles] ur ON ur.RoleId = r.Id
	INNER JOIN @userIds UI ON UI.Id = ur.UserId

RETURN 0
