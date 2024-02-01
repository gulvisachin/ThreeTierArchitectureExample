var dtable;
$(document).ready(function () {
    dtable = $('#myTable').DataTable({
        "ajax": {
            "url": "Countries/AllCountries"
        }
        ,
        "columns": [
            { "data": "name" },
            {
                "data": "id",
                "render": function (data) {
                    return `<a href="/Countries/CreateUpdate?id=${data}"><i class="bi bi-pencil-square"></i></a> 
                            <a onClick=Remove("/Countries/Delete/${data}")><i class="bi bi-trash3-fill"></i></a> 
                            `
                }
            },

        ]

    });
});

function Remove(url) {
    DeleteRecord(url)
}