var taskId = "";

$(document).ready(function () {
    $("#updateBtn").hide();

    const urlParams = new URLSearchParams(window.location.search);
    const taskIdParam = urlParams.get('taskId');

    if (taskIdParam) {
        taskId = taskIdParam
        SetTaskDetails();
    }
})

$("#createBtn").on("click", function () {
    if (validateInputs()) {
        var model = {
            title: $("#title").val(),
            description: $("#description").val(),
            priority: $("#priority").val(),
            dueDate: $("#due-date").val(),
            status: $("#status").val()
        }
        $.ajax({
            type: "POST",
            url: "/api/Task/add-task",
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify(model),
            success: function (result) {
                window.location.href = "/";
            },
            error: function (result) {
                console.log(result)
            }
        });
    }    
})

$("#updateBtn").on("click", function () {
    if (validateInputs()) {
        var model = {
            id: taskId,
            title: $("#title").val(),
            description: $("#description").val(),
            priority: $("#priority").val(),
            dueDate: $("#due-date").val(),
            status: $("#status").val()
        }
        $.ajax({
            type: "PUT",
            url: "/api/Task/modify-task",
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify(model),
            success: function (result) {
                window.location.href = "/";
            },
            error: function (result) {
                console.log(result)
            }
        });
    }
})

function validateInputs() {
    var isValid = true;
    if ($("#title").val().trim() == "") {
        $("#title").addClass("border-danger");
        isValid = false;
    }
    else {
        $("#title").removeClass("border-danger");
    }
    if ($("#due-date").val() == "") {
        $("#due-date").addClass("border-danger");
        isValid = false;
    }
    else {
        $("#due-date").removeClass("border-danger");
    }
    return isValid;
}

function formatDate(date) {
    var d = new Date(date),
        month = '' + (d.getMonth() + 1),
        day = '' + d.getDate(),
        year = d.getFullYear();

    if (month.length < 2)
        month = '0' + month;
    if (day.length < 2)
        day = '0' + day;

    return [year, month, day].join('-');
}

function SetTaskDetails() {
    $.ajax({
        type: "GET",
        url: "/api/Task/get-task?taskId=" + taskId,
        contentType: 'application/json; charset=utf-8',
        success: function (result, xhr) {
            if (xhr = "success") {
                var date = new Date(result.dueDate);
                var dueDate = `${date.getFullYear()}-${date.getMonth()}-${date.getDate()}`;
                $("#createBtn").hide();
                $("#updateBtn").show();
                $("#title").val(result.title);
                $("#description").val(result.description);
                $("#priority").val(result.priority);
                $("#due-date").val(formatDate(date));
                $("#status").val(result.status);
            }
        },
        error: function (result) {
            console.log(result)
        }

    })
}