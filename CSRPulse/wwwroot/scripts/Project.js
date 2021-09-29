$(document).ready(function () {
    StartDateValidation();

    $(".CollapseAll").click(function () {
        $('.tree2 .branch').each(function () {
            var icon = $(this).children('i:first');
            icon.toggleClass('mdi mdi-arrow-down-drop-circle mdi mdi-arrow-right-drop-circle');
            $(this).children().children().toggle();
        });
    });
});


function BindSubTheme() {
    let themeId = $('#ThemeId').val();
    $.ajax({
        type: 'GET',
        dataType: 'JSON',
        url: '/Project/BindSubThemeDropdown',
        data: { themeId: themeId },
        success: function (data) {
            var items = '<option label="Select SubTheme" selected="" value=""></option>';
            $('#SubThemeId').empty();

            $.each(data, function (i, item) {
                items += "<option value='" + item.value + "'>" + item.text + "</option>"
            });
            $('#SubThemeId').html(items);
        },
        error: function (responce) {
            commonMessage('error', 'error occurred on bind SubTheme dropdown.');
        }
    });
}

function StartDateValidation() {
    $("#StartDate").attr("min", new Date().toJSON().slice(0, 10));
    $("#EndDate").attr("min", new Date().toJSON().slice(0, 10));
}
function SetEndDateMinVal() {
    $("#EndDate").attr("min", $("#StartDate").val());
}

function CalcOtherSource() {
    totalBudget = parseFloat(RemoveComma($('#TotalBudget').val()));
    trustContribution = parseFloat(RemoveComma($('#TrustContribution').val()));

    if (totalBudget >= trustContribution) {
        otherSource = totalBudget - trustContribution;
        if (otherSource > 0)
            $('#btnAddOS').attr("disabled", false);
        else
            $('#btnAddOS').attr("disabled", true);

        $('#OtherSource').val(MakeMoneyFormat(otherSource));

        if (trustContribution > 0)
            $('#btnAddIS').attr("disabled", false);
        else
            $('#btnAddIS').attr("disabled", true);

        // Total Project Support from Internal Percentage
        if (trustContribution > 0) {
            $('#lblTrustPer').text(((trustContribution * 100) / totalBudget).toFixed(2) + " %");
            $('#lblRemaning_IS').text("Remaining amount (₹): " + MakeMoneyFormat(trustContribution));
        }
        else
            $('#lblTrustPer').text("0.00 %");

        // Other Sources Contribution Percentage
        if (otherSource > 0) {
            $('#lblOtherPer').text(((otherSource * 100) / totalBudget).toFixed(2) + " %");
            $('#lblRemaning_OS').text("Remaining amount (₹): " + MakeMoneyFormat(otherSource));
        }
        else
            $('#lblOtherPer').text("0.00 %");
    }
    else {
        $('#TrustContribution').val('');
        commonMessage('warning', 'Total Project Support from Internal can not be greater than Total Budget.');
    }
}

function checkAmountIS(id) {
    let subTotal = 0;
    let rowCount = $('#tblInternalSource > tbody  > tr').length;
    let TrustContribution = parseFloat(RemoveComma($('#TrustContribution').val())).toFixed(2);
    let BudgetValue = parseFloat(RemoveComma($('#TotalBudget').val())).toFixed(2);

    for (var i = 0; i < rowCount; i++) {
        let amount = RemoveComma($('#ProjectInternalSource_' + i + '__Amount').val());
        var Percent = ((amount * 100) / BudgetValue).toFixed(2);
        $('#lblPerIS_' + i).text(Percent + "%");
        if (amount == '')
            subTotal = parseFloat(subTotal).toFixed(2) + 0;
        else
            subTotal = parseFloat(subTotal) + parseFloat(amount)
    }
    if (subTotal > TrustContribution) {
        $('#' + id).val('');
        commonMessage('warning', 'Total breakup amount should not be greater then, Total Project Support from Internal.');
    }
    else
        $('#lblRemaning_IS').text('Remaining amount (₹): ' + MakeMoneyFormat((TrustContribution - subTotal)));
}

function checkAmountOS(id) {
    let subTotal = 0;
    let rowCount = $('#tblOtherSource > tbody  > tr').length;
    let OtherSource = parseFloat(RemoveComma($('#OtherSource').val())).toFixed(2);
    let BudgetValue = parseFloat(RemoveComma($('#TotalBudget').val())).toFixed(2);

    for (var i = 0; i < rowCount; i++) {
        let amount = RemoveComma($('#ProjectOtherSource_' + i + '__Amount').val());
        var Percent = ((amount * 100) / BudgetValue).toFixed(2);
        $('#lblPerOS_' + i).text(Percent + "%");
        if (amount == '')
            subTotal = parseFloat(subTotal).toFixed(2) + 0;
        else
            subTotal = parseFloat(subTotal) + parseFloat(amount)
    }
    if (subTotal > OtherSource) {
        $('#' + id).val('');
        commonMessage('warning', 'Total breakup amount should not be greater then, Total Other source amount.');
    }
    else
        $('#lblRemaning_OS').text('Remaining amount (₹): ' + MakeMoneyFormat((OtherSource - subTotal)));
}

