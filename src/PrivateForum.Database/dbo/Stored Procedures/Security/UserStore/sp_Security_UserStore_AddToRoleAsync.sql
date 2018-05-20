CREATE PROCEDURE [dbo].[sp_Security_UserStore_AddToRoleAsync]
	@roleName NVARCHAR(100),
	@userId INT
AS
	INSERT INTO [dbo].[identity_UserRoles] (UserId, RoleId) SELECT @userId, r.Id FROM [dbo].[identity_Roles] r WHERE r.Name = @roleName
RETURN 0
