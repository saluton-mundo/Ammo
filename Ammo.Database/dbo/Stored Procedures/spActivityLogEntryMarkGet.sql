CREATE PROCEDURE [dbo].[spActivityLogEntryMarkGet]
	@ACTIVITYLOGID				INT
	,@ACTIVITYLOGENTRYMARKID	INT
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
			alem.ActivityLogEntryMarkId
			,alem.ActivityLogId
			,alem.[Description]
			,alem.Color
		FROM 
			ActivityLogEntryMark AS alem
		WHERE 
			alem.ActivityLogId = ISNULL(@ACTIVITYLOGID, alem.ActivityLogId)
			AND alem.ActivityLogEntryMarkId = ISNULL(@ACTIVITYLOGENTRYMARKID, alem.ActivityLogEntryMarkId)
			AND alem.Deleted = 'False'
	END TRY
	BEGIN CATCH
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