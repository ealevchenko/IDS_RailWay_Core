﻿(function ($) {
    "use strict"; // Start of use strict
    var App = window.App || {};
    var $ = window.jQuery;

    var format_date = "YYYY-MM-DD";
    var format_time = "HH:mm:ss";
    var format_datetime = "YYYY-MM-DD HH:mm:ss";

    // Массив текстовых сообщений 
    $.Text_View =
    {
        'default':  //default language: ru
        {

            //'title_select': 'Выберите...',

        },
        'en':  //default language: English
        {
            //'title_select': 'Выберите...',
        }
    };

    // Определим глобальные переменные
    App.Lang = ($.cookie('lang') === undefined ? 'ru' : $.cookie('lang'));
    App.Langs = $.extend(true, App.Langs, getLanguages($.Text_View, App.Lang)); //, getLanguages($.Text_Common, App.Lang), getLanguages($.Text_Table, App.Lang)
    App.User_Name = $('input#username').val();

    var API_GIVC = App.api_givc;
    var TTDR = App.table_report;
    var api_givc = new API_GIVC({ url_api: "https://krr-app-paweb01.europe.mittalco.com/IDSRW_API" });

    // Модуль инициализаии компонентов формы
    var FE = App.form_element;

    var fe_ui = new FE();

    var cur_type = '-1';
    var cur_Id = -1;
    var curr_data = [];

    $(document).ready(function ($) {
        var list_type_requests = [
            {
                'value': 'req1892',
                'text': 'Дислокація вагонів (справка 1892)',
            },
            {
                'value': 'req0002',
                'text': 'Натурный лист поезда (справка 0002)',
            },
            {
                'value': 'req8858',
                'text': 'Дислокация вагона (справка 8858)',
            },
            {
                'value': 'req8692',
                'text': 'Оперативное сальдо (справка 8692)',
            },
            {
                'value': 'regDisvag',
                'text': 'Дислокация вагонов c грузом (справка Disvag)',
            }
        ];
        var last_requests = [];
        // Отобразить экран с информацией
        var view_report = function (type, id) {
            // Обработаем справку 1892
            if (type === 'req1892') {
                curr_data = [];
                if (id > 0) {
                    LockScreen(langView('mess_load_data', App.Langs));
                    api_givc.getRequestOfId(id, function (data) {

                        if (data !== null && data.resultRequests !== null) {
                            var res = JSON.parse(data.resultRequests);
                            if (res.disl_vag != null) {
                                curr_data = res.disl_vag;
                            }
                            table_table_req1892.view(curr_data);
                            LockScreenOff();
                        }
                    }.bind(this));
                } else {
                    table_table_req1892.view(curr_data);
                }

            }
        }

        var el_select_type_requests = new fe_ui.init_select($("#type-requests"), {
            data: list_type_requests,
            default_value: -1,
            fn_change: async function (e) {
                cur_type = $(e.currentTarget).val();
                last_requests = [];
                if (cur_type !== '-1') {
                    LockScreen(langView('mess_delay', App.Langs));
                    api_givc.getRequestOfTypeRequests(cur_type, function (data) {
                        $.each(data, function (i, el) {
                            last_requests.push({
                                'value': el.id,
                                'text': moment(el.dtRequests).format(format_datetime),
                                'disabled': false,
                            });

                        }.bind(this));
                        el_select_last_requests.update(last_requests, -1);
                        LockScreenOff();
                    }.bind(this));
                } else {
                    el_select_last_requests.update(last_requests, -1);
                    view_report(cur_type, cur_Id);
                }
            }.bind(this),
            check: function (value) {

            }.bind(this)
        });
        var el_select_last_requests = new fe_ui.init_select($("#last-requests"), {
            data: last_requests,
            default_value: -1,
            fn_change: function (e) {
                cur_Id = Number($(e.currentTarget).val());
                view_report(cur_type, cur_Id);
            }.bind(this),
            check: function (value) {

            }.bind(this)
        });

        var table_table_req1892 = new TTDR('div#req1892');               // Создадим экземпляр
        // Инициализация модуля "Таблица прибывающих составов"
        table_table_req1892.init({
            alert: null,
            detali_table: false,
            type_report: 'req1892',     //
            link_num: false,
            ids_wsd: null,
            fn_init: function () {
                LockScreenOff();
            },
            fn_action_view_detali: function (rows) {

            },
            fn_select_rows: function (rows) {

            }.bind(this),
        });



        LockScreenOff();
        /*        $('#example').DataTable();*/
    });


})(jQuery); // End of use strict