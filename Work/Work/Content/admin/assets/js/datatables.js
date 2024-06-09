/*
 Template Name: Zegva - Responsive Bootstrap 4 Admin Dashboard
 Author: Themesdesign
 Website: www.themesdesign.in
 File: Datatable js
 */

 $(document).ready(function() {
  

    //Buttons examples
     var table = $('#datatable').DataTable({
         lengthChange: true,
        dom: 'Bfrtip',
        buttons: [
            {
                extend: 'copyHtml5',
                exportOptions: {
                    columns: ':visible' 
                }
            },
            {
                extend: 'csvHtml5',
                exportOptions: {
                    columns: ':visible'
                }
            },
            {
                extend: 'excelHtml5',
                exportOptions: {
                    columns: ':visible'
                }
            },
            {
                extend: 'pdfHtml5',
                exportOptions: {
                    columns: ':visible'
                }
            },
            {
                extend: 'print',
                exportOptions: {
                    columns: ':visible'
                }
            }
            ,
            {
                extend: 'colvis',
               
            }
        ],
      
         "language": {
             "zeroRecords": "Không tìm thấy kết quả phù hợp",
             "info": "Hiển thị _START_ tới _END_ của _TOTAL_ mục",
             "search": "Tìm Kiếm",
             "emptyTable": "Chưa có dữ liệu!",
         }
     });

    

     $('#prints').DataTable({
         searching: false,
         ordering: false,
         "language": {
             "emptyTable": "Chưa có sản phẩm !"
         }
     });
 });