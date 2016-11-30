var functions = (function () {

    function addToCart() {
        alert("Tilføjet til indkøbskurv");
    }

    return {
        addToCart: addToCart,
    }
})();



(function ($) {
    $("body").on("click", "#add-cart-button", functions.addToCart);

})(jQuery);