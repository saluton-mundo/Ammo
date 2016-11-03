CREATE PROCEDURE [dbo].[spUserRoleCreate]
	 @USERID		UNIQUEIDENTIFIER
	,@ROLENAME		VARCHAR(255)
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
		BEGIN TRANSACTION

			-- get role id
			DECLARE @ROLEID uniqueidentifier = (SELECT RoleId FROM IdentityRole WHERE Name=@ROLENAME)

			-- insert, if not exist
			INSERT INTO 
				IdentityUserRole (UserId,RoleId)
					SELECT 
						IU.UserId,
						@ROLEID
					FROM
						IdentityUser IU
					LEFT JOIN 
						IdentityUserRole IUR 
						ON IU.UserId = IUR.UserId AND IUR.RoleId = @ROLEID
					WHERE
						IU.UserId = @USERID
						AND IUR.UserId IS NULL

		COMMIT TRANSACTION
	END TRY

	BEGIN CATCH

		IF @@ERROR<>0 AND @@TRANCOUNT > 0
			ROLLBACK TRANSACTION

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
