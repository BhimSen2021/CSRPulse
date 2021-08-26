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

$('#ThemeId').change(function () {
    let ThemeId = $('#ThemeId').val();
    if (ThemeId != '') {
        BindSubTheme(ThemeId);
    }
});

function BindSubTheme(themeId) {
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
    totalBudget = parseFloat($('#TotalBudget').val());
    trustContribution = parseFloat($('#TrustContribution').val());

    if (totalBudget >= trustContribution) {
        otherSource = totalBudget - trustContribution;
        if (otherSource > 0)
            $('#btnAddOS').attr("disabled", false);
        else
            $('#btnAddOS').attr("disabled", true);

        $('#OtherSource').val(otherSource);

        if (trustContribution > 0)
            $('#btnAddIS').attr("disabled", false);
        else
            $('#btnAddIS').attr("disabled", true);

        // Total Project Support from Internal Percentage
        if (trustContribution > 0) {
            $('#lblTrustPer').text(((trustContribution * 100) / totalBudget).toFixed(2) + " %");
            $('#lblRemaning_IS').text("Remaining amount (₹): " + trustContribution);
        }
        else
            $('#lblTrustPer').text("0.00 %");

        // Other Sources Contribution Percentage
        if (otherSource > 0) {
            $('#lblOtherPer').text(((otherSource * 100) / totalBudget).toFixed(2) + " %");
            $('#lblRemaning_OS').text("Remaining amount (₹): " + otherSource);
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
    let TrustContribution = parseFloat($('#TrustContribution').val()).toFixed(2);
    let BudgetValue = parseFloat($('#TotalBudget').val()).toFixed(2);

    for (var i = 0; i < rowCount; i++) {
        let amount = $('#ProjectInternalSource_' + i + '__Amount').val();
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
        $('#lblRemaning_IS').text('Remaining amount (₹): ' + (TrustContribution - subTotal));
}

function checkAmountOS(id) {
    let subTotal = 0;
    let rowCount = $('#tblOtherSource > tbody  > tr').length;
    let OtherSource = parseFloat($('#OtherSource').val()).toFixed(2);
    let BudgetValue = parseFloat($('#TotalBudget').val()).toFixed(2);

    for (var i = 0; i < rowCount; i++) {
        let amount = $('#ProjectOtherSource_' + i + '__Amount').val();
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
        $('#lblRemaning_OS').text('Remaining amount (₹): ' + (OtherSource - subTotal));
}

function SetLocation(lLevel) {
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
