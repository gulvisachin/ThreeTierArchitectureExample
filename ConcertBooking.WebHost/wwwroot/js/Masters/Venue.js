var dtable;
$(document).ready(function () {
    dtable = $('#myTable').DataTable({
        "ajax": {
            "url": "Venues/AllVenues",
            //  success: function (resp) {
            //    console.log(resp)
            //}
        }
        ,
        "columns": [
            { "data": "name" },
            { "data": "address" },
            { "data": "seatCapacity" },
            {
                "data": "id",
                "render": function (data) {
                    return `<a href="/Venues/CreateUpdate?id=${data}"><i class="bi bi-pencil-square"></i></a> 
                            <a onClick=Remove("/Venues/Delete/${data}")><i class="bi bi-trash3-fill"></i></a> 
                            `
                }
            },

        ]

    });
});

function Remove(url) {
    DeleteRecord(url)
}