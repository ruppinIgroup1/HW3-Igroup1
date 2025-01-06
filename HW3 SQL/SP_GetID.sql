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
-- Create date: <01.01>
-- Description:	<SP_GetID>
-- =============================================
CREATE OR ALTER PROCEDURE SP_GetID
    @Email NVARCHAR(20),
    @Password NVARCHAR(20)
AS
BEGIN
	SELECT U.id
	from UsersTable U
	where U.email = @Email and U.password = @Password
END
GO
