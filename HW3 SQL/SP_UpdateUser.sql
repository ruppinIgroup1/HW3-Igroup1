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
-- Author:		<Mor>
-- Create date: <28-12>
-- Description:	<SP_UpdateUser>
-- =============================================
CREATE OR ALTER PROCEDURE SP_UpdateUser
    @UserID INT,
    @Email NVARCHAR(100),
    @Password NVARCHAR(100),
    @Name NVARCHAR(100)
AS
BEGIN
    
    UPDATE UsersTable
    SET 
        Email = @Email,
        Password = @Password,
        Name = @Name
    WHERE id = @UserID;

  
    IF @@ROWCOUNT > 0
    BEGIN
        SELECT 'User updated successfully' AS Message;
    END
    ELSE
    BEGIN
        SELECT 'No user found with the provided UserID' AS Message;
    END
END
GO
