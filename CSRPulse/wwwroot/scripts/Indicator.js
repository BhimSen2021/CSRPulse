$(document).ready(function () {
    $('#ThemeId').change(function () {
        BindSubTheme($('#ThemeId').val());
        $.ajax({
            type: 'GET',
            dataType: 'JSON',
            url: '/Indicator/BindActivityDropdown',
            data: { themeId: $('#ThemeId').val() },
            success: function (data) {
                var items = '<option disabled="" label="Select Activity" selected="" value=""></option>';
                $('#ActivityId').empty();

                $.each(data, function (i, activity) {
                    items += "<option value='" + activity.value + "'>" + activity.text + "</option>"
                });
                $('#ActivityId').html(items);
            },
            error: function (responce) {
                commonMessage('error', 'error occurred on bind Activity dropdown.');
            }
        });
    });

    $('#ActivityId').change(function () {
        $.ajax({
            type: 'GET',
            dataType: 'JSON',
            url: '/Indicator/BindSubActivityDropdown',
            data: { themeId: $('#ThemeId').val(), activityId: $('#ActivityId').val() },
            success: function (data) {
                var items = '<option label="Select SubActivity" selected="" value=""></option>';
                $('#SubActivityId').empty();

                $.each(data, function (i, item) {
                    items += "<option value='" + item.value + "'>" + item.text + "</option>"
                });
                $('#SubActivityId').html(items);
            },
            error: function (responce) {
                commonMessage('error', 'error occurred on bind SubActivity dropdown.');
            }
        });
    });

    $('#ResponseType').change(function () {       
        let rType = $('#ResponseType').val();
        if (rType == 1 || rType == 4) {
            $('#IsCumulative').removeAttr('disabled');            
        }
        else {
            $('#IsCumulative').attr('disabled', 'disabled');
        }
    });

});

function BindSubTheme(themeId) {
    $.ajax({
        type: 'GET',
        dataType: 'JSON',
        url: '/Indicator/BindSubThemeDropdown',
        data: { themeId: $('#ThemeId').val() },
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