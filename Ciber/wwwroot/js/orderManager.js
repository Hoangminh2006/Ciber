function sendOrder() {
    var urlsend = '/Home/InsertData';

    var productId = $("#productId").val();
    var orderName = $("#orderName").val();
    var customerId = $("#customerId").val();
    var amount = $("#amount").val();
    var orderDate = $("#orderDate").val();
    if (!orderName) {
        $('.toast').toast('show');
        $('#toastbody').css("background-color", "red").css("color", "white");
        $('#toastbody').text("OrderName required");
        return;
    }
    if (amount < 1 || !amount) {
        $('.toast').toast('show');
        $('#toastbody').css("background-color", "red").css("color", "white");
        $('#toastbody').text("Amount required and larger than 1");
        return;
    }
    urlsend += "?ProductId=" + productId + "&CustomerId=" + customerId + "&OrderDate=" + orderDate + "&Amount=" + amount + "&OrderName=" + orderName;
    $.ajax({
        url: urlsend,
        type: "post",
        headers: {
            "content-type": "text/plain;charset=UTF-8"
        },
        beforeSend: function () {
            console.log("Waiting...");
        },
        error: function () {
            console.log("Error");
        },
        success: function (data) {
            $('.toast').toast('show');
            if (data.success) {
                $("#myModal").modal("hide");
                $('#toastbody').css("background-color", "blue").css("color", "white");
                reloadData();
            }
            else {
                $('#toastbody').css("background-color", "red").css("color", "white");
            }
            $('#toastbody').text(data.message);

        }
    });
}
function view() {
    var now = new Date();
    var y = now.getFullYear();
    var m = now.getMonth() + 1;
    var d = now.getDate();

    m = m < 10 ? "0" + m : m;
    d = d < 10 ? "0" + d : d;
    document.querySelector("#orderDate").value = y + "-" + m + "-" + d;
    $("#orderName").val("");
    getCustomer();
    getProduct();
    $("#amount").val(1);
    $("#myModal").modal("show");
}
function hideModal() {
    $("#myModal").modal("hide");
}
$(document).ready(function () {
    getCustomer();
    getProduct();
    getData();
});
function getCustomer() {
    var getCustomerList = '/Home/GetCustomerList';
    $.ajax({
        type: "POST",
        url: getCustomerList,
        data: "{}",
        success: function (data) {
            var s = "";
            for (var i = 0; i < data.item.length; i++) {
                s += '<option value="' + data.item[i].id + '">' + data.item[i].name + '</option>';
            }
            $("#customerId").html(s);
        }
    });
}
function getProduct() {
    var getProductList = '/Home/GetProductList';
    $.ajax({
        type: "POST",
        url: getProductList,
        data: "{}",
        success: function (data) {
            var s = "";
            for (var i = 0; i < data.item.length; i++) {
                s += '<option value="' + data.item[i].id + '">' + data.item[i].name + '</option>';
            }
            $("#productId").html(s);
        }
    });
}
function reloadData() {
    var table = $('#myTable').DataTable();
    table.ajax.reload();
}
function getData() {
    var urlHome = '/Home/GetEmployeeList';
    $('#myTable').DataTable({
        paging: true,
        searching: true,
        ajax: {
            url: urlHome,
            type: "POST",
            datatype: "json"
        },
        processing: true,
        serverSide: true,
        filter: true,
        columns: [
            { data: "productName", name: "ProductName" },
            { data: "categoryName", name: "CategoryName" },
            { data: "customerName", name: "CustomerName" },
            { data: "orderDateStr", name: "OrderDate" },
            { data: "amount", name: "Amount" },
        ]
    })
}