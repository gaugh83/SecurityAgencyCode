$("#Guard-form").validate({
    focusInvalid: false,
    focusCleanup: true,

    rules: {
        Name: {
           required: true,
            maxlength: 50,
        },

        SSN: {
            required: true,
            minlength: 9,
            maxlength: 9,
            regex: "^[1-9][0-9]*$"
        },
        Address: {
            required: false,
            maxlength: 512,
        },
        ContactNo:
            {
                required: false,
                maxlength: 10,
            },
        Zip:
       {
           required: false,
           maxlength: 6,

       },
        HourlyRate:
       {
           required: true,
           maxlength: 8,
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