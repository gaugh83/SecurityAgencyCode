var guardPaymentDataTable;
var varEndDate;
var varBeginDate;
$(document).ready(function () {
    guardPaymentDataTable = $('#tblGuard').dataTable({
        "ajax": "/GuardPayment/GetGuardPayment",
    });
    $('.loader-wrap').css('display', 'none');
});

function CreateUpdateGuardPaymentPopup(id) {
    $("#guardPaymentId").val(id);
    $('#buttonSave').prop("disabled", false).removeClass("ui-state-disabled");
    var modelHeading;
    if (id > 0) {
        modelHeading = "Update Guard Payment"; 
    }
    else {
        modelHeading = "Create Guard Payment";
    }
    Common.showGenericModal(modelHeading, '/GuardPayment/CreateUpdateGuardPaymentPopup?id=' + id, success());

};

//Delete Record From Databases
function DeleteGuardPayment(id) {
    alertify.confirm("Are you sure you want to delete this record?", function (e) {
        if (e) {
            $.ajax({
                url: "/GuardPayment/DeleteGuardPayment" + '/' + id,
                type: 'POST',
                dataType: 'json',
                success: function (data) {
                    if (data > 0) {
                        alertify.success("Record Deleted");
                    }
                    else {
                        alertify.error("Not a Human Error");
                    }
                    $('#tblGuard').DataTable().ajax.reload();
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

function CreateUpdateGuardPayment(id) {
    $('#buttonSave').prop("disabled", true).addClass("ui-state-disabled");
    $('#guardPaymentId').val(id);
    var dataToSend = $('#GuardPayment-form').serialize();
    if ($('#GuardPayment-form').valid()) {
        $.ajax({
            url: '/GuardPayment/CreateUpdateGuardPayment',
            data: dataToSend,
            type: 'Post',
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
                $('#tblGuard').DataTable().ajax.reload();
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

//Delete Record From Databases
function GetPaymentAmount() {
    var GuardId = $('#GuardId').val();
    if (GuardId == '')
    {
        alert('Please select Guard');
        return false;
    }
    var StartDate = $('#StartDate').val();
    var EndDate = $('#EndDate').val();
    $.ajax({
        url: "/GuardPayment/GetPaymentAmount?startDate=" + StartDate + "&endDate=" + EndDate + "&guardId=" + GuardId,
        type: 'GET',
        dataType: 'json',
        success: function (data) {
            debugger;
            $('#Amount').val(data.Amount);
            $('#StartDate').val(data.StartDate);
            $('#EndDate').val(data.EndDate);
            $('#HourlyRate').val(data.HourlyRate);
            $('#TotalHours').val(data.TotalHours);
        },
        error: function (statusText) {
            var error = JSON.parse(statusText.responseText)
            console.log(error.exceptionMessage);
        }
    });
}

$(function () {
    $(document).on('change', '#StartDate', function () {
        if (Date.parse($('#StartDate').val()) > Date.parse($('#EndDate').val())) {
            $('#StartDate').val(varBeginDate);
            alert("Start Date cannot be less than End Date")
            return false;
        }
        varBeginDate = $('#StartDate').val();
        GetPaymentAmount();
    });
    $(document).on('change', '#EndDate', function () {
        if (Date.parse($('#StartDate').val()) > Date.parse($('#EndDate').val())) {
            $('#EndDate').val(varEndDate);
            alert("End Date cannot be greater than Start Date")
            return false;
        }
        varEndDate = $('#EndDate').val();
        GetPaymentAmount();
    });
});
function CalculateAmount() {
    var calculatedAmount = parseFloat($('#HourlyRate').val()) * parseFloat($('#TotalHours').val());
    debugger;
    if (isNaN(calculatedAmount))
    {
        calculatedAmount = 0;
    }
    $('#Amount').val(calculatedAmount)
}

function GenerateGuardPaymentReport() {
    debugger;
    var guardid = $('#GuardId').val();
    var type = $('#Type').val();
    if (guardid == '')
    {
        guardid = 0;
    }
    var x = screen.width / 2 - 700 / 2;
    var y = screen.height / 2 - 450 / 2;
    window.open('../RDLReports/ReportPages/GuardPaymentReports.aspx?guardid=' + guardid + '&type=' + type, 'GuardPaymentReport', 'directories=no,titlebar=no,toolbar=no,location=no,status=no,menubar=no,scrollbars=no,resizable=no,width=510,height=550,left=' + x + ',top=' + (y - 80))
}
function success()
{ };

