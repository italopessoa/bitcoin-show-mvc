$(document).ready(function () {
    var table = $("#tableAwards tbody");
    $.ajax({
        url: "/Award/List"
    })
    .done(function (data) {
        $.each(data, function(i, element){
            table.append(`<tr>
                <td>฿ ${element.success}</td>
                <td>฿ ${element.fail}</td>
                <td>฿ ${element.quit}</td>
                <td>฿ ${element.level}</td>
            </tr>`);
        });
    });
});
