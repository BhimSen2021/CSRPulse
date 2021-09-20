$(document)
    .ajaxStart(function () {
        $('#AjaxLoader').show();
    })
    .ajaxStop(function () {
        $('#AjaxLoader').hide();
    })
    .ajaxComplete(function () {
        $('#AjaxLoader').hide();
    });

$(document).ajaxError(function (xhr, props) {

    if (props.status === 308) {        
        location.reload();
    } 
    $('#AjaxLoader').modal('hide');
});