CREATE PROCEDURE [dbo].[sp_Security_UserStore_DeleteAsync]
	@userId INT
AS
	DELETE FROM [dbo].[identity_UserRoles] WHERE UserId = @userId; 
	DELETE FROM  [dbo].[identity_Users]  WHERE Id = @userId;
RETURN 0
