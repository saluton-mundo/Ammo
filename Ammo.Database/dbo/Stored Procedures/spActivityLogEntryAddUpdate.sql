CREATE PROCEDURE [dbo].[spActivityLogEntryAddUpdate]
	@ACTIVITYLOGENTRYID			INT,
    @ACTIVITYLOGID				INT,
    @ACTIVITYID					INT,
    @ACTIVITYLOGENTRYMARKID		INT,
    @ENTRYDATE					DATETIME,
    @SESSIONUSERID				UNIQUEIDENTIFIER
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
		IF EXISTS(SELECT 1 FROM ActivityLogEntry WHERE ActivityLogEntryId = @ACTIVITYLOGENTRYID)
			BEGIN
				UPDATE 
					ActivityLogEntry
				SET 
					Deleted = 'False'
					,ActivityId = ISNULL(@ACTIVITYID, ActivityId)
					,ActivityLogEntryMarkId = ISNULL(@ACTIVITYLOGENTRYMARKID, ActivityLogEntryMarkId)
					,LastActionDate = GETDATE()
					,LastActionUserId = @SESSIONUSERID
				WHERE 
					ActivityLogEntryId = @ACTIVITYLOGENTRYID
			END
		ELSE 
			BEGIN 
				INSERT INTO ActivityLogEntry
				(
					ActivityId
					,ActivityLogEntryMarkId
					,ActivityLogId
					,EntryDate
					,Deleted
					,CreateDate
					,LastActionDate
					,CreateUserId
					,LastActionUserId
				)
				VALUES 
				(
					@ACTIVITYID
					,@ACTIVITYLOGENTRYMARKID
					,@ACTIVITYLOGID
					,@ENTRYDATE
					,'False'
					,GETDATE()
					,GETDATE()
					,@SESSIONUSERID
					,@SESSIONUSERID
				)
			END
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