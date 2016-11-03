﻿CREATE PROCEDURE [dbo].[spJournalGet]
	 @journalId		INT
	,@UserName		VARCHAR(50)
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
			CONVERT(VARCHAR(50),J.OwnerId) AS OwnerId
			,J.CoverUrl
			,J.JournalId
			,J.LastActionDate  AS LastEdited
			,J.Title
			,J.[Description]
			,JP.* 
		FROM 
			Journal AS J
		LEFT OUTER JOIN 
			JournalPage AS JP
			ON J.JournalId = JP.JournalId
		WHERE 
			J.JournalId = ISNULL(@journalId, J.JournalId)
			AND CONVERT(VARCHAR(50), J.OwnerId) = ISNULL(@UserName,  CONVERT(VARCHAR(50), J.OwnerId))
			AND J.Deleted = 'False'
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
