CREATE PROCEDURE [dbo].[spActivityLogActivityGet]
	  @ACTIVITYLOGID			INT,
      @ACTIVITYLOGACTIVITYID	INT
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
			ala.*
		FROM 
			ActivityLogActivity AS ala
		WHERE 
			ala.ActivityLogId = ISNULL(@ACTIVITYLOGID, ala.ActivityLogId)
			AND ala.ActivityId = ISNULL(@ACTIVITYLOGACTIVITYID, ala.ActivityId)
			AND ala.Deleted = 'False'
	END TRY 
	BEGIN CATCH
 
        DECLARE @ErrorMessage NVARCHAR(4000)
        DECLARE @ErrorSeverity INT
        DECLARE @ErrorState INT
 
        SELECT  @ErrorMessage = ERROR_MESSAGE(),
                @ErrorSeverity = ERROR_SEVERITY(),
                @ErrorState = ERROR_STATE()
 
        RAISERROR ( @ErrorMessage, @ErrorSeverity, @ErrorState)
    END CATCH
END
