$(document).ready(function () {
    pagination();
    //serach();
    
});



function pagination() {
    var contactLength = $('#gvContact tbody tr').length;
    var limitPerPage = 4;
    var totalPages = Math.round(contactLength / limitPerPage);

    $('.pagination').append('<a href="#" class="current-page active">' + 1 + '</a>')
    $('#gvContact tbody tr:gt(' + (limitPerPage - 1) + ')').hide();
    for (var i = 2; i <= totalPages; i++) {
        $('.pagination').append('<a href="#" class="current-page">' + i + '</a>')
    }
    $('.pagination').append('<a id="nextPage" href="#">' + '&raquo;' + '</a>')
    $('.pagination a.current-page').on('click', function () {
        if ($(this).hasClass('active')) {
            return false;
        }
        else {
            var currentPage = $(this).index();
            $('.pagination a').removeClass('active');
            $(this).addClass('active');
            $('#gvContact tbody tr').hide();

            var total = limitPerPage * currentPage;
            for (var i = total - limitPerPage; i < total; i++) {
                $('#gvContact tbody tr:eq(' + i + ')').show();
            }
        }


    });

    $('#nextPage').on('click', function () {
        var current = $('.pagination a.active').index();
        if (current === totalPages)
            return false;
        else {
            current++;
            $('.pagination a').removeClass('active');
            $('#gvContact tbody tr').hide();
            var total = limitPerPage * current;
            for (var i = total - limitPerPage; i < total; i++) {
                $('#gvContact tbody tr:eq(' + i + ')').show();
            }

            $('.pagination a.current-page:eq(' + (current - 1) + ')').addClass('active');
        }
    });


    $('#previous-page').on('click', function () {
        var current = $('.pagination a.active').index();
        if (current === 1)
            return false;
        else {
            current--;
            $('.pagination a').removeClass('active');
            $('#gvContact tbody tr').hide();
            var total = limitPerPage * current;
            for (var i = total - limitPerPage; i < total; i++) {
                $('#gvContact tbody tr:eq(' + i + ')').show();
            }

            $('.pagination a.current-page:eq(' + (current - 1) + ')').addClass('active');
        }
    });
}


//function serach() {
//    $('.search').on('keyup', function () {
//        var term = $(this).val().toLowerCase();

//        $('tbody tr').each(function () {
//            var lastname = $('tbody tr td:nth-child(2)').text().toLowerCase();
//            var line = $(this).text().toLowerCase();
//            console.log(line);
//            if (line.indexOf(term) === -1)
//                $(this).hide();
//            else
//                $(this).show();
//        });
//    });
//};