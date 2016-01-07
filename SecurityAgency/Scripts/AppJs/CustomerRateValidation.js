$("#CustomerHourlyRate-form").validate({
    focusInvalid: false,
    focusCleanup: true,

    rules: {
        HourlyRate: {
            required: true,

        },

        CustomerId: {
            required: true,

        },
        EffectiveFrom: {
            required: true,
        },

        EffectiveTo: {
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