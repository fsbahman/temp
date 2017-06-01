console.log("good I'm loaded!");
function AddItem() {
    var obj = {
        OrderedBy: '1B2A0CD5-A230-4A5F-812C-ECA54F63D69B',
        SalesOrderLines: [
            {
                Item: '336583bf-dd08-4d27-8b65-7e60a2960868',
                Quantity: 80
            },
            {
                Item: 'cb9cecdc-de58-4f63-b17e-db87e6367750',
                Quantity: 50
            }]
    };
    $.ajax({
        type: "POST",
        contentType:"application/json",
        url: "http://localhost/YetAnotherBranch/api/v1/99/salesorder/SalesOrders",
        data: JSON.stringify(obj),
        success: SuccessFunction,
        dataType: "json"
    });
}

function SuccessFunction(data){
    console.log(data);
}