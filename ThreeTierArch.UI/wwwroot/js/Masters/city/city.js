var dtable;
$(document).ready(function () {
    dtable = $('#myTable').DataTable({
        "ajax": {
            "url": "Cities/AllCities"
        }
        ,
        "columns": [
            { "data": "name" },
            { "data": "state.name" },
            { "data": "state.country.name" },
            {
                "data": "id",
                "render": function (data) {
                    return `<a href="/Cities/CreateUpdate?id=${data}"><i class="bi bi-pencil-square"></i></a> 
                            <a onClick=Remove("/Cities/Delete/${data}")><i class="bi bi-trash3-fill"></i></a> 
                            `
                }
            },

        ]

    });
});

function Remove(url) {
    DeleteRecord(url)
}