var functions = (function () {

    function addToCart() {
        var id = $("#productId").val();
        var productQuantity = $("#quantity-product").val();

        $.ajax({
            type: "POST",
            url: "/Product/AddToBasket",
            data: JSON.stringify({ productId: id, quantity: productQuantity }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",

        });
    }
    function createSize() {
        var widthVal = $("#width-Size").val();
        var heightVal = $("#height-Size").val();
        var nameVal = $("#name-Size").val();
        var descriptionVal = $("#description-Size").val();

        $.ajax({
            type: "POST",
            url: "/Admin/CreateSize",
            data: JSON.stringify({ width: widthVal, height: heightVal, name: nameVal, description: descriptionVal }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
        });

        top.location.href = "/admin/size";
    }
    function deleteSize()
	{
        var id = $(this).data("id");

        $.ajax({
            type: "POST",
            url: "/Admin/DeleteSize",
            data: JSON.stringify({ id: id }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
        });

        top.location.href = "/admin/size";
    }
    function editSize() {
        var widthVal = $("#width-Size").val();
        var heightVal = $("#height-Size").val();
        var nameVal = $("#name-Size").val();
        var descriptionVal = $("#description-Size").val();
        var idVal = $("#id-Size").val();

        $.ajax({
            type: "POST",
            url: "/Admin/EditChosenSize",
            data: JSON.stringify({ width: widthVal, height: heightVal, name: nameVal, description: descriptionVal, id: idVal }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
        });

        top.location.href = "/admin/size";
    }
    function createGame() {
		console.log("createGame()");
    }

    return {
        addToCart: addToCart,
        createSize: createSize,
        deleteSize: deleteSize,
        editSize: editSize,
        createGame: createGame
    }
})();

(function ($) {
    $("body").on("click", "#add-cart-button", functions.addToCart);
    $("body").on("click", "#create-size-button", functions.createSize);
    $("body").on("click", "#delete-size-button", functions.deleteSize);
    $("body").on("click", "#edit-chosen-size-button", functions.editSize);
    $("body").on("click", "#createGameBtn", functions.createGame);
})(jQuery);