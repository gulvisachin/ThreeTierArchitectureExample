var dtable;
$(document).ready(function () {
    dtable = $('#myTable').DataTable({
        "ajax": {
            "url": "states/Allstates",
            //success: function (resp) {
            //    console.log(resp)
            //}
        }
        ,
        "columns": [
            { "data": "name" },
            { "data": "country.name" },
            {
                "data": "id",
                "render": function (data) {
                    return `<a href="/states/CreateUpdate?id=${data}"><i class="bi bi-pencil-square"></i></a> 
                            <a onClick=Remove("/states/Delete/${data}")><i class="bi bi-trash3-fill"></i></a> 
                            `
                }
            },

        ]

    });
});


function Remove(url) {
    DeleteRecord(url)
}