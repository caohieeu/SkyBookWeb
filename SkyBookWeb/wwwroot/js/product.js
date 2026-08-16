//$('#tblData').DataTable({
//    ajax: {
//        url: '/api/product/getall',
//        dataSrc: ''
//    },
//    columns: [
//        { data: 'title', width: "25%" },
//        { data: 'isbn', width: "15%" },
//        { data: 'price', width: "10%", "render": function (data) { return '$' + data.toFixed(2); } },
//        { data: 'author', width: "15%" },
//        {
//            data: 'category.name', width: "10%", "render": function (data)
//            {
//                return '<span class="badge bg-secondary">' + data + '</span>';
//            }
//        },
//        { defaultContent: '', width: "25%" }
//    ],

//})

//fetch("/api/product/getall")
//    .then(respond => respond.json())
//    .then(result => {
//        console.log(typeof result);
//        console.log(result);
//    })
//    .catch(err => {
//        console.log(err);
//    })

function assignDataToProductDiv(data) {
    var strHtml = "";
    data.forEach(item => {
        let strItemHtml = "<tr>";
        strItemHtml += `<td class="fw-medium">` + item.title + "</td>";
        strItemHtml += `<td class="fw-medium">` + item.isbn + "</td>";
        strItemHtml += `<td class="fw-medium">` + item.author + "</td>";
        strItemHtml += `<td class="fw-medium">` + item.price + "</td>";
        strItemHtml += `<td><span class="badge bg-light text-dark border">` + item.category + "</td>";
        strItemHtml += `<td class="text-end">`;
        strItemHtml += `<a href="Product/Upsert/${item.id}" class="btn btn-sm btn-outline-success me-1">
                                <i class="bi bi-pencil-square me-1"></i>Edit
                            </a>`;
        strItemHtml += `<a onclick="Delete(${item.id})" class="btn btn-sm btn-outline-danger me-1">
                                <i class="bi bi-trash me-1"></i>Delete
                            </a>`;
        strItemHtml += "</td>";
        strItemHtml += "</tr>";

        strHtml += strItemHtml;
    });
    $(".table-data-product").html(strHtml);
}

function changeIconColumn(iconClassName) {
    let icon = $(this).find("i")[0];
    if (icon.classList.contains("bi-arrow-up")) {
        icon.classList.remove("bi-arrow-up")
        icon.classList.add("bi-arrow-down")
    }
    else {
        icon.classList.remove("bi-arrow-down")
        icon.classList.add("bi-arrow-up")
    }
}

function loadData(queryParams) {
    $(document).ready(function () {

        $.ajax({
            type: "GET",
            url: `${window.location.protocol}//${window.location.host}/api/product/GetAll`,
            data: {
            search: queryParams?.search,
            pageIndex: queryParams?.pageIndex,
            pageSize: queryParams?.pageSize,
            sort: queryParams?.sort
        },
            dataType: "json",
            success: function (data) {
                assignDataToProductDiv(data)
            },
            error: function (error) {
                console.error("AJAX load product data error: ", error)
            }
        })
})
    }

loadData();

$('#submitSearch').click(function () {
    console.log($('#search-inp').val())
    var searchValue = {
        search: $('#search-inp').val(),
        sort: $('#nameType').val() + $('#orderBy').val(),
        pageIndex: 1,
        pageSize: 10
    }

    loadData(searchValue)
})

//swagger
function Delete(id) {
    Swal.fire({
        title: "Are you sure?",
        text: "You won't be able to revert this!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Yes, delete it!"
    }).then((result) => {
        if (result.isConfirmed) {
            fetch(`${window.location.protocol}//${window.location.host}/api/product/delete/${id}`, {
                method: 'DELETE'
            })
                .then(respone => {
                    if (!respone.ok)
                        throw new Error("Delete failed");

                    return respone.json();
                })
                .then(data => {
                    if (data.success) {
                        if (result.isConfirmed) Swal.fire({
                            title: "Deleted!",
                            text: data.message,
                            icon: "success"
                        }).then((result) => {
                            loadData();
                        });
                    }
                    else {
                        Swal.fire({
                            icon: "error",
                            title: "Oops...",
                            text: data.message,
                            footer: "<a href=\"#\">Why do I have this issue?</a>"
                        });
                    }
                })
                .catch(ex => console.log(ex))
        }
    });
}