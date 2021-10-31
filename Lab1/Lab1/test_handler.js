var apiCall = (value) => {
    switch (value) {
        case "GET": {
            fetch("http://localhost:62607/a.svv", { headers: { 'Accept': 'application/json' }})
                .then((response) => { return response.text() })
                .then((data) => {
                    document.getElementById("output").innerHTML = data;
                });
        } break;
        case "POST": {
            let param = document.getElementById("input").value;
            fetch(`http://localhost:62607/a.svv?result=${param}`, { method: "POST"});
        } break;
        case "PUT": {
            let param = document.getElementById("input").value;
            fetch(`http://localhost:62607/a.svv?push=${param}`, { method: "PUT" });
        } break;
        case "DELETE": {
            fetch("http://localhost:62607/a.svv", { method: "DELETE"  });
        } break;
    }
}