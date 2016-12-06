var Games;
(function (Games) {
    var SortBox = (function () {
        function SortBox($element) {
            var _this = this;
            this.$element = $element;
            var $tHeads = this.$element.find("thead th");
            this.$findByGameElement = $tHeads.eq(0);
            this.$findBySizeElement = $tHeads.eq(1);
            this.$tbody = this.$element.find("tbody");
            this.$findByGameElement.on("click", function (eObj) { _this.onClick_findByGame(); });
            this.$findBySizeElement.on("click", function (eObj) { _this.onClick_findBySize(); });
            //this.initGamesAndSizeElements();
        }
        SortBox.prototype.initGamesAndSizeElements = function () {
            var gameTableJson = this.$element.attr("game-table-json");
            this.sizes = [];
            this.games = [];
            var gameTableData = JSON.parse(gameTableJson);
            for (var i = 0; i < gameTableData.Sizes.length; i++) {
                var size = new Size(gameTableData.Sizes[i]);
                this.sizes.push(size);
                this.sizes[size.Name] = size;
            }
            for (var i = 0; i < gameTableData.Games.length; i++) {
                var game = new Game(gameTableData.Games[i].Name);
                this.games.push(game);
                this.games[game.Name] = game;
                for (var j = 0; j < gameTableData.Games[i].Sizes.length; j++) {
                    var textSize = gameTableData.Games[i].Sizes[j];
                    var objSize = this.sizes[textSize];
                    game.Sizes.push(objSize);
                    objSize.Games.push(game);
                }
            }
            console.log(this.sizes);
            console.log(this.games);
        };
        SortBox.prototype.onClick_findByGame = function () {
        };
        SortBox.prototype.onClick_findBySize = function () {
        };
        SortBox.prototype.isCurrentSortOnGame = function () {
            if (this.$findByGameElement.hasClass("column-active") == true)
                return true;
            return false;
        };
        return SortBox;
    }());
    Games.SortBox = SortBox;
    var Game = (function () {
        function Game(name) {
            this.Name = name;
            this.Sizes = [];
        }
        return Game;
    }());
    var Size = (function () {
        function Size(name) {
            this.Name = name;
            this.Games = [];
        }
        return Size;
    }());
})(Games || (Games = {}));
var sortBox;
$(document).ready(function () {
    //sortBox = new Games.SortBox($("table.GamesTable"));
});
function sortGame() {
    var $tHeads = $(".GamesTable th");
    $tHeads.eq(0).addClass("column-active");
    $tHeads.eq(1).removeClass("column-active");
    $(".GamesTable tbody").eq(1).css("display", "none");
    $(".GamesTable tbody").eq(0).css("display", "table-row-group");
}
function sortSize() {
    var $tHeads = $(".GamesTable th");
    $tHeads.eq(0).removeClass("column-active");
    $tHeads.eq(1).addClass("column-active");
    $(".GamesTable tbody").eq(0).css("display", "none");
    $(".GamesTable tbody").eq(1).css("display", "table-row-group");
}
//# sourceMappingURL=Games.js.map