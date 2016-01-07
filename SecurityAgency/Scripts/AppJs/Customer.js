var customerDataTable;

$(document).ready(function () {
    customerDataTable = $('#tblCustomer').dataTable({
        "ajax": "/Customer/GetCustomers"
    });
    $('.loader-wrap').css('display', 'none');
});

function CreateUpdateCustomerPopup(id) {
    debugger;   
    $("#customerId").val(id);
    DuplicateEmail = false;
    var modelHeading;
    if (id > 0) {
        modelHeading = "Update Customer"; 
    }
    else {
        modelHeading = "Create Customer";
    }
    Common.showGenericModal(modelHeading, '/Customer/CreateUpdateCustomerPopup?id=' + id, success());

};

//Delete Record From Databases
function DeleteCustomer(id) {
    alertify.confirm("Are you sure you want to delete this record?", function (e) {
        if (e) {
            $.ajax({
                url: "/Customer/DeleteCustomer" + '/' + id,
                type: 'POST',
                dataType: 'json',
                success: function (data) {
                    if (data > 0) {
                        alertify.success("Record Deleted");
                    }
                    else {
                        alertify.error("Not a Human Error");
                    }
                    $('#tblCustomer').DataTable().ajax.reload();
                    Common.closeGenericModal();
                },
                error: function (statusText) {
                    var error = JSON.parse(statusText.responseText)
                    console.log(error.exceptionMessage);
                }
            });
        } else {
            return false;
        }
    });
}

var DuplicateEmail = false;
function CreateUpdateCustomer(id) {
    debugger;
    $('#buttonSave').prop("disabled", true).addClass("ui-state-disabled");
    if (!DuplicateEmail) {
        $('#customerId').val(id);
        var dataToSend = $('#Customer-form').serialize();
        if ($('#Customer-form').valid()) {
            $.ajax({
                url: '/Customer/CreateUpdateCustomer',
                data: dataToSend,
                type: 'Post',
                async: false,
                success: function (data) {
                    if (data == id) {
                        alertify.success("Record Updated");
                    }
                    else if (data == -1) {
                        alertify.error("Not a Human Error");

                    }
                    else if (data) {
                        alertify.success("Record Added");
                    }
                    else {
                        alertify.error("Not a Human Error");
                    }
                    $('#tblCustomer').DataTable().ajax.reload();
                    Common.closeGenericModal();
                    $('#buttonSave').prop("disabled", false).removeClass("ui-state-disabled");
                },
                error: function (err) {
                    console.log(err);
                    $('#buttonSave').prop("disabled", false).removeClass("ui-state-disabled");
                }
            });
        }
        else {
            $('#buttonSave').prop("disabled", false).removeClass("ui-state-disabled");
        }
    }
    else {
        $('#buttonSave').prop("disabled", false).removeClass("ui-state-disabled");
        alert('Email Address already in use');
    }
}

function GenerateCustomerReport() {
    var startDate = $('#StartDate').val();
    var endDate = $('#EndDate').val();
    if (Date.parse(startDate) > Date.parse(endDate)) {
        alert("Start Date cannot be less than End Date")
        return false;
    }

    var x = screen.width / 2 - 700 / 2;
    var y = screen.height / 2 - 450 / 2;
    window.open('../RDLReports/ReportPages/CustomerReports.aspx?startDate=' + startDate + '&endDate=' + endDate, 'CustomerReport', 'directories=no,titlebar=no,toolbar=no,location=no,status=no,menubar=no,scrollbars=no,resizable=no,width=660,height=550,left=' + x + ',top=' + (y - 80))
}
function validateEmailAddress(CustomerId) {
    $('#buttonSave').prop("disabled", false).removeClass("ui-state-disabled");
    var email = $("#Email").val();
    if (email != '') {
        $.ajax({
            url: "/Customer/validateCustomerEmailAddress?customerId=" + CustomerId + "&email=" + email,
            type: 'POST',
            dataType: 'json',
            async: false,
            success: function (data) {
                if (data) {
                    DuplicateEmail = true;
                    //alert('Email Address already in use');
                }
                else {
                    DuplicateEmail = false;
                }
            },
            error: function (statusText) {
                var error = JSON.parse(statusText.responseText)
                console.log(error.exceptionMessage);
            }
        });
    }
}

function success()
{ };

