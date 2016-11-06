CREATE PROCEDURE [dbo].[spJournalIndexGet]
	@JOURNALID	INT
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
		-- --------------------------------------------------------------------
		-- FIRST SELECT CONTAINS THE HEADER 
		-- --------------------------------------------------------------------
		SELECT 
			j.JournalId
			,j.OwnerId
		FROM 
			Journal AS j
		WHERE 
			j.JournalId = @JOURNALID
			AND j.Deleted = 'False';

		-- --------------------------------------------------------------------
		-- SECOND SELECT CONTAINS THE BULLET COLLECTION
		-- --------------------------------------------------------------------
		SELECT 
			bc.BulletCollectionId
			,bc.Name
		FROM 
			Journal AS j
 		INNER JOIN 
			JournalBulletCollection AS jbc
			ON 	j.JournalId = jbc.JournalId
			AND jbc.Deleted = 'False'
		INNER JOIN 
			BulletCollection AS bc
			ON ISNULL(jbc.BulletCollectionId, 2) = bc.BulletCollectionId
			AND bc.Deleted = 'False'
		WHERE 
			j.JournalId = @JOURNALID
			AND j.Deleted = 'False';

		-- ---------------------------------------------------------------------
		-- THIRD SELECT CONTAINS THE TAGS
		-- ---------------------------------------------------------------------
		SELECT 
			jt.*
		FROM 
			JournalTag AS jt
		WHERE 
			jt.JournalId = @JOURNALID
			AND jt.Deleted = 'False'
		ORDER BY 
			jt.TagContent ASC;

		-- ---------------------------------------------------------------------
		-- FINAL SELECT CONTAINS THE BOOKMARKS
		-- ---------------------------------------------------------------------
		SELECT 
			jb.*
		FROM 
			JournalBookmark AS jb
		WHERE 
			jb.JournalId = @JOURNALID
		ORDER BY
			jb.PageId ASC;
	END TRY 
	BEGIN CATCH
 
        DECLARE @ErrorMessage NVARCHAR(4000)
        DECLARE @ErrorSeverity INT
        DECLARE @ErrorState INT
 
        SELECT  @ErrorMessage = ERROR_MESSAGE(),
                @ErrorSeverity = ERROR_SEVERITY(),
                @ErrorState = ERROR_STATE()

        ROLLBACK
 
        RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState)
    END CATCH
END
