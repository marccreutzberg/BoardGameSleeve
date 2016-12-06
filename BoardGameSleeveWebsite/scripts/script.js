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
        alert('CREATE SIZE JS');

        //var widthVal = $("#width-Size").val();
        //var heightVal = $("#height-Size").val();
        //var nameVal = $("#name-Size").val();
        //var descriptionVal = $("#description-Size").val();

        //$.ajax({
        //    type: "POST",
        //    url: "/Admini/CreateSize",
        //    data: JSON.stringify({ width: widthVal, height: heightVal, name: nameVal, description: descriptionVal }),
        //    contentType: "application/json; charset=utf-8",
        //    dataType: "json",
        //});
    }

    return {
        addToCart: addToCart,
        createSize: createSize,
    }
})();

(function ($) {
    $("body").on("click", "#add-cart-button", functions.addToCart);
    $("body").on("click", "#create-size-button", functions.createSize);
})(jQuery);