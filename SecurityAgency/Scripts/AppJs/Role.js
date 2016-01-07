var userDataTable;

$(document).ready(function () {
    userDataTable = $('#tblUser').dataTable({
        "ajax": "/User/GetRoles"
    });
    $('.loader-wrap').css('display', 'none');
});


function CreateUpdateRolePopup(id) {
    //$("#userId").val(id);
    //var modelHeading;
    //if (id > 0) {
    //    modelHeading = "Update User";
    //}
    //else {
    //    modelHeading = "Create User";
    //}
    //Common.showGenericModal(modelHeading, '/User/CreateUpdateUserPopup?id=' + id, success());
    window.location = "/User/RolePermissions?id=" + id;

};

//Delete Record From Databases
function DeleteRole(id) {
    alertify.confirm("Are you sure you want to delete this record?", function (e) {
        if (e) {
            $.ajax({
                url: "/User/DeleteRole" + '/' + id,
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

function success()
{ };