CREATE PROCEDURE [dbo].[sp_Security_UserStore_RemoveFromRoleAsync]
	@roleName NVARCHAR(100),
	@userId INT
AS
	DELETE FROM [dbo].[identity_UserRoles] WHERE UserId = @userId AND RoleId in (SELECT r.Id FROM [dbo].[identity_Roles] r WHERE r.Name = @roleName)
RETURN 0
