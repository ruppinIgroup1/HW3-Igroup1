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
-- Create date: <27-12>
-- Description:	<SP_UserDeleteGame>
-- =============================================
CREATE OR ALTER PROCEDURE SP_UserDeleteGame(
 @UserID int,
 @AppID int)
AS
BEGIN
	
	DELETE FROM GamesUserTable Where @UserID= UserID and @AppID = AppID

	UPDATE GamesTable
    SET numberOfPurchases = numberOfPurchases - 1
    WHERE AppID = @AppID;

	UPDATE GamesTable
    SET numberOfPurchases = 0
    WHERE AppID = @AppID AND numberOfPurchases < 0;

END 
GO

EXEC SP_UserDeleteGame 4,5;

