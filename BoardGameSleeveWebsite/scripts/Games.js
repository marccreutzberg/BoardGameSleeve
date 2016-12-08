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
