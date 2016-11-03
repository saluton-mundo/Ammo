CREATE PROCEDURE [dbo].[spRoleGet]
	@USERID		UNIQUEIDENTIFIER
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
			IR.Name
		FROM
			IdentityRole IR
		INNER JOIN 
			IdentityUserRole IUR 
			ON IR.RoleId = IUR.RoleId 
			AND IUR.UserId = @USERID
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
