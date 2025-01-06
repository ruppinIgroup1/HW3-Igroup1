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
-- Description:	<SP_LoginUser>
-- =============================================
CREATE OR ALTER PROCEDURE SP_LoginUser
    @Email NVARCHAR(20),
    @Password NVARCHAR(20)
AS
BEGIN
    
    SELECT *
    FROM UsersTable
    WHERE Email = @Email AND Password = @Password;

END




