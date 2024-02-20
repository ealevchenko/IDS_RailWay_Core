(function ($) {
    "use strict"; // Start of use strict
    var App = window.App || {};
    var $ = window.jQuery;

    var format_datetime = "YYYY-MM-DD HH:mm:ss";

    // Массив текстовых сообщений 
    $.Text_View =
    {
        'default':  //default language: ru
        {

            'title_select': 'Выберите...',
        },
        'en':  //default language: English
        {
            'title_select': 'Выберите...',
        }
    };

    // Определим глобальные переменные
    App.Lang = ($.cookie('lang') === undefined ? 'ru' : $.cookie('lang'));
    App.Langs = $.extend(true, App.Langs, getLanguages($.Text_View, App.Lang)); //, getLanguages($.Text_Common, App.Lang), getLanguages($.Text_Table, App.Lang)
    App.User_Name = $('input#username').val();

    var API_GIVC = App.api_givc;
    var api_givc = new API_GIVC();


    // Модуль инициализаии компонентов формы
    var FE = App.form_element;
    var fe_ui = new FE();

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

        var el_select_type_requests = new fe_ui.init_select($("#type-requests"), {
            data: list_type_requests,
            default_value: -1,
            fn_change: async function (e) {
                var type = $(e.currentTarget).val();
                api_givc.getRequestOfTypeRequests(type, function (data) {

                }.bind(this));
                var response = await fetch("https://krr-app-paweb01.europe.mittalco.com/IDSRW_API/GIVC/Request/type_requests/" + type, {
                    method: "GET",
                    headers: {
                        "Accept": "application/json; charset=utf-8"
                    }
                });
                // если запрос прошел нормально
                if (response.ok === true) {
                    // получаем данные
                    var result = await response.json();

                    result.forEach(user => {
                        // добавляем полученные элементы в таблицу
                        last_requests = []
                    });
                }
            }.bind(this),
            check: function (value) {

            }.bind(this)
        });

        var el_select_last_requests = new fe_ui.init_select($("#last-requests"), {
            data: last_requests,
            default_value: -1,
            fn_change: function (e) {
                var sel = $(e.currentTarget).val();
            }.bind(this),
            check: function (value) {

            }.bind(this)
        });
        //var $default_option = $('<option></option>', {
        //    'value': '-1',
        //    'text': langView('title_select', App.Langs),
        //});

        //var $select_type_requests = $("#type-requests");
        //$select_type_requests.empty();
        //$select_type_requests.append($default_option);
        //$select_type_requests.on('change', function (e) {
        //    var sel = $(this).val();
        //});

        //var select_type_requests = $("#type-requests").on('change', function (e) {
        //    var sel = $(this).val();
        //});
        LockScreenOff();
    });


})(jQuery); // End of use strict