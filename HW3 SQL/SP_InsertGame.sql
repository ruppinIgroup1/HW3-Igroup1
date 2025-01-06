CREATE OR ALTER PROCEDURE SP_InsertGame (
    @AppID int,
    @Name nvarchar(255),
    @IsMac bit,
    @Price decimal(10, 2),
    @Description nvarchar(MAX),
    @IsWindows bit,
    @Website nvarchar(MAX),
    @HeaderImage nvarchar(MAX),
    @IsLinux bit,
    @ScoreRank int,
    @Recommendations nvarchar(MAX),
    @Publisher nvarchar(MAX),
    @ReleaseDate date
)
AS
BEGIN
    INSERT INTO GamesTable(
	AppID,
	Name,
        IsMac,
        Price,
        Description,
        IsWindows,
        Website,
        HeaderImage,
        IsLinux,
        ScoreRank,
        Recommendations,
        Publisher,
        ReleaseDate
    )
    VALUES (
	@AppID,
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
GO

CREATE TABLE [GamesUserTable] (
[UserID] smallint NOT NULL,
[AppID] int NOT NULL,
PRIMARY KEY (UserID, AppID),
FOREIGN KEY (UserID) references UsersTable(id),
FOREIGN KEY (AppID) references GamesTable(AppID),
);

