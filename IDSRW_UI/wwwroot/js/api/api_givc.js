(function (window) {
    'use strict';

    var App = window.App || {};
    var $ = window.jQuery;
    // Определим язык
    App.Lang = ($.cookie('lang') === undefined ? 'ru' : $.cookie('lang'));

    //import data from '../../data/setup.json' assert { type: 'JSON' };
    //console.log(data);


    // Массив текстовых сообщений 
    $.Text_View =
    {
        'default':  //default language: ru
        {
            'mess_load_reference': 'Загружаю справочники...',
        },
        'en':  //default language: English
        {
            'mess_load_reference': 'Loading references ...',
        }
    };
    // Определлим список текста для этого модуля
    App.Langs = $.extend(true, App.Langs, getLanguages($.Text_View, App.Lang));
    //****************************************************************************************
    //-------------------------------- Конструктор и инициализация ---------------
    // создать класс api ГИВЦ
    function api_givc(options) {
        this.list_countrys = null;
        this.settings = $.extend({
            url_api: null,
        }, options);

    };
    //****************************************************************************************
    //-------------------------------- Функции работы с БД через api ---------------
    // Загрузить таблицы базы данных 
    api_givc.prototype.load = function (list, lock, update, callback) {
        var process = 0;
        var result = [];
        var out_load = function (process) {
            if (process === 0) {
                if (lock) { LockScreenOff(); }
                if (typeof callback === 'function') {
                    callback(result);
                }
            }
        };
        if (list) {
            $.each(list, function (i, table) {
                if (table === 'countrys') {
                    if (lock) LockScreen(langView('mess_load_reference', App.Langs));
                    if (update || !this.list_countrys) {
                        process++;
                        this.getCountrys(function (data) {
                            this.list_countrys = data;
                            process--;
                            result.push('countrys');
                            out_load(process);
                        }.bind(this));
                    };
                };
            }.bind(this));
        };
        out_load(process);
    };
    //======= Получить все выполненные запросы по указаной справке ======================================
    api_givc.prototype.getRequestOfTypeRequests = function (type_requests, count, callback) {
        $.ajax({
            type: 'GET',
            url: this.settings.url_api + '/GIVC/Request/type_requests/' + type_requests + '/count/' + count,
            async: true,
            dataType: 'json',
            beforeSend: function () {
                AJAXBeforeSend();
            },
            success: function (data) {
                if (typeof callback === 'function') {
                    callback(data);
                }
            },
            error: function (x, y, z) {
                OnAJAXError("api_givc.getRequestOfTypeRequests", x, y, z);
            },
            complete: function () {
                AJAXComplete();
            },
        });
    };
    //
    api_givc.prototype.getRequestOfId = function (id, callback) {
        $.ajax({
            type: 'GET',
            url: this.settings.url_api + '/GIVC/Request/' + id,
            async: true,
            dataType: 'json',
            beforeSend: function () {
                AJAXBeforeSend();
            },
            success: function (data) {
                if (typeof callback === 'function') {
                    callback(data);
                }
            },
            error: function (x, y, z) {
                OnAJAXError("api_givc.getRequestOfId", x, y, z);
            },
            complete: function () {
                AJAXComplete();
            },
        });
    };
    //
    api_givc.prototype.postGIVC = function (parameters, callback) {
        $.ajax({
            url: this.settings.url_api + '/GIVC/',
            type: 'POST',
            data: JSON.stringify(parameters),
            contentType: "application/json;charset=utf-8",
            async: true,
            beforeSend: function () {
                AJAXBeforeSend();
            },
            success: function (data) {
                if (typeof callback === 'function') {
                    callback(data);
                }
            },
            error: function (x, y, z) {
                OnAJAXError("api_givc.postGIVC", x, y, z, callback);
            },
            complete: function () {
                AJAXComplete();
            },
        });
    };

    App.api_givc = api_givc;

    window.App = App;

})(window);