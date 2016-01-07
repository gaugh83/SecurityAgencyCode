$("#User-form").validate({
    focusInvalid: false,
    focusCleanup: true,

    rules: {
        UserName: {
            required: true,
            maxlength: 32,
        },

        Email: {
            required: true,
            regex: "^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$"
        },
        Address: {
            required: true,
            maxlength: 512,
        },
        RoleId:{
            required:true,
        },
        Password:
            {
                required:true,
            },
        ContactNo:
            {
                required: true,
                minlength: 10,
                maxlength: 20,

            },
        Zip:
       {
           required: true,
           minlength: 6,
           maxlength: 10,

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