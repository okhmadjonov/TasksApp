$(document).ready(function () {
    var table = $('#auditTable').DataTable({
        scrollX: true,
        scrollCollapse: true,
        paging: false,
        columnDefs: [{
            sortable: false,
            orderable: false,
            "class": "index",
            targets: 0
        }],
        fixedColumns: true
    });

    table.on('order.dt search.dt', function () {
        table.column(0, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
            cell.innerHTML = i + 1;
        });
    }).draw();
});
