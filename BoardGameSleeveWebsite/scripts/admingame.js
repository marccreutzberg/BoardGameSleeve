var GameAdmin = (function () {
    function GameAdmin() {
        var _this = this;
        var allSizes = JSON.parse($("#gameAdmin").attr("data-json_allSizes"));
        console.log(allSizes);
        this.editForm = new GameAdmin_EditForm(allSizes);
        $(".editGameLink").on("click", function (eObj) {
            var $element = $(eObj.target);
            var $tr = $element.parent().parent();
            var gameId = parseInt($tr.children("td").eq(0).attr("data-id"));
            var gameName = $tr.children("td").eq(0).text();
            var textSizes = $tr.children("td").eq(1).text();
            var textSizesArray = textSizes.split(",");
            for (var i = 0; i < textSizesArray.length; i++)
                textSizesArray[i] = textSizesArray[i].trim();
            _this.editForm.showAndEdit(gameId, gameName, textSizesArray);
        });
    }
    return GameAdmin;
}());
var GameAdmin_EditForm = (function () {
    function GameAdmin_EditForm(allSizeNames) {
        var _this = this;
        this.$element = $("#editForm");
        this.$headlineName = $("#editForm_headlineName");
        this.$nameInput = $("#editForm_nameInput");
        this.$table = $("#editForm_table");
        this.$selectToAdd = $("#editForm_selectToAdd");
        this.$addButton = $("#editForm_addButton");
        this.$acceptButton = $("#editForm_acceptButton");
        this.$cancelButton = $("#editForm_cancelButton");
        this.allSizeNames = allSizeNames;
        this.$addButton.on("click", function () { _this.onclick_addSize(); });
    }
    GameAdmin_EditForm.prototype.showAndEdit = function (gameId, gameName, sizes) {
        this.$element.show();
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
    GameAdmin_EditForm.prototype.onclick_addSize = function () {
        var sizeSelected = this.$selectToAdd.val();
        if (sizeSelected == null)
            return;
        this.$selectToAdd.children("[value='" + sizeSelected + "']").remove();
        this.addSizeOnTable(sizeSelected);
    };
    GameAdmin_EditForm.prototype.addSizeOnTable = function (sizeName) {
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
    GameAdmin_EditForm.prototype.addOption = function (sizeName) {
        this.$selectToAdd.append($("<option value='" + sizeName + "'>" + sizeName + "</option>"));
    };
    return GameAdmin_EditForm;
}());
var gameAdmin;
$(document).ready(function () {
    gameAdmin = new GameAdmin();
});
//# sourceMappingURL=admingame.js.map