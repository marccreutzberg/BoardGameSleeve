class GameAdmin
{
    editForm: GameAdmin_EditForm;
    constructor()
    {
        var allSizes: string[] = JSON.parse($("#gameAdmin").attr("data-json_allSizes"));
        console.log(allSizes);
        this.editForm = new GameAdmin_EditForm(allSizes);
        $(".editGameLink").on("click", (eObj: JQueryEventObject) =>
        {
            var $element = $(eObj.target);
            var $tr = $element.parent().parent();
            var gameId: number = parseInt($tr.children("td").eq(0).attr("data-id"));
            var gameName: string = $tr.children("td").eq(0).text();
            var textSizes: string = $tr.children("td").eq(1).text();
            var textSizesArray: string[] = textSizes.split(",");
            for (var i = 0; i < textSizesArray.length; i++)
                textSizesArray[i] = textSizesArray[i].trim();
            this.editForm.showAndEdit(gameId, gameName, textSizesArray);
        });
    }
}

class GameAdmin_EditForm
{
    $element: JQuery;
    $headlineName: JQuery;
    $nameInput: JQuery;
    $table: JQuery;
    $selectToAdd: JQuery;
    $addButton: JQuery;
    $acceptButton: JQuery;
    $cancelButton: JQuery;

    allSizeNames: string[];

    curEdit_gameId: number;

    constructor(allSizeNames: string[])
    {
        this.$element = $("#editForm");
        this.$headlineName = $("#editForm_headlineName");
        this.$nameInput = $("#editForm_nameInput");
        this.$table = $("#editForm_table");
        this.$selectToAdd = $("#editForm_selectToAdd");
        this.$addButton = $("#editForm_addButton");
        this.$acceptButton = $("#editForm_acceptButton");
        this.$cancelButton = $("#editForm_cancelButton");

        this.allSizeNames = allSizeNames;

        this.$addButton.on("click", () => { this.onclick_addSize(); });
    }

    showAndEdit(gameId: number, gameName: string, sizes: string[]): void
    {
        this.$element.show();
        this.curEdit_gameId = gameId;
        this.$headlineName.text(gameName);
        this.$nameInput.val(gameName);

        for (var i = 0; i < sizes.length; i++)
            this.addSizeOnTable(sizes[i]);

        var optionSizes: string[] = [];
        for (var i = 0; i < this.allSizeNames.length; i++)
            if ($.inArray(this.allSizeNames[i], sizes) == -1)
                optionSizes.push(this.allSizeNames[i]);

        for (var i = 0; i < optionSizes.length; i++)
            this.addOption(optionSizes[i]);
    }
    onclick_addSize()
    {
        var sizeSelected: string = this.$selectToAdd.val();
        if (sizeSelected == null)
            return;
        this.$selectToAdd.children("[value='" + sizeSelected + "']").remove();
        this.addSizeOnTable(sizeSelected);
    }

    addSizeOnTable(sizeName: string)
    {
        var $tr = $("<tr><td>" + sizeName + "</td><td><a href='#' onclick='return false'>Remove</a></td></tr>");
        this.$table.children("tbody").append($tr);

        //On remove click
        $tr.find("a").on("click", (eObj: JQueryEventObject) =>
        {
            var $aElement: JQuery = $(eObj.target);
            var $tr2: JQuery = $aElement.parent().parent();
            var removingSizeName = $tr2.children().eq(0).text();
            this.addOption(removingSizeName);
            $tr2.remove();
        });
    }
    addOption(sizeName: string)
    {
        this.$selectToAdd.append($("<option value='" + sizeName + "'>" + sizeName + "</option>"));
    }
}

var gameAdmin: GameAdmin
$(document).ready(() =>
{
    gameAdmin = new GameAdmin();
})