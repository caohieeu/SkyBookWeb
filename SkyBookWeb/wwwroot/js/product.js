$('#tblData').DataTable({
    ajax: {
        url: '/api/product/getall',
        dataSrc: ''
    },
    columns: [
        { data: 'title', width: "25%" },
        { data: 'isbn', width: "15%" },
        { data: 'price', width: "10%", "render": function (data) { return '$' + data.toFixed(2); } },
        { data: 'author', width: "15%" },
        {
            data: 'category.name', width: "10%", "render": function (data)
            {
                return '<span class="badge bg-secondary">' + data + '</span>';
            }
        },
        { defaultContent: '', width: "25%" }
    ],

})

fetch("/api/product/getall")
    .then(respond => respond.json())
    .then(result => {
        console.log(typeof result);
        console.log(result);
    })
    .catch(err => {
        console.log(err);
    })