CREATE PROCEDURE [dbo].[sp_Security_UserStore_CreateAsync]
	 @Id INT
	,@FirstName NVARCHAR(100)
    ,@LastName NVARCHAR(100)
    ,@Email NVARCHAR(100)
    ,@PasswordHash NVARCHAR(MAX)
    ,@UserName NVARCHAR(100)
    ,@DateCreated DATETIME
AS
	INSERT INTO   [dbo].[identity_Users]  
        ([FirstName]
        ,[LastName]
        ,[Email]
        ,[PasswordHash]
        ,[UserName]
        ,[DateCreated])
    VALUES
        (@FirstName
        ,@LastName
        ,@Email
        ,@PasswordHash
        ,@UserName
        ,@DateCreated);

	DECLARE @userId INT;
    SELECT @userId = @@IDENTITY;

	INSERT INTO [dbo].[forum_UserProfiles]
	([Id], IsBlocked, IsDeleted, Created, CreatedBy, Modified, ModifiedBy)
	VALUES
	(@userId, 0, 0, @DateCreated, '101ed', @DateCreated, '101ed')

	SELECT @userId
RETURN 0
