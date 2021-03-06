﻿CREATE PROCEDURE [dbo].[spJournalAddUpdate]
     @JournalId		INT
	,@OwnerId		UNIQUEIDENTIFIER
	,@Title			VARCHAR(75)
	,@Description	VARCHAR(1024)
	,@CoverUrl		VARCHAR(255)
	,@UserId		UNIQUEIDENTIFIER
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
		IF EXISTS(SELECT 1 FROM Journal WHERE JournalId = @JournalId) 
			BEGIN
				UPDATE Journal
				SET	Title = @Title
					,[Description] = ISNULL(@Description, [Description])
					,CoverUrl = ISNULL(@CoverUrl, CoverUrl)
					,Deleted = 'False'
					,LastActionDate = GETDATE()
					,LastActionUserId = @UserId
				WHERE JournalId = @JournalId
			END
		ELSE
			BEGIN 
				INSERT INTO 
					dbo.Journal
				(
					OwnerId, 
					Title, 
					[Description], 
					CoverUrl,
					Deleted,
					CreateDate,
					LastActionDate,
					LastActionUserId
				)
				VALUES 
				(
					@OwnerId,
					@Title,
					@Description,
					@CoverUrl,
					'False',
					GETDATE(),
					GETDATE(),
					@UserId
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