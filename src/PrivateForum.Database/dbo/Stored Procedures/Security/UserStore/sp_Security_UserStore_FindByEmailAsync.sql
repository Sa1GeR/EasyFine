CREATE PROCEDURE [dbo].[sp_Security_UserStore_FindByEmailAsync]
	@email NVARCHAR(MAX)
AS
	DECLARE @userId INT;

	SELECT @userId = u.Id FROM  [dbo].[identity_Users]  u WHERE u.Email = @email;
	
	SELECT * FROM [dbo].[identity_Users] WHERE Id = @userId
    SELECT r.Name FROM [dbo].[identity_Roles] r INNER JOIN [dbo].[identity_UserRoles] ur ON ur.RoleId = r.Id AND ur.UserId = @userId;
RETURN 0
