var HomeManager = function () {

    var InitMessageTable = function () {

        if (!jQuery().dataTable) {
            return;
        }

        $('#message-table').dataTable({
            "aoColumnDefs": [
                { "bSortable": true, "aTargets": [0] }
            ],
            "aaSorting": [[0, 'asc']],
            "aLengthMenu": [
               [5, 10, 25, 50],
               [5, 10, 25, 50] // change per page values here
            ],
            // set the initial value
            "iDisplayLength": 5,
            "sPaginationType": "bootstrap",
            "oLanguage": {
                "sEmptyTable": "No profiles found",
                "sZeroRecords": "No profiles found",
                "sLengthMenu": "_MENU_ records displayed per page",
                "oPaginate": {
                    "sPrevious": "Prev",
                    "sNext": "Next"
                }
            }
        });

        jQuery('#message-table_wrapper .dataTables_filter input').addClass("form-control input-medium"); // modify table search input
        jQuery('#message-table_wrapper .dataTables_length select').addClass("form-control input-small"); // modify table per page dropdown
    }

    var DisplayError = function (msg) {
        toastr.options = {
            positionClass: "toast-top-right",
            showMethod: 'slideDown',
            showDuration: 'fast',
            hideMethod: 'slideUp',
            hideDuration: 'fast',
            fadeOut: 250,
            newestOnTop: false
        }
        toastr.error(msg);
    }


    return {

        //main function to initiate the module
        init: function () {
            InitMessageTable();
        },

        DeleteMessage: function (id, index) {

            var table = $('#message-table').dataTable();
            var row = $('#Row' + index).get(0)

            $.ajax({
                type: "POST",
                url: "/Home/DeleteMessage",
                data: { id: id },
                success: function (result) {
                    if (result.success == true) {
                        table.fnDeleteRow(table.fnGetPosition(row));
                    }
                    else
                        DisplayError("Unable to delete the message.  Please contact technical support.")
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    DisplayError("Unable to delete the message.  Please contact technical support.")
                }
            });
        },

        SetStar: function (id, index) {
            
            var image = $('#Column' + index).find('i');
            var input = $('#Column' + index).find('input');

            $.ajax({
                type: "POST",
                url: "/Home/SetStar",
                data: { id: id, isStarred: input.val() },
                success: function (result) {
                    if (result.success == true) {

                        if (input.val() == 'True') {
                            image.removeClass('fa-star');
                            image.addClass('fa-star-o');
                            input.val('False');
                        }
                        else {
                            image.removeClass('fa-star-o');
                            image.addClass('fa-star');
                            input.val('True');
                        }
                    }
                    else
                        DisplayError("Unable to change the rating.  Please contact technical support.")
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    DisplayError("Unable to change the rating.  Please contact technical support.")
                }
            });
        }
    }
}();