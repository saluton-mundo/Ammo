﻿CREATE PROCEDURE [dbo].[spActivityLogEntryDelete]
	@ACTIVITYLOGENTRYID		INT
	,@SESSIONUSERID			UNIQUEIDENTIFIER
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
		UPDATE 
			ActivityLogEntry 
		SET 
			Deleted = 'True'
			,LastActionDate = GETDATE()
			,LastActionUserId = @SESSIONUSERID
		WHERE 
			ActivityLogEntryId = @ACTIVITYLOGENTRYID
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
