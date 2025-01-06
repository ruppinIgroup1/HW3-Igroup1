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
-- Description:	<SP_UserBuyGame>
-- =============================================
CREATE OR ALTER PROCEDURE SP_UserBuyGame(
 @UserID smallint,
 @AppID int)
AS
BEGIN


 IF EXISTS (
        SELECT 1
        FROM GamesUserTable GU 
        WHERE UserID = @UserID AND AppID = @AppID
    )
    BEGIN
        PRINT 'The user already owns this game!!!.';
        RETURN;
    END

INSERT INTO GamesUserTable  (
UserID, AppID)
VALUES (
@UserID, @AppID);

UPDATE GamesTable
    SET numberOfPurchases = numberOfPurchases + 1
    WHERE AppID = @AppID;
END 
GO

EXEC SP_UserBuyGame 4,5;



  
