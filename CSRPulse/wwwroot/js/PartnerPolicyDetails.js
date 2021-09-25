function SaveDetails() {
    if ($("#btnSave").val() == "SavePolicyDetails") {
        //$("#div-partnerpolicymodule-grid").html(data.htmlData);
        alert('wrong');

        var policyDetails = {
            PolicyId: $("#PolicyId").val, PolicyModuleId: $("#PolicyModuleId").val,
            IsApproved: $("#IsApproved").val(), Impletedsince: $("#Impletedsince").val(), IsLastUpdatedOn: $("#IsLastUpdatedOn").val()
        };
        $.ajax({
            url: 'Partner/SavePolicyDetails',
            type: 'POST',
            data: JSON.stringify({ policyDetails }),
            success: function () {
                alert('Record saved !!!');
            },
            error: function () {
                alert('Record not saved !!!');
            }
        });
    }
}