CREATE PROCEDURE [dbo].[spUserLoginCreate]
	 @LOGINPROVIDER		NVARCHAR(128)
	,@PROVIDERKEY		NVARCHAR(128)
	,@USERID			UNIQUEIDENTIFIER
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
		INSERT INTO 
			[dbo].[IdentityLogin]
			(
				[LoginProvider]
			   ,[ProviderKey]
			   ,[UserId]
			)
		 VALUES
		 (
			@LOGINPROVIDER
			,@PROVIDERKEY
			,@USERID
		 ) 
	 END TRY

	 BEGIN CATCH
		DECLARE @ErrorMessage NVARCHAR(4000)
        DECLARE @ErrorSeverity INT
        DECLARE @ErrorState INT
 
        SELECT  @ErrorMessage = ERROR_MESSAGE(),
                @ErrorSeverity = ERROR_SEVERITY(),
                @ErrorState = ERROR_STATE()
 
        ROLLBACK
 
        RAISERROR ( @ErrorMessage, @ErrorSeverity, @ErrorState)
    END CATCH
END