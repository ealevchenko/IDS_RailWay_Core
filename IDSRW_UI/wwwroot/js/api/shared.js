/* ----------------------------------------------------------
    Обработчики ajax - функций
-------------------------------------------------------------*/
// Событие перед запросом
var AJAXBeforeSend = function () {
    //OnBegin();
}
// Обработка ошибок
var OnAJAXError = function (metod, x, y, z, callback) {
    var data = {
        message: null,
        status: null,
        statusText: null
    };
    if (x && x.status) {
        data.status = x.status;
    }
    if (x && x.statusText) {
        data.statusText = x.statusText;
    }
    if (x && x.responseJSON) {
        data.message = x.responseJSON.Message;
    }
    alert('Metod js : ' + metod + '\nStatus : ' + data.status + '\nStatusText : ' + data.statusText + '\nMessage : ' + data.message);
    if (typeof callback === 'function') {
        callback(data);
    }
};
// Событие после выполнения
var AJAXComplete = function () {
    //LockScreenOff();
};