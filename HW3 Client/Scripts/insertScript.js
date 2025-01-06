$(document).ready(() => {
    $('#insertBTN').click(() => {
        const GamesList = [];
        GAME.forEach((game) => {
            const serverGame = {
                "appID": game.AppID,
                "name": game.Name,
                "Release_date": new Date(game.Release_date).toISOString(),
                "price": game.Price,
                "description": game.description,
                "Header_image": game.Header_image,
                "website": game.Website,
                "windows": game.Windows,
                "mac": game.Mac,
                "linux": game.Linux,
                "Score_rank": game.Score_rank,
                "recommendations": game.Recommendations,
                "Developers": game.Developers
            };

            GamesList.push(serverGame);
        });
        console.log(GamesList);

        $.ajax({
            url: 'https://localhost:7015/api/Games/InitPostForAllGames',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(GamesList),
            success: function (response) {
                alert('All Games inserted successfully:', response);
            },
            error: function (error) {
                console.error('Error inserting games:', error);
            }
        });
    });
});
