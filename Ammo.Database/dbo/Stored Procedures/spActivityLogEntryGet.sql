CREATE PROCEDURE [dbo].[spActivityLogEntryGet]
	@ACTIVITYLOGENTRYID		INT
	,@ACTIVITYLOGID			INT
	,@ACTIVITYID			INT
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
			ale.*
		FROM 
			ActivityLogEntry AS ale
		WHERE 
			ale.ActivityLogEntryId = ISNULL(@ACTIVITYLOGENTRYID, ale.ActivityLogEntryId)
			AND ale.ActivityLogId = ISNULL(@ACTIVITYLOGID, ale.ActivityLogId)
			AND ale.ActivityId = ISNULL(@ACTIVITYID, ale.ActivityId)
			AND ale.Deleted = 'False'
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
