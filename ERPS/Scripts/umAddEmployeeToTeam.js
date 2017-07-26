//var serverUrl = "http://localhost:53654/api/AddEmployee/AddEmployeeToTeam";
var serverUrl = "http://localhost:64292/ManagementAddEmployee/ParseJSONEmployee";

$(document).on('click', '#form-submit', function () {
    $.ajax({
        url: "AddEmployee",
        //url: "ParseJSONEmployee",
        type: "POST",
        data: $('form#my-form').serialize(),
    });
});

function addEmployeeToTeam() {
    window.alert("hello");
    var xhttp = new XMLHttpRequest();
    xhttp.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            document.getElementById("name").innerHTML = "Hacked";
        }
    };
    xhttp.open("POST", serverUrl, true);
    //xhttp.open("GET", serverUrl, true);
    var employee = { "Id": 1, "name": "User", "Age": 20 };
    myJSON = JSON.stringify(employee);
    // Use overloaded with string to send post request
    xhttp.send(myJSON);
    //xhttp.send();
}