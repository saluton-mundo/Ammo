CREATE PROCEDURE [dbo].[spBulletCollectionBulletGet]
	 @BULLETCOLLECTIONID	INT
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
			b.*
		FROM 
			BulletCollectionBullet AS bcb
		INNER JOIN 
			Bullet AS b
			ON bcb.BulletId = b.BulletId
			AND b.Deleted = 'False'
		WHERE 
			bcb.BulletCollectionId = @BULLETCOLLECTIONID
			AND bcb.Deleted = 'False'
	END TRY 
	BEGIN CATCH
 
        DECLARE @ErrorMessage NVARCHAR(4000)
        DECLARE @ErrorSeverity INT
        DECLARE @ErrorState INT
 
        SELECT  @ErrorMessage = ERROR_MESSAGE(),
                @ErrorSeverity = ERROR_SEVERITY(),
                @ErrorState = ERROR_STATE()

        ROLLBACK
 
        RAISERROR ( @ErrorMessage, @ErrorSeverity, @ErrorState)
    END CATCH
END
