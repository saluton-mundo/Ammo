CREATE PROCEDURE [dbo].[spUserGetByName]
	@USERNAME VARCHAR(255)
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
		SELECT 
			IU.*,	-- user object
			IP.*	-- profile object
		FROM 
			IdentityUser IU
		INNER JOIN 
			IdentityProfile IP 
			ON IU.UserId = IP.UserId 
		WHERE 
			IU.UserName = @USERNAME
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