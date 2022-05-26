// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function getErrorMessage(errors) {
    if (errors == null || errors.length <= 0)
        return "";
    var msg = ""; //"<ul>";
    for (var i = 0; i < errors.length; i++)
        //msg += "<li>" + errors[i] + "</li>";
        msg += errors[i] + "<br/>";
    //msg += "</ul>";
    return msg;
}

function weFillFormErrors(options) {
    if (!options.formSelector)
        console.error('weFillFormErrors: "formSelector" is missing.');
    if (options.errorMap) {
        $(options.formSelector + " div.invalid-feedback").detach();
        $(options.formSelector + ' *').removeClass('is-invalid');
        var keys = Object.keys(options.errorMap);
        for (var i = 0; i < keys.length; i++) {
            var msg = options.errorMap[keys[i]];
            if (options.msgFormatCallback) {
                msg = options.msgFormatCallback(msg);
            }
            var selector = options.formSelector + ' .form-control[name=' + keys[i] + ']';
            $(selector).addClass('is-invalid');
            $(selector).closest('div').addClass('is-invalid');

            var html = '<div class="invalid-feedback d-block"><i class="fa fa-exclamation-circle"></i> ' + msg + '</div>';

            var closestedSelector = 'div';
            $(selector).closest(closestedSelector).append(html);

            var selector2 = options.formSelector + ' .need-validate[name=' + keys[i] + ']';
            $(selector2).addClass('is-invalid');
            $(selector2).closest('div').append(html);

            var selector3 = options.formSelector + ' .need-validate[data-name=' + keys[i] + ']';
            $(selector3).addClass('is-invalid');
            $(selector3).closest('div').append(html);
        }
    }
}

function weClearErrors(formSelector, isSidePanel) {
    $("div.invalid-feedback").detach();
    $(formSelector + ' *').removeClass('is-invalid');
    $('.has-error').removeClass('has-error');

    var selector = '.we-biz-error-area .alert';
    if (isSidePanel) {
        selector = '#side_panel ' + selector;
    }

    $(selector).css('display', 'none');
    $(selector).html('');
}

function weShowBizErrors(errors) {
    $('.has-error').removeClass('has-error');
    var selector = '.we-biz-error-area .alert';
    $(selector).css('display', 'none');
    if (errors && errors.length > 0) {
        $(selector).closest('.wrapper').addClass('has-error');
        var msg = typeof errors === 'string' ? errors : GetErrorMessage(errors);
        $(selector + ' .alert-text').html(msg);
        $(selector).css('display', '');
    }
}