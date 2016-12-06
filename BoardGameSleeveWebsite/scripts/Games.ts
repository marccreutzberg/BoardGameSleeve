module Games
{
    export class SortBox
    {
        $element: JQuery;
        $findByGameElement: JQuery;
        $findBySizeElement: JQuery;
        $tbody: JQuery;

        sizes: Size[];
        games: Game[]; 

        constructor($element: JQuery)
        {
            this.$element = $element;
            
            var $tHeads: JQuery = this.$element.find("thead th");
            this.$findByGameElement = $tHeads.eq(0);
            this.$findBySizeElement = $tHeads.eq(1);
            this.$tbody = this.$element.find("tbody");

            this.$findByGameElement.on("click", (eObj: JQueryEventObject) => { this.onClick_findByGame(); });
            this.$findBySizeElement.on("click", (eObj: JQueryEventObject) => { this.onClick_findBySize(); });

            //this.initGamesAndSizeElements();
        }
        initGamesAndSizeElements()
        {
            var gameTableJson: string = this.$element.attr("game-table-json");
            this.sizes = [];
            this.games = [];

            var gameTableData = JSON.parse(gameTableJson);
            for (var i = 0; i < gameTableData.Sizes.length; i++)
            {
                var size: Size = new Size(gameTableData.Sizes[i]);
                this.sizes.push(size);
                this.sizes[size.Name] = size;
            }
            for (var i = 0; i < gameTableData.Games.length; i++)
            {
                var game: Game = new Game(gameTableData.Games[i].Name)
                this.games.push(game);
                this.games[game.Name] = game;

                for (var j = 0; j < gameTableData.Games[i].Sizes.length; j++)
                {
                    var textSize: string = gameTableData.Games[i].Sizes[j];
                    var objSize: Size = this.sizes[textSize];
                    game.Sizes.push(objSize);
                    objSize.Games.push(game);
                }
            }

            console.log(this.sizes);
            console.log(this.games);
        }

        onClick_findByGame()
        {

        }
        onClick_findBySize()
        {
        }
        isCurrentSortOnGame(): boolean
        {
            if (this.$findByGameElement.hasClass("column-active") == true)
                return true;
            return false;
        }
    }

    class Game
    {
        Name: string;
        Sizes: Size[];
        constructor(name: string)
        {
            this.Name = name;
            this.Sizes = [];
        }
    }
    class Size
    {
        Name: string;
        Games: Game[];
        constructor(name: string)
        {
            this.Name = name;
            this.Games = [];
        }
    }
}
var sortBox: Games.SortBox;
$(document).ready(() =>
{
    //sortBox = new Games.SortBox($("table.GamesTable"));
    
});

function sortGame()
{
    var $tHeads: JQuery = $(".GamesTable th");
    $tHeads.eq(0).addClass("column-active");
    $tHeads.eq(1).removeClass("column-active");
    $(".GamesTable tbody").eq(1).css("display", "none");
    $(".GamesTable tbody").eq(0).css("display", "table-row-group");
}
function sortSize()
{
    var $tHeads: JQuery = $(".GamesTable th");
    $tHeads.eq(0).removeClass("column-active");
    $tHeads.eq(1).addClass("column-active");
    $(".GamesTable tbody").eq(0).css("display", "none");
    $(".GamesTable tbody").eq(1).css("display", "table-row-group");
}