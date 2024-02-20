(function (window) {
    'use strict';

    var App = window.App || {};
    var $ = window.jQuery;
    // Определим язык
    App.Lang = ($.cookie('lang') === undefined ? 'ru' : $.cookie('lang'));

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
    function api_givc() {
        this.list_countrys = null;
    }
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
    api_givc.prototype.getRequestOfTypeRequests = function (type_requests, callback) {
        $.ajax({
            type: 'GET',
            //url: '../../api/ids/directory/currency/all',
            url: 'https://krr-app-paweb01.europe.mittalco.com/IDSRW_API/GIVC/Request/type_requests/' + type_requests,
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
                OnAJAXError("ids_directory.getCurrency", x, y, z);
            },
            complete: function () {
                AJAXComplete();
            },
        });
    };


    App.api_givc = api_givc;

    window.App = App;

})(window);