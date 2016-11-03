CREATE PROCEDURE [dbo].[spUserDelete]
	@USERID		UNIQUEIDENTIFIER
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
 
    DECLARE @localTran bit
    IF @@TRANCOUNT = 0
    BEGIN
        SET @localTran = 1
        BEGIN TRANSACTION LocalTran
    END

	BEGIN TRY 
		-- Mark the user as deleted
		UPDATE 
			[dbo].IdentityUser 
		SET	
			Deleted = 'True'
		WHERE 
			UserId = @USERID
		-- Mark the user's profile as deleted
		UPDATE 
			[dbo].IdentityProfile
		SET 
			Deleted = 'True'
		WHERE
			UserId = @USERID

	    IF @localTran = 1 AND XACT_STATE() = 1
            COMMIT TRANSACTION
    END TRY
    
	BEGIN CATCH
 
        DECLARE @ErrorMessage NVARCHAR(4000)
        DECLARE @ErrorSeverity INT
        DECLARE @ErrorState INT
 
        SELECT  @ErrorMessage = ERROR_MESSAGE(),
                @ErrorSeverity = ERROR_SEVERITY(),
                @ErrorState = ERROR_STATE()
 
        IF @localTran = 1 AND XACT_STATE() <> 0
            ROLLBACK TRAN
 
        RAISERROR ( @ErrorMessage, @ErrorSeverity, @ErrorState)
    END CATCH
END;
