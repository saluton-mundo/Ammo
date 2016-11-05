CREATE PROCEDURE [dbo].[spBulletCollectionGet]
	 @BULLETCOLLECTIONID	INT
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
		SELECT 
			bc.*
			,b.*
		FROM 
			BulletCollection AS bc
		LEFT OUTER JOIN 
			BulletCollectionBullet AS bcb
			ON bc.BulletCollectionId = bcb.BulletCollectionId
			AND bcb.Deleted = 'False'
		LEFT OUTER JOIN 
			Bullet AS b
			ON bcb.BulletId = b.BulletId
			AND b.Deleted = 'False'
		WHERE 
			bc.BulletCollectionId = ISNULL(@BULLETCOLLECTIONID, bc.BulletCollectionId)
			AND bc.Deleted = 'False'
			AND 
			(
				bc.CreateUserId = ISNULL(@SESSIONUSERID, bc.CreateUserId)
				OR bc.IsAmmoDefault = 'True'
			)
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

