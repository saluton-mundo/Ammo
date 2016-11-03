CREATE PROCEDURE [dbo].[spUserLoginDelete]
	@USERID			UNIQUEIDENTIFIER
	,@LOGINPROVIDER	NVARCHAR(128)
	,@PROVIDERKEY	NVARCHAR(128)
AS
BEGIN
	BEGIN TRY
		UPDATE 
			IdentityLogin
		SET 
			Deleted = 'True'
		WHERE 
			UserId = @USERID
			AND LoginProvider = @LOGINPROVIDER
			AND ProviderKey = @PROVIDERKEY
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
