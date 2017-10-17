function loadAwards() {
    var table = $("#tableAwards tbody");
    table.children().empty();
    $.ajax({
            url: "/Award/List"
        })
        .done(function (data) {
            $.each(data, function (i, element) {
                table.append(`<tr>
                <td>฿ ${element.success}</td>
                <td>฿ ${element.fail}</td>
                <td>฿ ${element.quit}</td>
                <td>${element.levelName}</td>
                <td>
                <a href='/Award/Edit/${element.id}'><i class="material-icons">mode_edit</i></a>
                <a onclick="deleteAward(${element.id});" href='#'><i class="material-icons">delete</i></a>
                </td>
                </tr>`);
            });
        });
}

function deleteAward(id) {
    var dataPost = {
        id
    };
    $.ajax({
        type: "DELETE",
        url: "/Award/Delete",
        data: dataPost,
        success: () => Materialize.toast("Removed!", 4000, "green"),
        error: () => Materialize.toast("Error!", 4000, "red")
    }).done(function () {
        loadAwards();
    });
}

$(document).ready(function () {
    loadAwards();
});
