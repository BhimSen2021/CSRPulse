$(document).ready(function () {
    "use strict";
    $("#basic-datatable").DataTable({
        keys: !0,
        language: {
            paginate: {
                previous: "<i class='mdi mdi-chevron-left'>",
                next: "<i class='mdi mdi-chevron-right'>"
            }
        },
        drawCallback: function () {
            $(".dataTables_paginate > .pagination").addClass("pagination-rounded")
        },
        "lengthMenu": [[25, 50, 100, -1], [25, 50, 100, "All"]]
    });


});

function Basicdatatable() {
    $(".basic-datatable").DataTable({
        destroy: true,
        keys: !0,
        language: {
            paginate: {
                previous: "<i class='mdi mdi-chevron-left'>",
                next: "<i class='mdi mdi-chevron-right'>"
            }
        },
        drawCallback: function () {
            $(".dataTables_paginate > .pagination").addClass("pagination-rounded")
        },
        "lengthMenu": [[25, 50, 100, -1], [25, 50, 100, "All"]]
    });

    // Enable tooltip
    $('[data-bs-toggle="tooltip"]').tooltip();    
}