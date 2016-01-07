var customerInvoiceDataTable;
var varEndDate;
var varBeginDate;
$(document).ready(function () {
    customerInvoiceDataTable = $('#tblCustomer').dataTable({
        "ajax": "/CustomerInvoice/GetCustomerInvoice"
    });
    $('.loader-wrap').css('display', 'none');
});

//Delete Record From Databases
function DeleteCustomerInvoice(id) {
    alertify.confirm("Are you sure you want to delete this record?", function (e) {
        if (e) {
            $.ajax({
                url: "/CustomerInvoice/DeleteCustomerInvoice" + '/' + id,
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

function CreateCustomerPopup(id) {
    debugger;
    $("#InvoiceId").val(id);
    $('#buttonSave').prop("disabled", false).removeClass("ui-state-disabled");
    var modelHeading;
    if (id > 0) {
        modelHeading = "Update Customer Invoice";
    }
    else {
        modelHeading = "Create Customer Invoice";
    }
    Common.showGenericModal(modelHeading, '/CustomerInvoice/CreateCustomerInvoicePopup?id=' + id, success());
};
function CreateCustomerInvoice(id) {
    $('#buttonSave').prop("disabled", true).addClass("ui-state-disabled");
    //Validate Invoice Date
    var skipsave = '';
    var beginDate = $('#BeginDate').val();
    var endDate = $('#EndDate').val();
    var customerId = $('#CustomerId').val();
    $.ajax({
        url: '/CustomerInvoice/CheckInvoiceOverlap?customerId=' + customerId + '&beginDate=' + beginDate + '&endDate=' + endDate + '&inoviceId=' + id,
        type: 'GET',
        async: false,
        success: function (data) {
            if (data != 0) {
                skipsave = 'skipsave';
                $('#buttonSave').prop("disabled", false).removeClass("ui-state-disabled");
                alert('Invoice#' + data + ' for this period has already been generated. Please select different date');
                return false;
            }
        },
        error: function (err) {
            $('#buttonSave').prop("disabled", false).removeClass("ui-state-disabled");
            console.log(err);
        }
    });
    debugger;
    if (skipsave == '') {
        var dataToSend = $('#Customer-form').serialize();
        if ($('#Customer-form').valid()) {
            $.ajax({
                url: '/CustomerInvoice/CreateCustomerInvoice',
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
        else
        {
            $('#buttonSave').prop("disabled", false).removeClass("ui-state-disabled");
        }
    }
}
function PrintInvoice(invoiceId)
{
    var x = screen.width / 2 - 700 / 2;
    var y = screen.height / 2 - 450 / 2;
    window.open('../CustomerInvoice/PrintInvoice?invoiceId=' + invoiceId, 'CustomerInvoice', 'width=800,height=450,left='+x+',top='+y)
}
function ResetHoursandAmount(type) {
    debugger;
    var invoiceId = $('#InvoiceId').val();
    var beginDate = $('#BeginDate').val();
    var endDate = $('#EndDate').val();
    var customerId = $('#CustomerId').val();
    if (customerId == 0) {
        alert('Please select Customer');
        return false;
    }
    $.ajax({
        url: '/CustomerInvoice/GetCustomerHoursAndAmount?customerId=' + customerId + '&type=' + type + '&beginDate=' + beginDate + '&endDate=' + endDate + '&inoviceId=' + invoiceId,
        type: 'GET',
        async:false,
        success: function (data) {
            debugger;
            if (data) {
                $('#BeginDate').val(data.beginDate);
                $('#EndDate').val(data.endDate);
                if (data.totalHours != 0) {
                    $('#TotalHours').val(data.totalHours);
                }
                if (data.amount != 0) {
                    $('#Amount').val(data.amount);
                }
                $('#HourlyRate').val(data.hourlyRate);
            }
        },
        error: function (err) {
            console.log(err);
        }
    });
    CalculateAmount();
}
$(function () {
    $(document).on('change', '#BeginDate', function () {
        if (Date.parse($('#BeginDate').val()) > Date.parse($('#EndDate').val())) {
            $('#BeginDate').val(varBeginDate);
            alert("Begin Date cannot be less than End Date")
            return false;
        }
        varBeginDate = $('#BeginDate').val();
        ResetHoursandAmount('oldCustomer');
    });
    $(document).on('change', '#EndDate', function () {
        if (Date.parse($('#BeginDate').val()) > Date.parse($('#EndDate').val())) {
            $('#EndDate').val(varEndDate);
            alert("Begin Date cannot be greater than End Date")
            return false;
        }
        varEndDate = $('#EndDate').val();
        ResetHoursandAmount('oldCustomer');
    });
});
function GenerateCustomerInvoiceReport() {
    var CustomerId = $('#CustomerId').val();
    if (CustomerId == '')
        CustomerId = 0;

    var startDate = $('#StartDate').val();
    var endDate = $('#EndDate').val();
    var InvoiceType = '';//$('#InvoiceType').val();
    var x = screen.width / 2 - 700 / 2;
    var y = screen.height / 2 - 450 / 2;
    window.open('../RDLReports/ReportPages/CustomerInvoiceReports.aspx?startDate=' + startDate + '&endDate=' + endDate + '&customerid=' + CustomerId + '&invoiceType=' + InvoiceType, 'CustomerInvoiceReport', 'directories=no,titlebar=no,toolbar=no,location=no,status=no,menubar=no,scrollbars=no,resizable=no,width=695,height=550,left=' + x + ',top=' + (y - 80))
}
function CalculateAmount() {
    var calculatedAmount = parseFloat($('#HourlyRate').val()) * parseFloat($('#TotalHours').val());
    debugger;
    if (isNaN(calculatedAmount)) {
        calculatedAmount = 0;
    }
    $('#Amount').val(Math.round(calculatedAmount * 100) / 100)
}

function success()
{ };
