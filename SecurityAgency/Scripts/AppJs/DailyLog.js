var dailylogDataTable;

$(document).ready(function () {
    dailylogDataTable = $('#tbldailylog').dataTable({
        "ajax": "/DailyLog/GetdailyLog"
    });
    $('.loader-wrap').css('display', 'none');
});

function CreateUpdateDailyLogPopup(id) {    
    $("#DailyLogId").val(id);
    $('#buttonSave').prop("disabled", false).removeClass("ui-state-disabled");
    var modelHeading;
    if (id > 0) {
        modelHeading = "Update DailyLog";
    }
    else {
        modelHeading = "Create DailyLog";
    }
    Common.showGenericModal(modelHeading, '/DailyLog/CreateUpdateDailyLogPopup?id=' + id, success());

};

//Delete Record From Databases
function DeleteDailyLog(id) {
    alertify.confirm("Are you sure you want to delete this record?", function (e) {
        if (e) {
            $.ajax({
                url: "/DailyLog/DeleteDailyLog" + '/' + id,
                type: 'POST',
                dataType: 'json',
                success: function (data) {
                    if (data > 0) {
                        alertify.success("Record Deleted");
                    }
                    else {
                        alertify.error("Not a Human Error");
                    }
                    $('#tbldailylog').DataTable().ajax.reload();
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

function CreateUpdateDailyLog(id) {
    $('#buttonSave').prop("disabled", true).addClass("ui-state-disabled");
    //Check log 
    debugger;
    var skipsave = '';
    customerId = $('#CustomerId').val();
    LogDate = $('#Dated').val();
    $.ajax({
        url: '/CustomerInvoice/CheckDailyLogOverlap?customerId=' + customerId + '&LogDate=' + LogDate,
        type: 'GET',
        async: false,
        success: function (data) {
            if (data == "True") {
                skipsave = 'skipsave';
                if (id === 0) {
                    alert('Invoice for this period has already been generated. Please select different date');
                }
                else {
                    alert('Invoice for this period has already been generated. Updates are not allowed now.');
                }
                $('#buttonSave').prop("disabled", false).removeClass("ui-state-disabled");
                return false;
            }
        },
        error: function (err) {
            console.log(err);
            $('#buttonSave').prop("disabled", false).removeClass("ui-state-disabled");
        }
    });
    debugger;
    if (skipsave == '') {
        $('#DailyLogId').val(id);
        if ($('#DailyLog-form').valid()) {
            var dataToSend = $('#DailyLog-form').serialize();
            $.ajax({
                url: '/DailyLog/CreateUpdateDailyLog',
                data: dataToSend,
                type: 'Post',
                success: function (data) {
                    if (data == id) {
                        alertify.success("Record Updated");
                    }
                    else if (data) {
                        alertify.success("Record Added");
                    }
                    else {
                        alertify.error("Not a Human Error");
                    }
                    $('#buttonSave').prop("disabled", false).removeClass("ui-state-disabled");
                    $('#tbldailylog').DataTable().ajax.reload();
                    Common.closeGenericModal();
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
}

function success()
{ };