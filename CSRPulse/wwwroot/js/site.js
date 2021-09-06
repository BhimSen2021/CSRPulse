function onlyNumber(evt) {

    // Only ASCII character in that range allowed
    var ASCIICode = (evt.which) ? evt.which : evt.keyCode
    if (ASCIICode > 31 && (ASCIICode < 48 || ASCIICode > 57))
        return false;
    return true;
}
function DecimalNumberOnly(evt) {
    evt = (evt) ? evt : window.event;
    var charCode = (evt.which) ? evt.which : evt.keyCode;
    if (charCode == 8 || charCode == 37) {
        return true;
    } else if (charCode == 46 && evt.path[0].value.indexOf('.') != -1) {
        return false;
    } else if (charCode > 31 && charCode != 46 && charCode != 44 && (charCode < 48 || charCode > 57)) {
        return false;
    }
    return true;
}

function CFormat(id) {
    let MoneyVal = $('#' + id).val();
    if (MoneyVal != "" && MoneyVal != null) {
        MoneyVal = MoneyVal.toString();
        if (MoneyVal.trim() != "") {
            var val = MoneyVal.replace(/,/g, '');
            if (val >= 1000) {
                var values = [];
                values = val.split('.');
                var firstval = values[0];
                var len = firstval.length;
                var last3 = firstval.substr(len - 3, len);
                var beforelast3 = firstval.substr(0, len - 3);
                var lastval = values.length > 1 ? values[1] : "";

                var splitString = beforelast3.split("");
                var reverseArray = splitString.reverse();
                var rev = reverseArray.join("");

                var result = "";
                for (var i = 0; i < rev.length; i++) {
                    var c = rev[i];
                    if (i % 2 != 0)
                        result += c;
                    else
                        result += "," + c;
                }

                var splitString2 = result.split("");
                var reverseArray2 = splitString2.reverse();
                var res = reverseArray2.join("");
                MoneyVal = res + last3 + (lastval == "" ? ".00" : ("." + lastval));
            }
            else {
                if (MoneyVal.includes("."))
                    MoneyVal = MoneyVal;
                else
                    MoneyVal = MoneyVal + ".00";
            }
        }

    }
    $('#' + id).val(MoneyVal);
}

function MakeMoneyFormat(MoneyVal) {

    if (MoneyVal != "" && MoneyVal != null) {
        MoneyVal = MoneyVal.toString();
        if (MoneyVal.trim() != "") {
            var val = MoneyVal.replace(/,/g, '');
            if (val >= 1000) {
                var values = [];
                values = val.split('.');
                var firstval = values[0];
                var len = firstval.length;
                var last3 = firstval.substr(len - 3, len);
                var beforelast3 = firstval.substr(0, len - 3);
                var lastval = values.length > 1 ? values[1] : "";

                var splitString = beforelast3.split("");
                var reverseArray = splitString.reverse();
                var rev = reverseArray.join("");

                var result = "";
                for (var i = 0; i < rev.length; i++) {
                    var c = rev[i];
                    if (i % 2 != 0)
                        result += c;
                    else
                        result += "," + c;
                }

                var splitString2 = result.split("");
                var reverseArray2 = splitString2.reverse();
                var res = reverseArray2.join("");
                return res = res + last3 + (lastval == "" ? ".00" : ("." + lastval));
            }
            else {
                if (MoneyVal.includes("."))
                    return MoneyVal;
                else
                    return MoneyVal + ".00";
            }
        }
        else
            return MoneyVal;

    }
    else
        return MoneyVal;
}

function RemoveComma(MoneyVal) {
    if (MoneyVal != "" && MoneyVal != null) {
        var val = MoneyVal.replace(/,/g, '');
        return val;
    }
    else
        return MoneyVal;
}


$(function () {
    // Create the close button
    var closebtn = $('<button/>', {
        type: "button",
        text: 'x',
        id: 'close-preview',
        style: 'font-size: initial;',
    });
    closebtn.attr("class", "close pull-right");
    // Set the popover default content
    $('.image-preview').popover({
        trigger: 'manual',
        html: true,
        title: "<strong>Preview</strong>" + $(closebtn)[0].outerHTML,
        content: "There's no image",
        placement: 'bottom'
    });
    // Clear event
    $('.image-preview-clear').click(function () {
        $('.image-preview').attr("data-content", "").popover('hide');
        $('.image-preview-filename').val("");
        $('.image-preview-clear').hide();
        $('.image-preview-input input:file').val("");
        $(".image-preview-input-title").text("Browse");
        $('.validate-data').attr('disabled', true);
    });
    // Create the preview image
    $(".image-preview-input input:file").change(function () {
        var file = this.files[0];
        var reader = new FileReader();
        // Set preview image into the popover data-content
        reader.onload = function (e) {
            $(".image-preview-input-title").text("Change");
            $(".image-preview-clear").show();
            $(".image-preview-filename").val(file.name);
        }
        reader.readAsDataURL(file);
    });

    $(".image-preview-input-multiple input:file").change(function () {
        var files = $("#file").get(0).files;
        var totalFiles = files.length;
        var fileName = "";
        for (var i = 0; i < files.length; i++) {
            fileName += files[i].name + ",";
        }
        $(".image-preview-filename").val(fileName.replace(/,\s*$/, ""));
        $(".image-preview-input-title").text("Change");
        $(".image-preview-clear").show();
    });
});

function Filevalidation(obj) {
    var fileSize = obj.files[0].size;
    var mSize = $('#' + obj.id.replace('DocumentFile', 'DocumentMaxSize')).val();
    var fileExt = $('#' + obj.id).val().split('.').pop().toLowerCase();
    var validExt = "," + $('#' + obj.id.replace('DocumentFile', 'DocumentType')).val();

    if ((fileSize / 1048576) > mSize) {
        commonMessage('error', 'Maximum file size exceed, The file size can not exceed: ' + mSize + "MB");
        $('#' + obj.id).val('');
        return false;
    }

    else if (validExt.indexOf(fileExt.toString()) < 1) {
        commonMessage('error', 'Only formats are allowed: ' + validExt);
        $('#' + obj.id).val('');
        return false;
    }
    else
        return true;
}