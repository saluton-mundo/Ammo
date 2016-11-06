CREATE PROCEDURE [dbo].[spJournalTemplateAddUpdate]
	 @JOURNALTEMPLATEID		INT
	,@NAME					VARCHAR(255)
	,@TEMPLATEURI			VARCHAR(255)
	,@ISPREMIUM				BIT
	,@PRICE					FLOAT
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
		IF EXISTS(SELECT 1 FROM JournalTemplate AS jt WHERE jt.TemplateId = @JOURNALTEMPLATEID) 
			BEGIN 
				UPDATE 
					JournalTemplate
				SET 
					Name = ISNULL(@NAME, Name)
					,TemplateUri = ISNULL(@TEMPLATEURI, TemplateUri)
					,Price = CASE WHEN @SESSIONUSERID = CreateUserId THEN ISNULL(@PRICE, Price) ELSE Price END
					,Deleted = 'False'
					,LastActionDate = GETDATE()
					,LastActionUserId = @SESSIONUSERID
				WHERE 
					TemplateId = @JOURNALTEMPLATEID
			END
		ELSE 
			BEGIN
				INSERT INTO JournalTemplate
				(
					Name
					,TemplateUri
					,IsPremium
					,Price
					,Deleted
					,CreateDate
					,LastActionDate
					,CreateUserId
					,LastActionUserId
				)
				VALUES
				(
					@NAME
					,@TEMPLATEURI
					,ISNULL(@ISPREMIUM, 'False')
					,ISNULL(@PRICE, 0)
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

