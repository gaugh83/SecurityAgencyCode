$("#GuardPayment-form").validate({
    focusInvalid: false,
    focusCleanup: true,

    rules: {
        PaymentDate: {
            required: true,
        },

        GuardId: {
            required: true,
        },
        StartDate: {
            required: true,
        },

        EndDate: {
            required: true,
        },

        Amount: {
            required: true,
        },
        HourlyRate: {
            required: true,
            regex: "^(?=.*[1-9])\\d*(?:\\.\\d{1,2})?$"
        },
        TotalHours: {
            required: true,
            regex: "^[1-9][0-9]*$"
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