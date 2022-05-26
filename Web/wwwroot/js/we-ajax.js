
/**
 * Control ajax response in app.
 * ajax config : url, type, dataType, data
 * app config : bizType, formSelector, submitBtnSelector
 * other: returnUrl, success, error, failure
 * more info: "wwwroot/js/we-ajax.js"
 * Author: TK
 * Create date: 2022-05-26
 * @param {object} option option for configuration.
 * @param {''} option.url
 * @param {'post'} [option.type]
 * @param {'json'} [option.dataType]
 * @param {null} [option.data]
 * @param {'edit'} [option.bizType] has 3 type -> 'list', 'edit', 'switch', 'other'
 * @param {null} [option.formSelector] is required if option.bizType is 'edit'
 * @param {'#btn-save'} [option.submitBtnSelector]
 * @param {true} [option.isReload]
 * @param {string} [option.returnUrl]
 * @param {function} [option.success]
 * @param {function} [option.error]
 * @param {function} [option.complete]
 * @param {function} [option.successOverride]
 */
var weAjax = function (option) {
    if (!option.url)
        console.error('weAjax: "url" is missing!');
    option.data = typeof (option.data) === 'undefined' ? null : option.data;
    option.bizType = typeof (option.bizType) === 'undefined' ? 'edit' : option.bizType;
    option.submitBtnSelector = typeof (option.submitBtnSelector) === 'undefined' ? '#btn-save' : option.submitBtnSelector;
    option.isReload = typeof (option.isReload) === 'undefined' ? true : option.isReload;
    option.returnUrl = typeof (option.returnUrl) === 'undefined' ? null : option.returnUrl;
    if (option.bizType == 'edit' && !option.formSelector)
        console.error('weAjax: "formSelector" is missing!');

    var successFunc = typeof (option.successOverride) !== 'undefined' ?
        option.successOverride :
        function (res, textStatus, xhr) {
            if (res.statusCode == "0") { // 📌 StatusCodeSuccess

                // #region StatusCodeSuccess
                switch (option.bizType) {
                    case 'list':
                        var msg = res.message || "Save sucess";
                        alert(msg);
                        if (option.isReload == false && option.returnUrl) {
                            if (option.returnUrl == "-") {
                                // nothing
                            }
                            else {
                                location.href = option.returnUrl;
                            }
                        } else if (option.isReload) {
                            location.reload();
                        }
                        break;
                    case 'edit':
                        var msg = res.message || "Save sucess";
                        alert(msg);
                        weClearErrors(option.formSelector);
                        if (option.isReload == false && option.returnUrl) {
                            if (option.returnUrl == "-") {
                                // nothing
                            }
                            else {
                                location.href = option.returnUrl;
                            }
                        } else if (option.isReload) {
                            location.reload();
                        }
                        break;
                    case 'switch':
                        if (option.isReload)
                            location.reload();
                        break;
                    case 'other':
                        // do nothing
                        break;
                }
                // #endregion
            }
            else if (res.statusCode == "1") { // 📌 StatusCodeError

                $(option.submitBtnSelector).removeAttr('disabled');
                // #region StatusCodeError
                if ((!res.errors || res.errors.length == 0) && (!res.errorMap || Object.entries(res.errorMap).length == 0)) {
                    var msg = res.message || "Save sucess";
                    alert(msg);
                }
                else {

                    var message = getErrorMessage(res.errors);

                    switch (option.bizType) {
                        case 'list':
                            alert(message);
                            break;
                        case 'edit':
                            weFillFormErrors({
                                errorMap: res.errorMap,
                                formSelector: option.formSelector
                            });

                            weShowBizErrors(res.errors);
                            break;
                        case 'switch':
                            alert(message);
                            break;
                        case 'other':
                            alert(message);
                            break;
                    }
                }
                // #endregion
            }
            else if (res.statusCode == "2") { // 📌 StatusCodeBizError

                // #region StatusCodeBizError
                $(option.submitBtnSelector).removeAttr('disabled');

                var message = getErrorMessage(res.errors);
                message = message ? message : res.message;

                switch (option.bizType) {
                    case 'list':
                        alert(message);
                        break;
                    case 'edit':
                        weFillFormErrors({
                            errorMap: res.errorMap,
                            formSelector: res.formSelector,
                        });

                        weShowBizErrors(res.errors);
                        break;
                    case 'switch':
                        alert(message);
                        break;
                    case 'other':
                        alert(message);
                        break;
                }
                // #endregion
            }
            else if (res.statusCode == "3") {// 📌 StatusCodeUnauthorized
                alert("Unauthorized");
            }
        }

    $.ajax({
        url: option.url ? option.url : '',
        type: option.type ? option.type : 'post',
        dataType: option.dataType ? option.dataType : 'json',
        data: option.data ? option.data : null,
        async: true,
        beforeSend: function (xhr, settings) {
            $(option.submitBtnSelector).attr('disabled', true);
        },
        complete: function (jqXHR, textStatus) {
            $(option.submitBtnSelector).attr('disabled', false);
            if (option.complete && typeof (option.complete) === 'function')
                option.complete(jqXHR, textStatus);
        },
        success: successFunc,
        error: function (request, status, error) {
            alert("Server error");
            // call error call back
            if (option.error && typeof (option.error) === 'function')
                option.error(request, status, error);
        }
    });
}
