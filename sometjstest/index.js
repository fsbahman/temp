function dothis(){
    var eolframe = parent.parent.window.document.getElementById("MainWindow");
    eolframe.src = "LicCustomers.aspx?Status=C&Selector=List%3atList&_Division_=1";

    console.log(parent.parent.window.document.getElementById("MainWindow"));
}