var functions = (function () {


    function addToCart() {

        $('.basket img').addClass("clicked");
        $('.basketPopup').slideDown("slow");


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

        var error = 0;
        var widthVal = $("#width-Size").val();
        var heightVal = $("#height-Size").val();
        var nameVal = $("#name-Size").val();
        var descriptionVal = $("#description-Size").val();

        if (nameVal === "") {
            $("#size-error").text("Write a Size name");
            error++;
        }

        if (error === 0) {
            $.ajax({
                type: "POST",
                url: "/Admin/CreateSize",
                data: JSON.stringify({ width: widthVal, height: heightVal, name: nameVal, description: descriptionVal }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
            });

            $('.delete-popup h3').text('Size has been created');
            $('.delete-popup').fadeIn();
            $("#size-error").text("");

            setTimeout(function () {
                top.location.href = "/admin/Size";
            }, 3000);
        }

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

        setTimeout(function () {
            $("#progress").css("display", "block");

            $(".shopBasket").css("opacity", "0.2");
        }, 1000);

        setTimeout(function () {
            location.reload();
            $(".shopBasket").css("opacity", "1");
        }, 2000);

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

        setTimeout(function () {
            $("#progress").css("display", "block");
            $(".shopBasket").css("opacity", "0.2");
        }, 2000);

        setTimeout(function () {
            location.reload();
            $(".shopBasket").css("opacity", "1");
        }, 4500);

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
        })

        setTimeout(function () {
            $("#progress").css("display", "block");

            $(".shopBasket").css("opacity", "0.2");
        }, 2000);

        setTimeout(function () {
            location.reload();
            $(".shopBasket").css("opacity", "1");
        }, 4500);



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

        setTimeout(function () {
            $("#progress").css("display", "block");

            $(".shopBasket").css("opacity", "0.2");
        }, 1000);

        setTimeout(function () {
            location.reload();
            $(".shopBasket").css("opacity", "1");
        }, 2000);

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

        $('.delete-popup h3').text('The chosen size has been deleted');

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
        $('.delete-popup h3').text('The product has been deleted');
        $('.' + id).hide();
        setTimeout(function () {
            $('.delete-popup').fadeOut();
            $('.delete-popup h3').text();
            location.reload();
        }, 3000);


    }

    function editSize() {
        var error = 0;
        var widthVal = $("#width-Size").val();
        var heightVal = $("#height-Size").val();
        var nameVal = $("#name-Size").val();
        var descriptionVal = $("#description-Size").val();
        var idVal = $("#id-Size").val();


        if (nameVal === "") {
            $('#edit-size-error').text('Write a Size name');
            error++;
        }

        if (error === 0) {
            $('#edit-size-error').text('');
            $.ajax({
                type: "POST",
                url: "/Admin/EditChosenSize",
                data: JSON.stringify({ width: widthVal, height: heightVal, name: nameVal, description: descriptionVal, id: idVal }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
            });


            $('.delete-popup').fadeIn();
            $('.delete-popup h3').text('The Size has been edit');

            setTimeout(function () {
                top.location.href = "/admin/size";
            }, 3000);



        }
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

    function searchGames() {
        var searchInput = this.value;
        var searchToUpperCase = searchInput.toUpperCase();
        $("#game-dropdown").css("display", "block");
        $("#game-dropdown").children("option").hide();
        var gameSizeDropdown = 0;

        if (searchToUpperCase != "") {
            $("#game-dropdown option").each(function (i) {
                var games = "";
                var optionValue = this.value;
                var text = this.text;

                if (optionValue != 0 && optionValue != "empty") {
                    var json = $(this).attr("data-games");
                    var games = JSON.parse(json);
                    var showOption = new Boolean(false);

                    if (games != "") {
                        $.each(games, function (key, value) {
                            if (value.toUpperCase().indexOf(searchToUpperCase) >= 0) {
                                showOption = true;
                            }
                        })

                    }
                }

                if (showOption === true) {
                    $("#game-dropdown").children("option[value^=" + optionValue + "]").show();
                    gameSizeDropdown += 1;
                }

                else {
                    $("#game-dropdown").children("option[value^=" + optionValue + "]").hide();
                }
            })

            if (gameSizeDropdown === 0) {
                $("#game-dropdown").children("option[value^=" + 0 + "]").show();
                $("#game-dropdown").children("option[value^=" + "empty" + "]").hide();
                gameSizeDropdown += 2;
            }

            if (gameSizeDropdown === 1) {
                $("#game-dropdown").children("option[value^=" + "empty" + "]").show();
                gameSizeDropdown += 1;
            }

            $("#game-dropdown").attr("size", gameSizeDropdown);
        }
    }

    function hideSearchResults() {
        var id = $(this).attr("data-id");

        if (id === "size") {

            $("#size-dropdown").css("display", "none");
            $("#size-dropdown").children().hide();

            $("#searchBoardGame").val("");
        }

        else {
            $("#game-dropdown").css("display", "none");
            $("#game-dropdown").children().hide();
            $("#searchWidth").val("");
            $("#searchHeight").val("");
        }

    }

    function gameSearchToProduct() {
        var productId = $(this).val();

        window.location.href = "/Product/SingleProduct/" + productId;
    }

    function sizeSearchToProduct() {
        var sizeId = $(this).val();

        window.location.href = "/Product/Size/" + sizeId;
    }

    function showHideSearchInputs() {
        var id = $(this).attr("data-id");

        if (id === "game") {
            $(".search-game").css("display", "block");
            $("#game-browse").removeClass("highlight-browse");
            $("#size-browse").addClass("highlight-browse");
            $(".search-size").css("display", "none");
            $("#size-dropdown").css("display", "none");
        }
        else {
            $(".search-size").css("display", "block");
            $(".search-game").css("display", "none");
            $("#size-browse").removeClass("highlight-browse");
            $("#game-browse").addClass("highlight-browse");
            $("#game-dropdown").css("display", "none");
        }

    }

    function searchSizes() {
        var widthInput = parseInt($("#searchWidth").val());
        var heightInput = parseInt($("#searchHeight").val());
        var size = 0;


        $("#size-dropdown").css("display", "block");
        $("#size-dropdown").children("option").hide();

        $("#size-dropdown option").each(function () {
            var value = this.value;

            if (value != 0 && value != "empty") {
                var sizeHeight = parseInt($(this).attr("data-height"));
                var sizeWidth = parseInt($(this).attr("data-width"));

                if (sizeHeight === heightInput || sizeWidth === widthInput) {
                    $("#size-dropdown").children("option[value^=" + $(this).val() + "]").show();
                    size += 1;
                }

                else {
                    $("#size-dropdown").children("option[value^=" + $(this).val() + "]").hide();
                }
            }
        })


        if (size === 0) {
            $("#size-dropdown").children("option[value^=" + 0 + "]").show();
            $("#size-dropdown").children("option[value^=" + "empty" + "]").hide();
            size += 1;
        }

        if (size != 0) {
            size += 1;
            $("#size-dropdown").children("option[value^=" + "empty" + "]").show();
        }


        $("#size-dropdown").attr('size', size);
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
        searchGames: searchGames,
        hideSearchResults: hideSearchResults,
        showHideSearchInputs: showHideSearchInputs,
        gameSearchToProduct: gameSearchToProduct,
        sizeSearchToProduct: sizeSearchToProduct,
        searchSizes: searchSizes,
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
    $("body").on("click", ".browse", functions.showHideSearchInputs);
    $("body").on("change", "#game-dropdown", functions.gameSearchToProduct);
    $("body").on("change", "#size-dropdown", functions.sizeSearchToProduct);

    $("body").on("focusout", ".basketQuantityCount", functions.updateBasketQuantity);
    $("body").on("focusout", ".checkout-field", functions.saveCheckoutInfo);
    $("body").on("focusout", ".zip", functions.saveCheckoutInfo);
    $("body").on("focusout", ".city", functions.saveCheckoutInfo);
    $("body").on("focusout", ':input[type="number"]', functions.inputNumber);
    $("body").on("focusout", "#game-dropdown", functions.hideSearchResults);
    $("body").on("focusout", "#size-dropdown", functions.hideSearchResults);

    $("body").on("input", "#searchBoardGame", functions.searchGames);
    $("body").on("input", "#searchWidth", functions.searchSizes);
    $("body").on("input", "#searchHeight", functions.searchSizes);




})(jQuery);