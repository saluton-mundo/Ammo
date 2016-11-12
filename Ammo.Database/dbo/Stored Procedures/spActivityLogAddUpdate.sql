CREATE PROCEDURE [dbo].[spActivityLogAddUpdate]
	@ACTIVITYLOGID			INT
	,@JOURNALID				INT
	,@OWNERID				UNIQUEIDENTIFIER
	,@ACTIVITYLOGMONTH		DATETIME
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
		IF EXISTS(SELECT 1 FROM ActivityLog WHERE ActivityLogId = @ACTIVITYLOGID) 
			BEGIN 
				UPDATE 
					ActivityLog
				SET 
					 Deleted = 'False'
					,LogMonth = ISNULL(@ACTIVITYLOGMONTH, LogMonth)
					,LastActionDate = GETDATE()
					,LastActionUserId = @SESSIONUSERID
				WHERE 
					ActivityLogId = @ACTIVITYLOGID
			END
		ELSE 
			BEGIN 
				INSERT INTO ActivityLog
				(
					JournalId
					,OwnerId
					,LogMonth 
					,Deleted
					,CreateDate
					,LastActionDate
					,CreateUserId
					,LastActionUserId
				)
				VALUES 
				(
					@JOURNALID
					,@OWNERID
					,@ACTIVITYLOGMONTH
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