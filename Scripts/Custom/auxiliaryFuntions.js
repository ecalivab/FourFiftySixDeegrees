
//Modal + Btn - Btn Auxiliary Funcion inside ListMenuPizza
function addBtn(price, qty)
{
    qty = qty + 1;
    $('#priceQuantity').text(price * qty);
    $("#quantityModal").text(qty);

    if (qty > 1) {
        $('#minus').prop("disabled", false);
    }
    return qty;
}

function subBtn(price, qty)
{
    if (qty != 1) {
        qty = qty - 1;
        $('#priceQuantity').text(price * qty);
        $("#quantityModal").text(qty);
 
        if (qty == 1) { //Edge condition Quanty arrive to 1 need to disable button
            $('#minus').prop("disabled", true);
        }
        return qty;
    }

    return 1; //Should Never arrive!
}

function resetModal()
{
    $('#inputQuantity').val(1);
    $("#quantityModal").text(1);
    $('#minus').prop("disabled", true);
    $("#plus").unbind("click");
    $("#minus").unbind("click");
    $("#addToOrder").unbind("click");
}

function calcTotal() {
    var sum = 0.0;
    var sumWithComa = 0.0;
    $(".pricePartial").each(function () {
        var val = $.trim($(this).text());
        if (val) {
            val = parseFloat(val.replace(/^\€/, ""));
            sum += !isNaN(val) ? val : 0;
        }
    });
    
    $("#totval").text(sum);
    sumWithComa = $("#totval").text().replace(".", ",");
    $("#totalAmount").val(sumWithComa);
    return sum;

}