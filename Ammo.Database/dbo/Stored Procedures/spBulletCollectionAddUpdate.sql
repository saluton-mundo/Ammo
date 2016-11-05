CREATE PROCEDURE [dbo].[spBulletCollectionAddUpdate]
	@BULLETCOLLECTIONID		INT
	,@NAME					VARCHAR(255)
	,@ISAMMODEFAULT			BIT
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
		IF EXISTS(SELECT 1 FROM BulletCollection WHERE BulletCollectionId = @BULLETCOLLECTIONID)
			BEGIN
				INSERT INTO BulletCollection
				( 
					Name
					,IsAmmoDefault
					,Deleted
					,CreateDate
					,LastActionDate
					,CreateUserId
					,LastActionUserId
				) 
				VALUES
				(
					@NAME
					,@ISAMMODEFAULT
					,'False'
					,GETDATE()
					,GETDATE()
					,@SESSIONUSERID
					,@SESSIONUSERID
				)
			END
		ELSE 
			BEGIN 
				UPDATE 
					BulletCollection 
				SET 
					Name = ISNULL(@NAME, Name)
					,Deleted = 'False'
					,LastActionDate = GETDATE()
					,LastActionUserId = @SESSIONUSERID
				WHERE 
					BulletCollectionId = @BULLETCOLLECTIONID;
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