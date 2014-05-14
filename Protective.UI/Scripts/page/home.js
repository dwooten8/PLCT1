var HomeManager = function () {

    var InitMessageTable = function () {

    }

    var DisplayError = function (element, msg) {
        App.unblockUI(element);
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
        }
    }
}();