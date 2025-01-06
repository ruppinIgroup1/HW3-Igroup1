CREATE TABLE [GamesTable] (
    AppID int PRIMARY KEY,
    Name nvarchar(255) NOT NULL,
    IsMac bit,
    Price decimal(10, 2),
    Description nvarchar(MAX),
    IsWindows bit,
    Website nvarchar(MAX),
    HeaderImage nvarchar(MAX),
    IsLinux bit,
    ScoreRank int,
    Recommendations nvarchar(MAX),
    Publisher nvarchar(MAX),
    ReleaseDate date,
    numberOfPurchases int DEFAULT 0
);

select * from UsersTable
select * from GamesUserTable 
select * from GamesTable


EXEC SP_AddNewUser 'orelna@nana.com', 'orel123', 'orel';
EXEC SP_UserBuyGame 5,22670;
EXEC SP_UserDeleteGame 5,231330;
EXEC SP_MyGames 5;
EXEC SP_GetByRankScore 5,1;
EXEC SPÉ_Price 5,5;
EXEC SP_Myusers;
EXEC SP_LoginUser 'amit3432@gmail.com', '1231234';
EXEC SP_UpdateUser 9,'amit@gmail.comm', '123123', 'Amityogev';
EXEC SP_GetID 'amit3432@gmail.com', '1231234';


 INSERT INTO Games (
    Name,
    Mac,
    Price,
    Description,
    Windows,
    Website,
    HeaderImage,
    Linux,
    ScoreRank,
    Recommendations,
    Publisher,
    ReleaseDate
  )
  VALUES (
    @Name,
    @IsMac,
    @Price,
    @Description,
    @IsWindows,
    @Website,
    @HeaderImage,
    @IsLinux,
    @ScoreRank,
    @Recommendations,
    @Publisher,
    @ReleaseDate
  );
END


