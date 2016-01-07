var Common = {

    showGenericModal: function (title, handlebarUrl, callbackFunction) {
        var modalBodyContainer = $("#modalBody");
        $("#modalTitle").html(title);
        $(modalBodyContainer).load(handlebarUrl);
        $("#generalModal").modal({ show: true });
    },

    closeGenericModal: function () {
        $("#generalModal").modal('hide');
    }


};

    var Permissions=
{
    AddCustomer:2,
    EditCustomer : 3,
    DeleteCustomer : 4,
    ViewCustomer : 5,
    AddGuard : 6,
    EditGuard : 7,
    DeleteGuard : 8,
    ViewGuard : 9,
    AddUser : 10,
    EditUser : 11,
    DeleteUser : 13,
    ViewUser : 14,
    AddCustomerHourlyRate:18,
    EditCustomerHourlyRate:19,
    DeleteCustomerHourlyRate:21,
    ViewCustomerHourlyRate:22,
    AddGuardHourlyRate:30,
    EditGuardHourlyRate:31,
    DeleteGuardHourlyRate:32,
    ViewGuardHourlyRate:33,
    AddDailyLog:35,
    EditDailyLog:36,
    DeleteDailyLog:37,
    ViewDailyLog:38
}
    var Role=
    {
        Admin: 1,
        Supervisor: 2,
        User: 3
    }