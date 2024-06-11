const filters = {
    "priority": 0,
    "status": ""
}

$(document).ready(function () {
    $(".filter-section").hide();

    GetTasks(filters)

    $("#apply-filters").on("click", function () {
        var filtersObj = {
            "priority": parseInt($("#apply-priority").val()),
            "status": $("#apply-status").val()
        }
        GetTasks(filtersObj);
    });

    $("#reset-filters").on("click", function () {
        $('#apply-priority').prop('selectedIndex', 0);
        $('#apply-status').prop('selectedIndex', 0);

        var filtersObj = {
            "priority": 0,
            "status": ""
        }
        GetTasks(filtersObj);
    });
})

function GetTasks(filters) {

    $.ajax({
        type: "POST",
        url: "/api/Task/filtered-tasks-list",
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify(filters),
        success: function (result, xhr) {
            var htmlContent = "";
            if (xhr != "nocontent") {
                $.each(result, function (index, item) {

                    var datestring = item.dueDate.toString();
                    var date = new Date(item.dueDate);
                    var dueDate = `${date.getFullYear()}-${date.getMonth() + 1}-${date.getDate()}`;
                    var htmlRaw = `<tr>
                    <input type="hidden" id="taskId" value="${item.id}"/>
                                <td><h2>${item.title}</h2></td>
                                <td><p>${item.description}</p></td>
                                <td>${item.priority}</td>
                                <td>${datestring.includes("0001-01-01") ? '-' : dueDate}</td>
                                <td><span class="${item.status == "Active" ? "active-status" : "Inactive-status"}">${item.status}</span></td>
                                <td>
                                    <div class="table-button">
                                        <button type="button" class="editbtn btn editTask"><i class="fa-solid fa-pen-to-square"></i>Edit</button>
                                        <button type="button" class="deletebtn btn deleteTask"><i class="fa-regular fa-trash-can"></i>Delete</button>
                                    </div>
                                </td>
                            </tr>`;
                    htmlContent = htmlContent + htmlRaw;

                })
            }
            else {
                htmlContent = "<tr><td>No records found!</td><td></td><td></td><td></td><td></td><td></td></tr>"
            }
            $(".list-raw").html(htmlContent);
        },
        error: function (result) {
            console.log(result)
        }

    })
}

$(document).on("click", ".deleteTask", function () {

    Swal.fire({
        title: "Do you want to remove this taks?",
        showCancelButton: true,
        confirmButtonText: "Yes"
    }).then((result) => {
        /* Read more about isConfirmed, isDenied below */
        if (result.isConfirmed) {
            var taskId = $(this).parent().closest("tr").find("#taskId").val();
            if (taskId != "") {
                $.ajax({
                    type: "DELETE",
                    url: "/api/Task/remove-task?taskId=" + taskId,
                    contentType: 'application/json; charset=utf-8',
                    success: function (result, xhr) {
                        if (xhr = "success")
                            GetTasks(filters);
                    },
                    error: function (result) {
                        console.log(result)
                    }
                })
            }
        }
    });

    //var result = confirm("Do you want to remove this taks?");
    //if (result) {
    //    var taskId = $(this).parent().closest("tr").find("#taskId").val();
    //    if (taskId != "") {
    //        $.ajax({
    //            type: "DELETE",
    //            url: "/api/Task/remove-task?taskId=" + taskId,
    //            contentType: 'application/json; charset=utf-8',
    //            success: function (result, xhr) {
    //                if (xhr = "success")
    //                    GetTasks(filters);
    //            },
    //            error: function (result) {
    //                console.log(result)
    //            }

    //        })
    //    }
    //}
});

$(document).on("click", ".editTask", function () {
    var taskId = $(this).parent().closest("tr").find("#taskId").val();
    if (taskId != "") {
        let url = "/Home/CreateOrUpdate?taskId=" + taskId;
        window.location.href = url;
    }
});

$("#filterBtn").on("click", function () {
    $(".filter-section").toggle();
})
