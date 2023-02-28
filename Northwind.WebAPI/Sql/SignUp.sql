ALTER PROCEDURE [dbo].[SignUp]
    @pUserName NVARCHAR(50), 
    @pUserPassword NVARCHAR(50),
    @responseMessage NVARCHAR(250) OUTPUT
AS
BEGIN
    SET NOCOUNT ON

    DECLARE @salt UNIQUEIDENTIFIER=NEWID()
    BEGIN TRY

        INSERT INTO dbo.[Users] (user_name, user_password, user_salt)
        VALUES(@pUserName, HASHBYTES('SHA2_512', @pUserPassword+CAST(@salt AS NVARCHAR(36))), @salt)

       SET @responseMessage='Success'

    END TRY
    BEGIN CATCH
        SET @responseMessage=ERROR_MESSAGE() 
    END CATCH

END