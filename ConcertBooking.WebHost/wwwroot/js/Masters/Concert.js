var dtable;
$(document).ready(function () {
    dtable = $('#myTable').DataTable({
        "ajax": {
            "url": "Concerts/AllConcerts",
            "loadingElementId":"loader"
            //  success: function (resp) {
            //    console.log(resp)
            //}
        },
           
        
        "columns": [
            { "data": "name" },
            { "data": "description" },
            { "data": "dateTime" },
            { "data": "venue.name" },
            { "data": "artist.name" },
            {
                "data": "imageUrl",
                "render": function (data, type, row, meta) {
                    return `<img  src=${data} height="30px" width="30px"/>`
                }
            },
            {
                "data": "id",
                "render": function (data) {
                    return `<a href="/Concerts/CreateUpdate?id=${data}"><i class="bi bi-pencil-square"></i></a> |
                     <a href="/Concerts/GetTickets?concertId=${data}"><i class="bi bi-ticket-perforated"></i></a> |
                            <a onClick=Remove("/Concerts/Delete/${data}")><i class="bi bi-trash3"></i></a> 
                            `
                }
            },

        ]

    });
});

function Remove(url) {
    DeleteRecord(url)
}