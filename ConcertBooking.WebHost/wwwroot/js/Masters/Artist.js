var dtable;
$(document).ready(function () {
    dtable = $('#myTable').DataTable({
        "ajax": {
            "url": "Artists/AllArtists"
        }
        ,
        "columns": [
            { "data": "name" },
            { "data": "bio" },
            {
                "data": "imageUrl",
                "render": function (data, type, row, meta) {
                    return `<img  src=${data} class="rounded-circle" height="30px" width="30px"/>`
                }
            },
            {
                "data": "id",
                "render": function (data) {
                    return `<a href="/Artists/CreateUpdate?id=${data}"><i class="bi bi-pencil-square"></i></a> 
                            <a onClick=Remove("/Artists/Delete/${data}")><i class="bi bi-trash3-fill"></i></a> 
                            `
                }
            },

        ]

    });
});

function Remove(url) {
    DeleteRecord(url)
}