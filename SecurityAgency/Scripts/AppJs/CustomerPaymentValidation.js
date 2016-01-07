$("#CustomerPayment-form").validate({
    focusInvalid: false,
    focusCleanup: true,

    rules: {
        CustomerId: {
            required: true,
        },

        InvoiceId: {
            required: true,
        },
        Amount: {
            required: true,
            regex: "^(?=.*[1-9])\\d*(?:\\.\\d{1,2})?$"
        },

        PaymentDate: {
            required: true,
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