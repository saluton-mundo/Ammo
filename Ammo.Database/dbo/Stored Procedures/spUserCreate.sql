-- =============================================
-- Author:      John Nolan
-- Create date: 1st October 2016
-- Description: Creates a User and a scaffolds a Profile 
-- =============================================
CREATE PROCEDURE [dbo].[spUserCreate]
	 @USERID UNIQUEIDENTIFIER
	,@EMAIL NVARCHAR(255)
	,@EMAILCONFIRMED BIT
    ,@PASSWORDHASH NVARCHAR(MAX)
    ,@SECURITYSTAMP NVARCHAR(MAX)
    ,@PHONENUMBER NVARCHAR(MAX)
    ,@PHONENUMBERCONFIRMED BIT
    ,@TWOFACTORENABLED BIT
    ,@LOCKOUTENDDATEUTC DATETIME
    ,@LOCKOUTENABLED BIT
    ,@ACCESSFAILEDCOUNT INT
    ,@USERNAME NVARCHAR(256)
    ,@CREATEBY UNIQUEIDENTIFIER
    ,@MODIFYBY UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT,
        QUOTED_IDENTIFIER,
        ANSI_NULLS,
        ANSI_PADDING,
        ANSI_WARNINGS,
        ARITHABORT,
        CONCAT_NULL_YIELDS_NULL ON;
    SET NUMERIC_ROUNDABORT OFF;
 
    BEGIN TRY 
		BEGIN TRANSACTION
			-- create user
			INSERT INTO [dbo].[IdentityUser]
				([UserId]
				,[Email]
				,[EmailConfirmed]
				,[PasswordHash]
				,[SecurityStamp]
				,[PhoneNumber]
				,[PhoneNumberConfirmed]
				,[TwoFactorEnabled]
				,[LockoutEndDateUtc]
				,[LockoutEnabled]
				,[AccessFailedCount]
				,[UserName]
				,[CreateBy]
				,[ModifyBy])
			VALUES
				(@USERID
				,@EMAIL
				,@EMAILCONFIRMED
				,@PASSWORDHASH
				,@SECURITYSTAMP
				,@PHONENUMBER
				,@PHONENUMBERCONFIRMED
				,@TWOFACTORENABLED
				,@LOCKOUTENDDATEUTC
				,@LOCKOUTENABLED
				,@ACCESSFAILEDCOUNT
				,ISNULL(NULLIF(@USERNAME,''), @EMAIL)
				,@CREATEBY
				,@MODIFYBY)

			-- create the user's profile
			INSERT INTO [dbo].[IdentityProfile]
			   ([UserId]
			   ,[CreateBy]
			   ,[ModifyBy])
			VALUES
			   (@USERID
			   ,@CREATEBY
			   ,@MODIFYBY)

            COMMIT TRANSACTION
    END TRY
    BEGIN CATCH
 
        DECLARE @ErrorMessage NVARCHAR(4000)
        DECLARE @ErrorSeverity INT
        DECLARE @ErrorState INT
 
        SELECT  @ErrorMessage = ERROR_MESSAGE(),
                @ErrorSeverity = ERROR_SEVERITY(),
                @ErrorState = ERROR_STATE()
 
        ROLLBACK TRANSACTION
 
        RAISERROR ( @ErrorMessage, @ErrorSeverity, @ErrorState)
    END CATCH
END