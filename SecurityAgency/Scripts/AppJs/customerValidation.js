$("#Customer-form").validate({
    focusInvalid: false,
    focusCleanup: true,

    rules: {
        Name: {
            required: true,
            maxlength: 79,
        },

        Email: {
            required: true,
            regex: "^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$"
            
        },
        Address: {
            maxlength: 512,
        },
        ContactNo:
        {
            maxlength:10,
                   
        },
        Zip:
       {
           maxlength:6,
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