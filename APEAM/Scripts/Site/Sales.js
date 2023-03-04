$(document).ready(function () {
    $("#addProductButton").on("click", function () {
        $(".checked-product:checked").each(function (i) {

            var rowId = $("tr"+`[data-id='${$(this).data("id")}']`);            

            if (!$(rowId).length) {

                var row = "<tr data-id='" + $(this).data("id") + "' class='articleRow'>" +
                    "<td>" + $(this).data("name") + "</td>" +
                    "<td class='priceSelector'>" + $(this).data("price") + "</td>" +
                    "<td class='quantitySelector'><input type='number' name='ItemLists[" + i + "].Quantity' data-id='" + i + "' class='form-control quantityText' value='1'/></td>" +
                    "<td class='totalPriceSelector' data-id='" + i + "'>" + $(this).data("price") + "</td>" +
                    "<td><p><a href='javascript:void(0)' class='deleteRow'>Eliminar</a></p></td>" +
                    "<input type='hidden' name='ItemLists[" + i + "].ProductId' value='" + $(this).data("id") + "'/>" +
                    "<input type='hidden' name='ItemLists[" + i + "].Name' value='" + $(this).data("name") + "'/>" +
                    "<input type='hidden' name='ItemLists[" + i + "].Price' value='" + $(this).data("price") + "'/>" +
                    "<input type='hidden' name='ItemLists[" + i + "].Description' value='" + $(this).data("description") + "'/>" +
                    "<input type='hidden' name='ItemLists[" + i + "].SaleId' value='" + $("#ID") + "'/>" +
                    "</tr> ";
                   

                $("#articlesBody").append(row);

                var suma = 0.0;
                $(".totalPriceSelector").each(function () {
                    var articleTotal = Number($(this).text());

                    suma += articleTotal;
                });

                $("#pricing").text(suma);
            }
            
        });    
    });

    $(document).on("click", ".deleteRow", function () {        
        $(this).closest("tr").remove();

        FixIndex();
    });

    $(document).on("change", ".quantityText", function () {        
        var price = $(this).closest("tr").find(".priceSelector").text();        
        $(this).closest("tr").find(".totalPriceSelector").text(Number(price) * $(this).val());

        var suma = 0.0;
        $(".totalPriceSelector").each(function () {
            var articleTotal = Number($(this).text());

            suma += articleTotal;
        });

        $("#pricing").text(suma);
    });


    function FixIndex() {
        $(".articleRow").each(function (index) {
            $(this).find("input").each(function () {
                var parts = this.name.split(/\d+/);
                this.name = parts[0] + index + parts[1];               
            });            
        });
    }
});