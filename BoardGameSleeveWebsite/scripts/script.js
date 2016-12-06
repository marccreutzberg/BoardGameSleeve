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

    return {
        addToCart: addToCart,
    }
})();



(function ($) {
    $("body").on("click", "#add-cart-button", functions.addToCart);

})(jQuery);