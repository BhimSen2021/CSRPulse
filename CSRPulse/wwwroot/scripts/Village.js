$(document).ready(function () {
    $('#StateId').change(function () {
        $.ajax({
            type: 'GET',
            dataType: 'JSON',
            url: '/Village/BindDistrict',
            data: { stateId: $('#StateId').val() },
            success: function (data) {
                var items = '<option disabled="" label="Select District" selected="" value=""></option>';
                $('#DistrictId').empty();

                $.each(data, function (key, value) {
                    items += "<option value='" + value.value + "'>" + value.text + "</option>"
                });
                $('#DistrictId').html(items);
            },
            error: function (responce) {
                commonMessage('error', 'error occurred on bind District dropdown.');
            }
        });
    });

    $('#DistrictId').change(function () {
        $.ajax({
            type: 'GET',
            dataType: 'JSON',
            url: '/Village/BindBlock',
            data: { stateId: $('#StateId').val(), districtId: $('#DistrictId').val() },
            success: function (data) {
                var items = '<option disabled="" label="Select Block" selected="" value=""></option>';
                $('#BlockId').empty();

                $.each(data, function (key, value) {
                    items += "<option value='" + value.value + "'>" + value.text + "</option>"
                });
                $('#BlockId').html(items);
            },
            error: function (responce) {
                commonMessage('error', 'error occurred on bind Block dropdown.');
            }
        });
    });

});