CREATE PROCEDURE [dbo].[sp_Security_UserStore_UpdateAsync]
	 @Id INT
	,@FirstName NVARCHAR(100)
    ,@LastName NVARCHAR(100)
    ,@Email NVARCHAR(100)
    ,@PasswordHash NVARCHAR(MAX)
    ,@UserName NVARCHAR(100)
    ,@DateCreated DATETIME
AS
	UPDATE   [dbo].[identity_Users]  
    SET [FirstName] = @FirstName
        ,[LastName] = @LastName
        ,[Email] = @Email
        ,[PasswordHash] = @PasswordHash
        ,[UserName] = @UserName
        ,[DateCreated] = @DateCreated
    WHERE Id = @Id
RETURN 0
