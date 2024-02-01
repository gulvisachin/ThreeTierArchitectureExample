﻿var dtable;
$(document).ready(function () {
    dtable = $('#myTable').DataTable({
        "ajax": {
            "url": "People/AllPeoples"
        }
        ,
        "columns": [
            { "data": "name" },
            { "data": "city" },
            {
                "data": "id",
                "render": function (data) {
                    return `<a href="/People/CreateUpdate?id=${data}"><i class="bi bi-pencil-square"></i></a> 
                            <a onClick=Remove("/People/Delete/${data}")><i class="bi bi-trash3-fill"></i></a> 
                            `
                }
            },

        ]

    });
});


function Remove(url) {
    DeleteRecord(url)
}

//function Remove(url) {
//    Swal.fire({
//        title: 'Are you sure?',
//        text: "You won't be able to revert this!",
//        icon: 'warning',
//        showCancelButton: true,
//        confirmButtonColor: '#3085d6',
//        cancelButtonColor: '#d33',
//        confirmButtonText: 'Yes, delete it!'
//    }).then((result) => {
//        if (result.isConfirmed) {
//            $.ajax({
//                url: url,
//                type: 'DELETE',
//                success: function (data) {
//                    if (data.success) {
//                        dtable.ajax.reload();
//                        toastr.success(data.message)
//                    }
//                    else {
//                        toastr.error(data.error)
//                    }
//      (          }
//            })
//        }
//    })
//}