var customerDataTable;

$(document).ready(function () {
    customerPaymentDataTable = $('#tblCustomer').dataTable({
        "ajax": "/CustomerPayment/GetCustomersPayment"
    });
    $('.loader-wrap').css('display', 'none');
});

function CreateUpdateCustomerPaymentPopup(id) {
    $("#customerPaymentId").val(id);
    $('#buttonSave').prop("disabled", false).removeClass("ui-state-disabled");
    var modelHeading;
    if (id > 0) {
        modelHeading = "Update Customer Payment"; 
    }
    else {
        modelHeading = "Create Customer Payment";
    }
    Common.showGenericModal(modelHeading, '/CustomerPayment/CreateUpdateCustomerPaymentPopup?id=' + id, success());
};

//Delete Record From Databases
function DeleteCustomerPayment(id) {
    alertify.confirm("Are you sure you want to delete this record?", function (e) {
        if (e) {
            $.ajax({
                url: "/CustomerPayment/DeleteCustomerPayment" + '/' + id,
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

function CreateUpdateCustomerPayment(id) {
    $('#buttonSave').prop("disabled", true).addClass("ui-state-disabled");
    $('#customerPaymentId').val(id);
    var dataToSend = $('#CustomerPayment-form').serialize();
    if ($('#CustomerPayment-form').valid()) {
        $.ajax({
            url: '/CustomerPayment/CreateUpdateCustomerPayment',
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
    else {
        $('#buttonSave').prop("disabled", false).removeClass("ui-state-disabled");
    }
}

//Delete Record From Databases
function GetInvoiceAmount() {
    var id = $('#InvoiceId').val();
    $.ajax({
        url: "/CustomerPayment/GetInvoiceAmount" + '/' + id,
        type: 'GET',
        dataType: 'json',
        success: function (data) {
            $('#Amount').val(data.Amount);
        },
        error: function (statusText) {
            var error = JSON.parse(statusText.responseText)
            console.log(error.exceptionMessage);
        }
    });
}
function GetCustomerInvoices(selectedValue)
{
    
    var id = $('#CustomerId').val();
    $.ajax({
        url: "/CustomerPayment/GetCustomerInvoice" + '/' + id,
        type: 'GET',
        dataType: 'json',
        async:false,
        success: function (data) {
            var ddl = $('#InvoiceId');
            ddl.empty();
            ddl.append(
                    $('<option/>', {
                        value: ''
                    }).html('Select Invoice')
                );
            $(data).each(function () {
                ddl.append(
                    $('<option/>', {
                        value: this.InvoiceId
                    }).html(this.InvoiceNo)
                );
            });
        },
        error: function (statusText) {
            var error = JSON.parse(statusText.responseText)
            console.log(error.exceptionMessage);
        }        
    });
    if (selectedValue != 0) {
        $('#InvoiceId').val(selectedValue);
    }
    else
    {
        $('#Amount').val('');
    }
}
function success()
{ };

