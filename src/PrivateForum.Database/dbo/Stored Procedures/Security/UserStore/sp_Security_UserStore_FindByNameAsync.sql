CREATE PROCEDURE [dbo].[sp_Security_UserStore_FindByNameAsync]
	@username NVARCHAR(100)
AS
    DECLARE @userId int;
    SELECT @userId = u.Id FROM  [dbo].[identity_Users]  u WHERE u.UserName = @username;

    SELECT u.* FROM  [dbo].[identity_Users]  u WHERE u.Id = @userId;
    SELECT r.Name FROM [dbo].[identity_Roles] r INNER JOIN [dbo].[identity_UserRoles] ur ON ur.RoleId = r.Id AND ur.UserId = @userId
RETURN 0
