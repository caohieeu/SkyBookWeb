$('#tblData').DataTable({
    ajax: '/api/product/getall',
    columns: [
        { data: 'Title' },
        { defaultContent: ''}
    ],

})

fetch("/api/product/getall")
    .then(respond => respond.json())
    .then(result => {
        console.log(result);
    })
    .catch(err => {
        console.log(err);
    })