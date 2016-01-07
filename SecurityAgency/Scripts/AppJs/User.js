var userDataTable;

$(document).ready(function () {
    userDataTable = $('#tblUser').dataTable({
        "ajax": "/User/GetUsers"
    });
    $('.loader-wrap').css('display', 'none');
});


function CreateUpdateUserPopup(id) {
    $("#userId").val(id);
    var modelHeading;
    if (id > 0) {
        modelHeading = "Update User";
    }
    else {
        modelHeading = "Create User";
    }
    Common.showGenericModal(modelHeading, '/User/CreateUpdateUserPopup?id=' + id, success());

};
function PermissionUserPopup(id) {
    $("#userId").val(id);
    var modelHeading;
    if (id > 0) {
        modelHeading = "Create Permission";
    }
    else {
        modelHeading = "Update Permission";
    }
    Common.showGenericModal(modelHeading, '/User/PermissionUserPopup?id=' + id, success());

};

//Delete Record From Databases
function DeleteUser(id) {
    if (id != Role.Admin) {
        alertify.confirm("Are you sure you want to delete this record?", function (e) {
            if (e) {
                $.ajax({
                    url: "/User/DeleteUser" + '/' + id,
                    type: 'POST',
                    dataType: 'json',
                    success: function (data) {
                        if (data > 0) {
                            alertify.success("Record Deleted");
                        }
                        else {
                            alertify.error("Not a Human Error");
                        }
                        $('#tblUser').DataTable().ajax.reload();
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
    else
    {
        alertify.error("Admin User cannot be deleted");
    }
}


function CreateUpdateUser(id) {
    debugger;
    $('#userId').val(id);
    if ($('#User-form').valid()) {
        var dataToSend = $('#User-form').serialize();
        $.ajax({
            url: '/User/CreateUpdateUser',
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
                $('#tblUser').DataTable().ajax.reload();
                Common.closeGenericModal();
            },
            error: function (err) {
                console.log(err);
            }
        });
    }
}
function GetPermissionForRoles() {
    debugger;
    $('.checkboxPermission').attr("checked", false);
    var RoleId = $('#RoleId').val();
    $.ajax({
        url: '/User/GetPermissionForRoles?RoleId=' + RoleId,
        type: 'GET',
        async: true,
        success: function (data) {
            debugger;
            if (data != 0) {
                $.each(data, function (i, item) {
                    $('#chk_' + item.PermissionId).attr("checked", true);
                });
            }
        },
        error: function (err) {
            console.log(err);
        }
    });

}
function validateRole()
{
    if ($('#Role').val() == '') {
        alert('Please Fill Role');
        return false;
    }
}
function success()
{ };