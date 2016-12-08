var GameAdmin = (function () {
    function GameAdmin() {
        var _this = this;
        var allSizes = JSON.parse($("#gameAdmin").attr("data-json_allSizes"));
        this.$tableBlocker = $("#tableBlocker");
        this.$addNewGameButton = $("#addNewGameButton");
        this.editForm = new GameAdmin_EditAddForm(this, allSizes);
        $(".editGameLink").on("click", function (eObj) {
            var $element = $(eObj.target);
            var $tr = $element.parent().parent();
            var gameId = parseInt($tr.children("td").eq(0).attr("data-id"));
            var gameName = $tr.children("td").eq(0).text();
            var textSizes = $tr.children("td").eq(1).text();
            var textSizesArray = textSizes.split(",");
            for (var i = 0; i < textSizesArray.length; i++)
                textSizesArray[i] = textSizesArray[i].trim();
            _this.editForm.showAndEdit(gameId, gameName, textSizesArray[0] == "" ? [] : textSizesArray);
        });
        this.$addNewGameButton.on("click", function () { _this.editForm.showAndAdd(); });
    }
    return GameAdmin;
}());
var GameAdmin_EditAddForm = (function () {
    function GameAdmin_EditAddForm(gameAdmin, allSizeNames) {
        var _this = this;
        this.gameAdmin = gameAdmin;
        this.$element = $("#editAddForm");
        this.$headlineName = $("#editAddForm_headlineName");
        this.$nameInput = $("#editAddForm_nameInput");
        this.$table = $("#editAddForm_table");
        this.$selectToAdd = $("#editAddForm_selectToAdd");
        this.$addButton = $("#editAddForm_addButton");
        this.$acceptButton = $("#editAddForm_acceptButton");
        this.$cancelButton = $("#editAddForm_cancelButton");
        this.allSizeNames = allSizeNames;
        this.$addButton.on("click", function () { _this.onclick_addSize(); });
        this.$cancelButton.on("click", function () { _this.onclick_cancel(); });
        this.$acceptButton.on("click", function () { _this.onclick_accept(); });
    }
    GameAdmin_EditAddForm.prototype.showAndEdit = function (gameId, gameName, sizes) {
        this.isEditMode = true;
        this.$element.show();
        this.gameAdmin.$tableBlocker.css("display", "block");
        this.curEdit_gameId = gameId;
        this.$headlineName.text(gameName);
        this.$nameInput.val(gameName);
        for (var i = 0; i < sizes.length; i++)
            this.addSizeOnTable(sizes[i]);
        var optionSizes = [];
        for (var i = 0; i < this.allSizeNames.length; i++)
            if ($.inArray(this.allSizeNames[i], sizes) == -1)
                optionSizes.push(this.allSizeNames[i]);
        for (var i = 0; i < optionSizes.length; i++)
            this.addOption(optionSizes[i]);
    };
    GameAdmin_EditAddForm.prototype.showAndAdd = function () {
        this.isEditMode = false;
        this.$element.show();
        this.gameAdmin.$tableBlocker.css("display", "block");
        for (var i = 0; i < this.allSizeNames.length; i++)
            this.addOption(this.allSizeNames[i]);
    };
    GameAdmin_EditAddForm.prototype.onclick_addSize = function () {
        var sizeSelected = this.$selectToAdd.val();
        if (sizeSelected == null)
            return;
        this.$selectToAdd.children("[value='" + sizeSelected + "']").remove();
        this.addSizeOnTable(sizeSelected);
    };
    GameAdmin_EditAddForm.prototype.addSizeOnTable = function (sizeName) {
        var _this = this;
        var $tr = $("<tr><td>" + sizeName + "</td><td><a href='#' onclick='return false'>Remove</a></td></tr>");
        this.$table.children("tbody").append($tr);
        //On remove click
        $tr.find("a").on("click", function (eObj) {
            var $aElement = $(eObj.target);
            var $tr2 = $aElement.parent().parent();
            var removingSizeName = $tr2.children().eq(0).text();
            _this.addOption(removingSizeName);
            $tr2.remove();
        });
    };
    GameAdmin_EditAddForm.prototype.addOption = function (sizeName) {
        this.$selectToAdd.append($("<option value='" + sizeName + "'>" + sizeName + "</option>"));
    };
    GameAdmin_EditAddForm.prototype.onclick_accept = function () {
        var newGameName = this.$nameInput.val();
        if (newGameName == "")
            return;
        var sizes = [];
        var $trSizes = this.$table.children().children();
        for (var i = 0; i < $trSizes.length; ++i)
            sizes.push($trSizes.eq(i).children().eq(0).text());
        //console.log("gameId: " + gameId);
        //console.log("newGameName: " + newGameName);
        //console.log("sizes: ");
        //console.log(sizes);
        var href;
        if (this.isEditMode == true) {
            var gameId = this.curEdit_gameId;
            href = "/admin/editgame?gameid=" + gameId + "&newname=" + newGameName;
        }
        else
            href = "/admin/creategame?name=" + newGameName;
        for (var i = 0; i < sizes.length; i++)
            href += "&sizeNames=" + sizes[i];
        window.location.href = href;
    };
    GameAdmin_EditAddForm.prototype.onclick_cancel = function () {
        this.$element.css("display", "none");
        this.gameAdmin.$tableBlocker.css("display", "none");
        this.$nameInput.text("");
        this.$selectToAdd.empty();
        this.$table.children().empty();
    };
    return GameAdmin_EditAddForm;
}());
var gameAdmin;
$(document).ready(function () {
    gameAdmin = new GameAdmin();
});
//# sourceMappingURL=admingame.js.map