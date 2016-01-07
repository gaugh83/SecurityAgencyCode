var guardDataTable;

$(document).ready(function () {
    guardDataTable = $('#tblGuard').dataTable({
        "ajax": "/Guard/GetGuards"
    });
    $('.loader-wrap').css('display', 'none');
});

function CreateUpdateGuardPopup(id) {
    $("#guardId").val(id);
    DuplicateSSN = false;
    var modelHeading;
    if (id > 0) {
        modelHeading = "Update Guard";
    }
    else {
        modelHeading = "Create Guard";
    }
    Common.showGenericModal(modelHeading, '/Guard/CreateUpdateGuardPopup?id=' + id, success());

};

//Delete Record From Databases
function DeleteGuard(id) {
    alertify.confirm("Are you sure you want to delete this record?", function (e) {
        if (e) {
            $.ajax({
                url: "DeleteGuard" + '/' + id,
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

var DuplicateSSN = false;
function CreateUpdateGuard(id) {
    $('#buttonSave').prop("disabled", true).addClass("ui-state-disabled");
    if (!DuplicateSSN) {
        $('#guardId').val(id);
        if ($('#Guard-form').valid()) {
            var dataToSend = $('#Guard-form').serialize();
            $.ajax({
                url: '/Guard/CreateUpdateGuard',
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
        else
        {
            $('#buttonSave').prop("disabled", false).removeClass("ui-state-disabled");
        }
    }
    else {
        $('#buttonSave').prop("disabled", false).removeClass("ui-state-disabled");
        alert('SSN already in use');        
    }
}

function GenerateGuardReport() {
    debugger;
    var startDate = $('#StartDate').val();
    var endDate = $('#EndDate').val();
    var x = screen.width / 2 - 700 / 2;
    var y = screen.height / 2 - 450 / 2;
    window.open('../RDLReports/ReportPages/GuardReports.aspx?startDate=' + startDate + '&endDate=' + endDate, 'GuardReport', 'directories=no,titlebar=no,toolbar=no,location=no,status=no,menubar=no,scrollbars=no,resizable=no,width=690,height=550,left=' + x + ',top=' + (y - 80))
}
function validateSSN(guardId) {
    $('#buttonSave').prop("disabled", false).removeClass("ui-state-disabled");
    var ssn = $("#SSN").val();
    if (ssn != '') {
        $.ajax({
            url: "/Guard/validateGuardSSN?guardId=" + guardId + "&SSN=" + ssn,
            type: 'POST',
            dataType: 'json',
            async:false,
            success: function (data) {
                if (data) {
                    DuplicateSSN = true;
                }
                else {
                    DuplicateSSN = false;
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