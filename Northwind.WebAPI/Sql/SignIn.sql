ALTER PROCEDURE [dbo].[SignIn]
    @pUserName NVARCHAR(254),
    @pUserPassword NVARCHAR(50),
    @responseMessage NVARCHAR(250)='' OUTPUT
AS
BEGIN

    SET NOCOUNT ON

    DECLARE @userID INT

    IF EXISTS (SELECT TOP 1 user_id FROM [dbo].[Users] WHERE User_Name=@pUserName)
    BEGIN
        SET @userID=(SELECT user_id FROM [dbo].[Users] WHERE User_Name=@pUserName AND user_password=HASHBYTES('SHA2_512', @pUserPassword+CAST(user_salt AS NVARCHAR(36))))

       IF(@userID IS NULL)
           SET @responseMessage='Incorrect password'
       ELSE 
           SET @responseMessage='User successfully logged in'
    END
    ELSE
       SET @responseMessage='Invalid login'

END