class GameAdmin
{
    $tableBlocker: JQuery;
    editForm: GameAdmin_EditAddForm;
	$addNewGameButton: JQuery;

    constructor()
    {
        var allSizes: string[] = JSON.parse($("#gameAdmin").attr("data-json_allSizes"));
        this.$tableBlocker = $("#tableBlocker");
		this.$addNewGameButton = $("#addNewGameButton");
        this.editForm = new GameAdmin_EditAddForm(this, allSizes);


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
            this.editForm.showAndEdit(gameId, gameName, textSizesArray[0] == "" ? [] : textSizesArray);
        });

		this.$addNewGameButton.on("click", () => { this.editForm.showAndAdd(); });
    }
}

class GameAdmin_EditAddForm
{
    gameAdmin: GameAdmin;

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

	isEditMode: boolean;

    constructor(gameAdmin: GameAdmin, allSizeNames: string[])
    {
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

        this.$addButton.on("click", () => { this.onclick_addSize(); });
        this.$cancelButton.on("click", () => { this.onclick_cancel(); });
        this.$acceptButton.on("click", () => { this.onclick_accept(); });
    }

    showAndEdit(gameId: number, gameName: string, sizes: string[]): void
    {
		this.isEditMode = true;
        this.$element.show();
        this.gameAdmin.$tableBlocker.css("display", "block");
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
    showAndAdd(): void
    {
		this.isEditMode = false;
		this.$element.show();
		this.gameAdmin.$tableBlocker.css("display", "block");
		for (var i = 0; i < this.allSizeNames.length; i++)
			this.addOption(this.allSizeNames[i]);
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

    onclick_accept()
    {
        var newGameName: string = this.$nameInput.val();

		if (newGameName == "")
			return;

        var sizes: string[] = [];
        var $trSizes: JQuery = this.$table.children().children();
        for (var i = 0; i < $trSizes.length; ++i)
            sizes.push($trSizes.eq(i).children().eq(0).text());

        //console.log("gameId: " + gameId);
        //console.log("newGameName: " + newGameName);
        //console.log("sizes: ");
        //console.log(sizes);
		
		var href;
		if (this.isEditMode == true)
		{
			var gameId: number = this.curEdit_gameId;
			href = "/admin/editgame?gameid=" + gameId + "&newname=" + newGameName;
		}
		else
			href = "/admin/creategame?name=" + newGameName;

        for (var i = 0; i < sizes.length; i++)
            href += "&sizeNames=" + sizes[i];

        window.location.href = href;
    }
    onclick_cancel()
    {
        this.$element.css("display", "none");
        this.gameAdmin.$tableBlocker.css("display", "none");
		this.$nameInput.text("");
        this.$selectToAdd.empty();
        this.$table.children().empty();
    }
}

var gameAdmin: GameAdmin
$(document).ready(() =>
{
    gameAdmin = new GameAdmin();
})