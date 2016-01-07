$("#Customer-form").validate({
    focusInvalid: false,
    focusCleanup: true,

    rules: {
        BeginDate: {
            required: true,
        },

        EndDate: {
            required: true,
        },
        TotalHours: {
            required: true,
            regex: "^[1-9][0-9]*$"
        },
        CustomerId: {
            required: true,
        },
        InvoiceDate: {
            required: true,
        },
        Amount: {
            required: true,
            regex: "^(?=.*[1-9])\\d*(?:\\.\\d{1,2})?$"
        },

        InvoiceNo: {
            required: true,
            regex: "^(\\+|-)?(\\d*\\.?\\d*)$"
        },
        HourlyRate:
       {
           required: true,
           regex: "^(?=.*[1-9])\\d*(?:\\.\\d{1,2})?$"
       },

    },
    highlight: function (element) {
        $(element).addClass('error');
    },
    unhighlight: function (element) {
        $(element).removeClass('error');
    },
    errorPlacement: function (error, element) {
    }
});