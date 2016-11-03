CREATE PROCEDURE [dbo].[spUserUpdate]
	 @USERID UNIQUEIDENTIFIER
	,@EMAIL NVARCHAR(255)
	,@EMAILCONFIRMED BIT
	,@FIRSTNAME VARCHAR(128)
	,@LASTNAME VARCHAR(128)
	,@MIDDLENAME VARCHAR(128)
	,@MODIFYBY UNIQUEIDENTIFIER
	,@PASSWORDHASH NVARCHAR(MAX)
	,@SECURITYSTAMP NVARCHAR(MAX)
	,@PHONENUMBER NVARCHAR(MAX)
	,@PHONENUMBERCONFIRMED BIT
	,@TWOFACTORENABLED BIT
	,@LOCKOUTENDDATEUTC DATETIME
	,@LOCKOUTENABLED BIT
	,@ACCESSFAILEDCOUNT INT
	,@USERNAME VARCHAR(256)
AS
BEGIN
	BEGIN TRY
		BEGIN TRANSACTION

		UPDATE [dbo].[IdentityUser]
		   SET [Email] = ISNULL(NULLIF(@EMAIL, ''), [Email])
			  ,[EmailConfirmed] = ISNULL(@EMAILCONFIRMED, [EmailConfirmed])
			  ,[PasswordHash] = ISNULL(@PASSWORDHASH, [PasswordHash])
			  ,[SecurityStamp] = ISNULL(@SECURITYSTAMP, [SecurityStamp])
			  ,[PhoneNumber] = ISNULL(@PHONENUMBER, [PhoneNumber])
			  ,[PhoneNumberConfirmed] = ISNULL(@PHONENUMBERCONFIRMED, [PhoneNumberConfirmed])
			  ,[TwoFactorEnabled] = ISNULL(@TWOFACTORENABLED, [TwoFactorEnabled])
			  ,[LockoutEndDateUtc] = @LOCKOUTENDDATEUTC
			  ,[LockoutEnabled] = @LOCKOUTENABLED
			  ,[AccessFailedCount] = ISNULL(@ACCESSFAILEDCOUNT, 0)
			  ,[UserName] = ISNULL(@USERNAME, [UserName])
			  ,[ModifyDate] = GETUTCDATE() 
		WHERE 
			UserId = @USERID


		UPDATE [dbo].[IdentityProfile]
		   SET [FirstName] = ISNULL(@FIRSTNAME, [FirstName])
			  ,[MiddleName] = ISNULL(@MIDDLENAME, [MiddleName])
			  ,[LastName] = ISNULL(@LASTNAME, [LastName])
			  ,[ModifyDate] = GETUTCDATE()
		WHERE 
			UserId = @USERID

		IF (@MODIFYBY IS NOT NULL)
		BEGIN
			UPDATE [dbo].[IdentityUser] SET [ModifyBy] = @MODIFYBY WHERE UserId = @USERID
			UPDATE [dbo].[IdentityProfile] SET [ModifyBy] = @MODIFYBY WHERE UserId = @USERID
		END

	   COMMIT TRANSACTION
	END TRY

	BEGIN CATCH
		IF @@ERROR<>0 AND @@TRANCOUNT > 0
			ROLLBACK TRANSACTION

		DECLARE
			@ErrorMessage nvarchar(4000) = ERROR_MESSAGE(),
			@ErrorNumber int = ERROR_NUMBER(),
			@ErrorSeverity int = ERROR_SEVERITY(),
			@ErrorState int = ERROR_STATE(),
			@ErrorLine int = ERROR_LINE(),
			@ErrorProcedure nvarchar(200) = ISNULL(ERROR_PROCEDURE(), '-');
		SELECT @ErrorMessage = N'Error %d, Level %d, State %d, Procedure %s, Line %d, ' + 'Message: ' + @ErrorMessage;
		RAISERROR (@ErrorMessage, @ErrorSeverity, 1, @ErrorNumber, @ErrorSeverity, @ErrorState, @ErrorProcedure, @ErrorLine)

		--THROW --if on SQL2012 or above
	END CATCH
END