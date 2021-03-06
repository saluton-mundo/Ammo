﻿CREATE PROCEDURE [dbo].[spBulletAddUpdate]
	@BULLETID	INT
	,@NAME	VARCHAR(255)
	,@IMAGEURI VARCHAR(255)
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
		IF EXISTS(SELECT 1 FROM Bullet WHERE BulletId = @BULLETID)
			BEGIN
				UPDATE 
					Bullet
				SET 
					Deleted = 'False'
					,Name = ISNULL(@NAME, Name)
					,ImageUri = ISNULL(@IMAGEURI, ImageUri)
					,LastActionDate = GETDATE()
					,LastActionUserId = @SESSIONUSERID
				WHERE
					BulletId = @BULLETID
			END
		ELSE 
			BEGIN
				INSERT INTO Bullet
				(
					Name
					,ImageUri
					,Deleted
					,CreateDate
					,LastActionDate
					,CreateUserId
					,LastActionUserId
				)
				VALUES
				(
					@NAME
					,@IMAGEURI
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



