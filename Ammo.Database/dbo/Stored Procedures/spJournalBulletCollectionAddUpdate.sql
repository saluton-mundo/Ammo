CREATE PROCEDURE [dbo].[spJournalBulletCollectionAddUpdate]
	@JOURNALID				INT
	,@BULLETCOLLECTIONID	INT
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
		IF EXISTS(SELECT 1 FROM JournalBulletCollection WHERE BulletCollectionId = @BULLETCOLLECTIONID AND JournalId = @JOURNALID) 
			BEGIN
				UPDATE 
					JournalBulletCollection
				SET 
					Deleted = 'False'
					,LastActionDate = GETDATE()
					,LastActionUserId = @SESSIONUSERID
				WHERE 
					JournalId = @JOURNALID
					AND BulletCollectionId = @BULLETCOLLECTIONID
			END
		ELSE
			BEGIN 
				INSERT INTO
					JournalBulletCollection
				(
					JournalId
					,BulletCollectionId
					,Deleted
					,CreateDate
					,LastActionDate
					,CreateUserId
					,LastActionUserId
				)
				VALUES
				(
					@JOURNALID
					,@BULLETCOLLECTIONID
					,'False'
					,GETDATE()
					,GETDATE()
					,@SESSIONUSERID
					,@SESSIONUSERID
				)
			END
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