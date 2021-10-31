$(document).ready(function () {

    GetAllRecords();

    $("#editRecord").click(function (event) {
        event.preventDefault();
        EditRecord();
    });

    $("#addRecord").click(function (event) {
        event.preventDefault();
        AddRecord();
    });

    $("#sort").click(function (event) {
        event.preventDefault();
        GetAllRecords(`?sort=${$('#sortOptions').val() + $('#orderOptions').val()}`);
    });

    $("#like").click(function (event) {
        event.preventDefault();
        GetAllRecords(`?like=${$('#likeInput').val()}`);
    });

    $("#globalLike").click(function (event) {
        event.preventDefault();
        GetAllRecords(`?globalLike=${$('#globalLikeInput').val()}`);
    });

    $("#format").click(function (event) {
        event.preventDefault();
        GetAllRecords(`?format=${$('#formatOptions').val()}`);
    });


    $("#fetch").click(function (event) {
        event.preventDefault();
        GetAllRecords(`?limit=${$('#limitInput').val()}&offset=${$('#offsetInput').val()}`);
    });

    $("#limit").click(function (event) {
        event.preventDefault();
        GetAllRecords(`?maxId=${$('#maxIdInput').val()}&minId=${$('#minIdInput').val()}`);
    });

    $("#columns").click(function (event) {
        event.preventDefault();
        let columns = '';
        $.each($("input[name='columns']:checked"), function () {
            columns += $(this).val() + ','
        })
        GetAllRecords(`?columns=${columns}`);
    });
});

function XML2JS(xmlDoc, containerTag) {
    var output = new Array();
    var rawData = xmlDoc.getElementsByTagName(containerTag)[0];
    var i, j, oneRecord, oneObject;
    for (i = 0; i < rawData.childNodes.length; i++) {
        if (rawData.childNodes[i].nodeType == 1) {
            oneRecord = rawData.childNodes[i]; oneObject = output[output.length] = new Object();
            for (j = 0; j < oneRecord.childNodes.length; j++) {
                if (oneRecord.childNodes[j].nodeType == 1) {
                    if (oneRecord.childNodes[j].tagName === 'Links') {
                        oneObject[oneRecord.childNodes[j].tagName] = XML2JS(oneRecord, 'Links');
                    } else {
                        oneObject[oneRecord.childNodes[j].tagName] = oneRecord.childNodes[j].firstChild.nodeValue;
                    }

                }
            }
        }
    }
    return output;
}

function GetAllRecords(querystring) {
    $("#createBlock").css('display', 'block');
    $("#editBlock").css('display', 'none');
    $.ajax({
        url: querystring != undefined ? `/api/students${querystring}` : '/api/students/',
        type: 'GET',
        success: function (response, status, xhr) {
            if (xhr.getResponseHeader('content-type').includes('application/json'))
                WriteResponse(JSON.parse(response));
            else {
                WriteResponse(XML2JS(response, 'ArrayOfStudentDto'));
            }
        },
        error: function (x, y, z) {
            alert(x + '\n' + y + '\n' + z);
        }
    });
}

function GetRecord(href) {
    $.ajax({
        url: href,
        type: 'GET',
        dataType: 'json',
        success: function (data) {
            ShowRecord(data);
        },
        error: function (x, y, z) {
            alert(x + '\n' + y + '\n' + z);
        }
    });
}

function AddRecord() {
    let record = {
        Name: $('#addName').val(),
        Phone: $('#addPhone').val(),
    };

    $.ajax({
        url: '/api/students/',
        type: 'POST',
        data: JSON.stringify(record),
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            GetAllRecords();
        },
        error: function (x, y, z) {
            alert(x + '\n' + y + '\n' + z);
        }
    });
}

function DeleteRecord(href) {

    $.ajax({
        url: href,
        type: 'DELETE',
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            GetAllRecords();
        },
        error: function (x, y, z) {
            alert(x + '\n' + y + '\n' + z);
        }
    });
}

function EditRecord() {
    let id = $('#editId').val()
    let record = {
        Id: $('#editId').val(),
        Name: $('#editName').val(),
        Phone: $('#editPhone').val()
    };
    $.ajax({
        url: '/api/students/' + id,
        type: 'PUT',
        data: JSON.stringify(record),
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            GetAllRecords();
        },
        error: function (x, y, z) {
            alert(x + '\n' + y + '\n' + z);
        }
    });
}

function WriteResponse(records) {
    var strResult = "<table><th>ID</th><th>Name</th><th>Phone</th>";
    $.each(records, function (index, record) {
        strResult += "<tr><td>" + record.StudentId + "</td><td> " + record.Name + "</td><td>" +
            record.Phone + "</td>";
        record.Links.forEach(link => {
            if (link.Rel !== "self") {
                strResult += `<td><input type="button" data-item="${link.Href}" value="${link.Method}" onclick="${link.Rel}(this)" /></td>`;
            }
        });
    });
    strResult += "</table>";
    $("#tableBlock").html(strResult);

}

function DeleteStudent(el) {
    var href = $(el).attr('data-item');
    DeleteRecord(href);
}

function UpdateStudent(el) {
    var href = $(el).attr('data-item');
    GetRecord(href);
}

function ShowRecord(record) {
    if (record != null) {
        $("#createBlock").css('display', 'none');
        $("#editBlock").css('display', 'block');
        $("#editId").val(record.StudentId);
        $("#editName").val(record.Name);
        $("#editPhone").val(record.Phone);
    }
    else {
        alert("Such record doesn't exist");
    }
}

function TestDeleteError() {
    let bad_id = document.getElementById('badId').value;
    $.ajax({
        url: 'http://localhost:54218/api/students/' + bad_id,
        type: 'DELETE',
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            GetAllRecords();
        },
        error: function (x, y, z) {
            let result = document.getElementById('show_error_resp');
            result.innerHTML = x.responseText;
        }
    });
}