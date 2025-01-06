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
-- Description:	<SP_MyGames>
-- =============================================
CREATE OR ALTER PROCEDURE SP_MyGames (@UserID int)
AS
BEGIN
	select G.*
	from GamesTable G inner join GamesUserTable GU on G.AppID = GU.AppID 
	INNER JOIN UsersTable U on GU.UserID = U.id
	where @UserID = UserID
END
GO
