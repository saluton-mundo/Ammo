CREATE PROCEDURE [dbo].[spJournalBulletCollectionGet]
	@JOURNALID		INT
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
			@JOURNALID AS JournalId
			,bc.*
			,b.*
		FROM
			JournalBulletCollection AS jbc
		INNER JOIN 
			BulletCollection AS bc
			ON jbc.BulletCollectionId = bc.BulletCollectionId
			AND bc.Deleted = 'False'
		INNER JOIN 
			BulletCollectionBullet AS bcb
			ON bc.BulletCollectionId = bcb.BulletCollectionId
			AND bcb.Deleted = 'False'
		INNER JOIN 
			Bullet AS b
			ON bcb.BulletId = b.BulletId
			AND b.Deleted = 'False'
		WHERE 
			jbc.JournalId = @JOURNALID
			AND jbc.Deleted = 'False'
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
