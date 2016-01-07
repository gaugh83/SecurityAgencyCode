$("#DailyLog-form").validate({
    focusInvalid: false,
    focusCleanup: true,

    rules: {
        Hours: {
            required: true,
            regex: "^[1-9][0-9]*$"
        },

        Dated:{
           required:true,
        },
        CustomerId:{
           required:true,
        },
        GuardId:{
           required:true,
        },
        Comments: {
            
            maxlength: 100,
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