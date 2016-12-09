var functions = (function () {

    function addToCart() {

        $('.basket img').addClass("clicked");
        $('.basketPopup').slideDown( "slow" );


        setTimeout(function () {
            $(".basket img").removeClass('clicked');
          
        }, 500);

        setTimeout(function () {
            $(".basket img").removeClass('clicked');
            $('.basketPopup').slideUp("slow");
        }, 5000);

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

        $('.delete-popup').fadeIn();
        setTimeout(function () {
            top.location.href = "/admin/index";
        }, 3000);




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
    function deleteSize() {
        var id = $(this).data("id");


        $.ajax({
            type: "POST",
            url: "/Admin/DeleteSize",
            data: JSON.stringify({ id: id }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
        });

        $('.delete-popup').fadeIn();
        $('.' + id).hide();
        setTimeout(function () {
            $('.delete-popup').fadeOut();
        }, 3000);
    }

    function deleteProduct() {
        var id = $(this).data("id");

        $.ajax({
            type: "POST",
            url: "/Admin/DeleteProduct",
            data: JSON.stringify({ id: id }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
        });

        $('.delete-popup').fadeIn();
        $('.' + id).hide();
        setTimeout(function () {
            $('.delete-popup').fadeOut();
        }, 3000);


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

    function saveCheckoutInfo() {
        var fullName = $("#name-field").val();
        var address = $("#address-field").val();
        var zip = $("#zip-field").val();
        var city = $("#city-field").val();
        var country = $("#country-field").val();
        var email = $("#email-field").val();
        var phone = $("#phone-field").val();
        var comment = $("#comment-field").val();


        $.ajax({
            type: "POST",
            url: "/Shop/SaveCheckoutInfo",
            data: JSON.stringify({ fullName: fullName, address: address, zip: zip, city: city, country: country, email: email, phone: phone, comment: comment }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
        });

    }


    function inputNumber() {
        var id = $(this).attr("id");
        var number = $("#" + id).val();

        number = Math.abs(number);
        $("#" + id).val(number);

        if ($("#" + id).val() <= 0) {
            $("#" + id).val(1)
        }
    }

    function printReceipt() {
        w = window.open();
        jQuery.get('/css/style.css', function (data) {
            w.document.write('<html><head><title>Order confirmation</title><style>');
            w.document.write(data);
            w.document.write('</style></head><body>');
            w.document.write($('#order-receipt').html());
            w.document.write('</body></html>');
            w.print();
            w.close();
        });
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
        deleteProduct: deleteProduct,
        saveCheckoutInfo: saveCheckoutInfo,
        inputNumber: inputNumber,
        printReceipt: printReceipt,
    }
})();

(function ($) {
    $("body").on("click", "#print-receipt", functions.printReceipt);
    $("body").on("click", "#add-cart-button", functions.addToCart);
    $("body").on("click", "#create-size-button", functions.createSize);
    $("body").on("click", "#delete-size-button", functions.deleteSize);
    $("body").on("click", "#delete-product-button", functions.deleteProduct);
    $("body").on("click", "#edit-chosen-size-button", functions.editSize);
    $("body").on("click", "#add-quantity", functions.addQuantity);
    $("body").on("click", "#subtract-quantity", functions.subtractQuantity);
    $("body").on("click", ".basketRemoveItem", functions.removeProductFromSession);
    $("body").on("focusout", ".basketQuantityCount", functions.updateBasketQuantity);
    $("body").on("focusout", ".checkout-field", functions.saveCheckoutInfo);
    $("body").on("focusout", ".zip", functions.saveCheckoutInfo);
    $("body").on("focusout", ".city", functions.saveCheckoutInfo);
    $("body").on("focusout", ':input[type="number"]', functions.inputNumber);


})(jQuery);