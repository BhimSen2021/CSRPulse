$(document).ready(function () {
    $('#ThemeId').change(function () {
        debugger;
        $.ajax({
            type: 'GET',
            dataType: 'JSON',
            url: '/SubActivity/BindActivityDropdown',
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
                commonMessage('error', 'error occurred on bind subActivity dropdown.');
            }
        });
    });
});
