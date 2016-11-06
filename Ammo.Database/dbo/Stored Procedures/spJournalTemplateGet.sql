CREATE PROCEDURE [dbo].[spJournalTemplateGet]
	@JOURNALTEMPLATEID		INT
	,@PREMIUMFLAG			BIT
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
			jt.*
		FROM 
			JournalTemplate AS jt
		WHERE 
			jt.TemplateId = ISNULL(@JOURNALTEMPLATEID, jt.TemplateId)
			AND jt.IsPremium = ISNULL(@PREMIUMFLAG, jt.IsPremium)
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
