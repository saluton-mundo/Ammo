﻿CREATE PROCEDURE [dbo].[spBulletCollectionBulletAddUpdate]
    @BULLETCOLLECTIONID INT
    ,@BULLETID INT
    ,@SESSIONUSERID UNIQUEIDENTIFIER
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
		IF EXISTS(SELECT 1 FROM BulletCollectionBullet WHERE BulletCollectionId = @BULLETCOLLECTIONID AND BulletId = @BULLETID) 
			BEGIN
				UPDATE 
					BulletCollectionBullet
				SET 
					Deleted = 'False'
					,LastActionDate = GETDATE()
					,LastActionUserId = @SESSIONUSERID
				WHERE 
					BulletCollectionId = @BULLETCOLLECTIONID
					AND BulletId = @BULLETID
			END
		ELSE
			BEGIN 
				INSERT INTO BulletCollectionBullet
				(
					BulletCollectionId
					,BulletId
					,Deleted
					,CreateDate
					,LastActionDate
					,CreateUserId
					,LastActionUserId
				)
				VALUES
				(
					@BULLETCOLLECTIONID
					,@BULLETID
					,'False'
					,GETDATE()
					,GETDATE()
					,@SESSIONUSERID
					,@SESSIONUSERID
				)
			END
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