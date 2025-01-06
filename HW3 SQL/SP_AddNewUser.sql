-- ================================================
-- Template generated from Template Explorer using:
-- Create Procedure (New Menu).SQL
--
-- Use the Specify Values for Template Parameters 
-- command (Ctrl-Shift-M) to fill in the parameter 
-- values below.
--
-- This block of comments will not be included in
-- the definition of the procedure.
-- ================================================
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<MOR>
-- Create date: <28-12>
-- Description:	<SP_AddNewUser>
-- =============================================
ALTER PROCEDURE SP_AddNewUser
    (@Email VARCHAR(20), @Password VARCHAR(20), @Name VARCHAR(20))
AS
BEGIN
    IF EXISTS (
        SELECT 1
        FROM UsersTable
        WHERE email = @Email 
    )
    BEGIN
        PRINT 'There is already a user with this Email, Try Again!';
    END
    ELSE
    BEGIN
        INSERT INTO UsersTable (email, password, Name)
        VALUES (@Email, @Password, @Name);

        
        DECLARE @UserId INT;
        SELECT @UserId = id FROM UsersTable WHERE email = @Email;


        SELECT @UserId AS id, @Name AS name, @Email AS email, @Password AS pasword
		END
END
GO
