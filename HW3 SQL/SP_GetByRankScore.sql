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
-- Description:	<SP_GetByRankScore>
-- =============================================
CREATE OR ALTER PROCEDURE SP_GetByRankScore 
    @UserID nvarchar(20),
    @RankScore int
    
AS
BEGIN
    SELECT *
    FROM GamesTable G 
    INNER JOIN GamesUserTable U ON G.AppID = U.AppID
    WHERE G.ScoreRank > @RankScore
    AND U.UserID = @UserID  
END
GO