function SetLocation(lLevel, ltype) {   
    var allVals = [];
    if (lLevel == '1') {
        $(".Lavel1").each(function () {
            if ($(this).is(':checked')) {
                allVals.push($(this).val());
            }
        });
    }
    else if (lLevel == '2') {
        $(".Lavel2").parent().parent().find("li .Lavel2").each(function () {
            if ($(this).is(':checked')) {
                allVals.push($(this).parent().parent().parent().find(".Lavel1").val() + ':' + $(this).val());
            }
        });
    }
    else if (lLevel == '3') {
        $(".Lavel3").parent().parent().parent().parent().find("li .Lavel3").each(function () {
            if ($(this).is(':checked')) {
                allVals.push($(this).parent().parent().parent().parent().parent().find(".Lavel1").val() + ':' + $(this).parent().parent().parent().find(".Lavel2").val() + ':' + $(this).val());
            }
        });
    }
    else if (lLevel == '4') {
        $(".Lavel3").parent().parent().parent().parent().parent().parent().find("li .Lavel4").each(function () {
            if ($(this).is(':checked')) {
                allVals.push($(this).parent().parent().parent().parent().parent().parent().parent().find(".Lavel1").val() + ':' + $(this).parent().parent().parent().parent().parent().find(".Lavel2").val() + ':' + $(this).parent().parent().parent().find(".Lavel3").val() + ':' + $(this).val());
            }
        });
    }
    let strLocations = allVals.toString();
    // Add location for location
    if (ltype == 1) {
        if (strLocations.length > 0) {
            $('#hdnLocationIds').val(strLocations);
            commonMessage('success', 'Selected location added.');
            $('#divalert').css('display', 'none');
            $("#AddLocationModal").modal('hide');
        }
        else {
            $('#divalert').css('display', 'block');
            $('#hdnLocationIds').val('');
        }
    }
}


function SaveLocation(lLevel, ltype) {
    if (ltype == 2)
        lLevel = $('#LocationLavel :selected').val();
    var allVals = [];
    if (lLevel == '1') {
        $(".dLavel1").each(function () {
            if ($(this).is(':checked')) {
                allVals.push($(this).val());
            }
        });
    }
    else if (lLevel == '2') {
        $(".dLavel2").parent().parent().find("li .dLavel2").each(function () {
            if ($(this).is(':checked')) {
                allVals.push($(this).parent().parent().parent().find(".dLavel1").val() + ':' + $(this).val());
            }
        });
    }
    else if (lLevel == '3') {
        $(".dLavel3").parent().parent().parent().parent().find("li .dLavel3").each(function () {
            if ($(this).is(':checked')) {
                allVals.push($(this).parent().parent().parent().parent().parent().find(".dLavel1").val() + ':' + $(this).parent().parent().parent().find(".dLavel2").val() + ':' + $(this).val());
            }
        });
    }
    else if (lLevel == '4') {
        $(".dLavel3").parent().parent().parent().parent().parent().parent().find("li .dLavel4").each(function () {
            if ($(this).is(':checked')) {
                allVals.push($(this).parent().parent().parent().parent().parent().parent().parent().find(".dLavel1").val() + ':' + $(this).parent().parent().parent().parent().parent().find(".dLavel2").val() + ':' + $(this).parent().parent().parent().find(".dLavel3").val() + ':' + $(this).val());
            }
        });
    }
    let strLocations = allVals.toString();
    
    //Add location for location details
        if (strLocations.length > 0) {
            $('#divLocationDetailAlert').css('display', 'none');
            $("#AddLocationDetailModal").modal('hide');
            var projectId = $('#ProjectId').val();
            $.ajax({
                type: 'GET',
                /*dataType: 'JSON',*/
                dataType: 'html',
                url: '/Project/SaveLocationDetail',
                data: { projectId: projectId, lLevel: lLevel, locationIds: strLocations },
                success: function (data) {
                    $("#div-location-detail-grid").html(data);
                    commonMessage('success', 'Location detail added successfully.');
                },
                error: function (responce) {
                    commonMessage('error', 'error occurred on save location details.');
                }
            });
        }
        else {
            $('#divLocationDetailAlert').css('display', 'block');
        }
}

