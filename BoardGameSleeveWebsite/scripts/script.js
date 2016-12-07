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
    }
    function updateBasketQuantity() {
        var productId = $(this).attr("data-id");
        var quantity = $(".quantity-number").val();

        $.ajax({
            type: "POST",
            url: "/Shop/UpdateBasketQuantity",
            data: JSON.stringify({ productId: productId, quantity: quantity }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
        });

    }
    function subtractQuantity() {
        var productId = $(this).attr("data-id");
        var prev = $("#quantity-number" + productId).val();
        var subtracted = prev - 1;

        $.ajax({
            type: "POST",
            url: "/Shop/SubtractToQuantityProduct",
            data: JSON.stringify({ productId: productId }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
        });

        $("#quantity-number" + productId).val(subtracted);
    }
    function addQuantity() {
        var productId = $(this).attr("data-id");
        var prev = $("#quantity-number" + productId).val();
        var added = parseInt(prev) + 1;

        $.ajax({
            type: "POST",
            url: "/Shop/AddToQuantityProduct",
            data: JSON.stringify({ productId: productId }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
        });

        $("#quantity-number" + productId).val(added);
    }
    function removeProductFromSession() {
        var productId = $(this).attr("data-id");
        
        $.ajax({
            type: "POST",
            url: "/Shop/DeleteProductFromSession",
            data: JSON.stringify({ productId: productId }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
        });

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
    function editSize()
    {
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

    function createProduct() {
        var name = $("#product-Name").val();
        var desc = $("#product-desc").val();
        var color = $("#product-Color").val();
        var price = $("#product-Price").val();
        var SleeveCountInProduct = $("#product-SleeveCountInProduct").val();
        var InStock = $("#product-InStock").val();

        var sizes = $('#product-size').val();

        console.log(name);
        console.log(desc);
        console.log(color);
        console.log(price);
        console.log(SleeveCountInProduct);
        console.log(InStock);
        console.log(sizes);

        //$.ajax({
        //    type: "POST",
        //    url: "/Admin/EditChosenSize",
        //    data: JSON.stringify({ width: widthVal, height: heightVal, name: nameVal, description: descriptionVal, id: idVal }),
        //    contentType: "application/json; charset=utf-8",
        //    dataType: "json",
        //});

        //top.location.href = "/admin/size";
    }


    return {
        addToCart: addToCart,
        createSize: createSize,
        updateBasketQuantity: updateBasketQuantity,
        addQuantity: addQuantity,
        subtractQuantity: subtractQuantity,
        removeProductFromSession: removeProductFromSession,
        editSize: editSize,
        deleteSize: deleteSize,
        createProduct: createProduct
    }
})();

(function ($) {
    $("body").on("click", "#add-cart-button", functions.addToCart);
    $("body").on("click", "#create-size-button", functions.createSize);
    $("body").on("click", "#delete-size-button", functions.deleteSize);
    $("body").on("click", "#edit-chosen-size-button", functions.editSize);
    $("body").on("click", "#createGameBtn", functions.createGame);
    $("body").on("focusout", ".basketQuantityCount", functions.updateBasketQuantity)
    $("body").on("click", "#add-quantity", functions.addQuantity);
    $("body").on("click", "#subtract-quantity", functions.subtractQuantity);
    $("body").on("click", ".basketRemoveItem", functions.removeProductFromSession);
    $("body").on("click", "#product-Create-Button", functions.createProduct);

    
})(jQuery);