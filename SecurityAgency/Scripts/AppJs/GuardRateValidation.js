$("#GuardRate-form").validate({
    focusInvalid: false,
    focusCleanup: true,

    rules: {
        HourlyRate: {
           required: true,
           
        },

        GuardId: {
            required: true,
           
        },
        EffectiveFrom: {
           required: true, 
        },

        EffectiveFrom:{
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