//var apiCall = (value) => {
//    switch (value) {
//        case "GET": {
//            fetch("http://localhost:63501/api/values", { headers: { 'Accept': 'application/json' } })
//                .then((response) => { return response.text() })
//                .then((data) => {
//                    document.getElementById("output").innerHTML = data;
//                });
//        } break;
//        case "POST": {
//            let param = document.getElementById("input").value;
//            fetch(`http://localhost:63501/api/values?result=${param}`, { method: "POST" });
//        } break;
//        case "PUT": {
//            let param = document.getElementById("input").value;
//            fetch(`http://localhost:63501/api/values?add=${param}`, { method: "PUT" });
//        } break;
//        case "DELETE": {
//            fetch("http://localhost:63501/api/values", { method: "DELETE" });
//        } break;
//    }
//}
$("#getBtn").click(function () {
    $.ajax({
        url: "http://localhost:63501/api/values",
        success: function (data) {
            $("#output").html(data);
        }
    });
});

$("#postBtn").click(function () {
    let param = $("#input").val();
    $.ajax({
        url: `http://localhost:63501/api/values?result=${param}`,
        method: "POST"
    });
});

$("#putBtn").click(function () {
    let param = $("#input").val();
    $.ajax({
        url: `http://localhost:63501/api/values?add=${param}`,
        method: "PUT"
    });
});

$("#deleteBtn").click(function () {
    $.ajax({
        url: "http://localhost:63501/api/values",
        method: "DELETE"
    });
});