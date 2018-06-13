CREATE PROCEDURE [dbo].[sp_Security_UserStore_CreateAsync]
	 @Id INT
	,@FirstName NVARCHAR(100)
    ,@LastName NVARCHAR(100)
    ,@Email NVARCHAR(100)
    ,@PasswordHash NVARCHAR(MAX)
    ,@UserName NVARCHAR(100)
    ,@DateCreated DATETIME
	,@Address NVARCHAR(1000)
	,@Birthday DATETIME
AS
	INSERT INTO   [dbo].[identity_Users]  
        ([FirstName]
        ,[LastName]
        ,[Email]
        ,[PasswordHash]
        ,[UserName]
        ,[DateCreated]
		,[BirthDay])
    VALUES
        (@FirstName
        ,@LastName
        ,@Email
        ,@PasswordHash
        ,@UserName
        ,@DateCreated
		,@Birthday);

	DECLARE @userId INT;
    SELECT @userId = @@IDENTITY;

	INSERT INTO [dbo].[forum_UserProfiles]
	([Id], [Address], IsBlocked, IsDeleted, Created, CreatedBy, Modified, ModifiedBy)
	VALUES
	(@userId, @Address, 0, 0, @DateCreated, 'system', @DateCreated, 'system')

	SELECT @userId
RETURN 0
