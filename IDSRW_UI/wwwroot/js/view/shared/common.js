    /* ----------------------------------------------------------
            Вывод текста согласно региональных настроек
    -------------------------------------------------------------*/
    // Метод определения списка по указаному языку
    getLanguages = function (languages, lang) {
        if (lang === 'ru') {
            var language = navigator.language ? navigator.language : navigator.browserLanguage;
            if (!language) return languages['default'];
            language = language.toLowerCase();
            for (var key in languages) {
                if (language.indexOf(key) != -1) {
                    return languages[key];
                }
            }
            return languages['default'];
        }
        else if (lang && lang in languages) {
            return languages[lang];
        }
        else {
            return languages['default'];
        }
    };
    // Показать текст
    langView = function (t, langs) {
        var _t = t.toLowerCase();
        var re = (t in langs) ? langs[t] : (_t in langs) ? langs[_t] : null;
        if (re === null) {
            throw new Error('Неопределённ параметр : ' + t);
        }
        return re;
    };
    //==============================================================================================
    /* ----------------------------------------------------------
                    Блокировка экрана
    -------------------------------------------------------------*/
    // Блокировать с текстом
    var LockScreen = function (message) {
        var lock = document.getElementById('lockPanel');
        if (lock)
            lock.className = 'LockOn';
        lock.innerHTML = message;
    };
    // Разблокировать 
    var LockScreenOff = function () {
        var lock = document.getElementById('lockPanel');
        if (lock)
            lock.className = 'LockOff';
    };
    //------------------------------------------------------------------------
    // Определение параметров переданных по url
    var getUrlVars = function () {
        var vars = [], hash;
        var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
        for (var i = 0; i < hashes.length; i++) {
            hash = hashes[i].split('=');
            vars.push(hash[0]);
            vars[hash[0]] = hash[1];
        }
        return vars;
    };
    var getUrlVar = function (name) {
        return getUrlVars()[name];
    };

(function (window) {
    'use strict';

    var App = window.App || {};
    var $ = window.jQuery;
    // Определим язык
    App.Lang = ($.cookie('lang') === undefined ? 'ru' : $.cookie('lang'));
    /* ----------------------------------------------------------
                        Список слов
    -------------------------------------------------------------*/
    $.Text_Common =
    {
        'default':  //default language: ru
        {
            'mess_delay': 'Мы обрабатываем ваш запрос...',
            'mess_load_table': 'Формируем таблицу...',
            'mess_load': 'Загрузка справочников...',
            'mess_save': 'Запись и обновление данных...',
            'mess_load_data': 'Получение запрашиваемых данных...',
            'mess_operation': 'Выполняю операцию...',
            'mess_update_uz': 'Обновляю данные на УЗ...',
            'mess_checking_data': 'Проверяю данные...',

            'mess_error_not_cars': 'Введите номер вагона или несколько вагонов, разделитель номеров ";"',
            'mess_error_input_num_cars': 'Ошибка ввода, номер позиции :{0}, введен неправильный номер :{1}',
            'mess_error_input_num_cars1': 'Ошибка ввода, номер позиции :{0}, номер не может быть меньше или равен 0 :{1}',
            'mess_error_input_num_cars2': 'Ошибка ввода, номер позиции :{0}, не системная нумерация (ошибка контрольной суммы) :{1}',
            'mess_error_input_num_cars_duble': 'Ошибка ввода, введеный номер :{0} - повторяется!',

            'mess_error_not_docs': 'Введите номер документа или несколько документов, разделитель номеров ";"',
            'mess_error_input_num_docs': 'Ошибка ввода, номер позиции :{0}, введен неправильный номер :{1}',
            'mess_error_input_num_docs1': 'Ошибка ввода, номер позиции :{0}, номер не может быть меньше или равен 0 :{1}',
            'mess_error_input_num_docs_duble': 'Ошибка ввода, введеный номер :{0} - повторяется!',


            'epd_status_unknown': 'Статус невідомий',
            'epd_status_draft': 'Чернетка',
            'epd_status_sending': 'Документ передається товарному касиру',
            'epd_status_registered': 'Документ переданий товарному касиру',
            'epd_status_reclaiming': 'Документ відкликається від товарного касира',
            'epd_status_accepted': 'Вантаж прийнято до перевезення',
            'epd_status_delivered': 'Вантаж прибув',
            'epd_status_recieved': 'Вантаж отримано одержувачем',
            'epd_status_uncredited': 'Документ розкредитовано товарним касиром',
            'epd_status_recieved_draft': 'Вантаж отримано одержувачем і редагується',
            'epd_status_recieved_sending': 'Вантаж отримано одержувачем і переданий товарному касиру',
            'epd_status_recieved_reclaiming': 'Вантаж отримано одержувачем і відкликається від товарного касира',
            'epd_status_canceled': 'Документ зіпсований товарним касиром',
            'epd_status_locked': 'Документ заблокований',

        },
        'en':  //default language: English
        {
            'mess_delay': 'We are processing your request ...',
            'mess_load_table': 'Forming table ...',
            'mess_load': 'Downloading reference books...',
            'mess_save': 'Writing and updating data ...',
            'mess_load_data': 'Receiving the requested data...',
            'mess_operation': 'Performing an operation...',
            'mess_update_uz': 'I am updating the data on the UZ ...',
            'mess_checking_data': 'Checking data...',

            'mess_error_not_cars': 'Enter the number of a car or several cars, number separator ";"',
            'mess_error_input_num_cars': 'Input error, item number :{0}, wrong number entered :{1}',
            'mess_error_input_num_cars1': 'Input error, position number :{0}, number cannot be less than or equal to 0 :{1}',
            'mess_error_input_num_cars2': 'Input error, position number :{0}, non-system numbering (checksum error) :{1}',
            'mess_error_input_num_cars_duble': 'Input error, number entered :{0} - repeated!',

            'epd_status_unknown': 'Unknown status',
            'epd_status_draft': 'Darling',
            'epd_status_sending': 'Document is being sent to the commodity cashier',
            'epd_status_registered': 'Document of transfers to the commodity cashier',
            'epd_status_reclaiming': 'Document reclaimed by cashier',
            'epd_status_accepted': 'Vantage accepted before moving',
            'epd_status_delivered': 'Epd_status_delivered',
            'epd_status_recieved': 'Vantage received by owner',
            'epd_status_uncredited': 'Document uncredited by commodity cashier',
            'epd_status_recieved_draft': 'Vantage received and edited',
            'epd_status_recieved_sending': 'Vantage received by the receiver and the goods cashier',
            'epd_status_recieved_reclaiming': 'Vantage received by the owner and reclaimed by the cashier',
            'epd_status_canceled': 'Document of zіpsovaniya commodity cashier',
            'epd_status_locked': 'Lock Document',
        }

    };
    // Массив текстовых сообщений 
    $.Text_View =
    {
        'default':  //default language: ru
        {
            'title_label_date': 'ПЕРИОД :',
            'title_select': 'Выберите...',
        },
        'en':  //default language: English
        {
            'title_label_date': 'PERIOD:',
            'title_select': 'Select ...',
        }
    };
    // Определлим список текста для этого модуля
    App.Langs = $.extend(true, App.Langs, getLanguages($.Text_View, App.Lang), getLanguages($.Text_Common, App.Lang));

    //================================================================================
    // Класс для создания объектов 
    //--------------------------------Конструктор и инициализация---------------
    // создать класс
    function form_element() {

    };

    form_element.prototype.init_select = function (element, options) {
        //TODO: создать и настроить SELECT сделать надпись выберите через placeholder, чтобы работала required
        this.$element = element;
        var $default_option = $('<option></option>', {
            'value': '-1',
            'text': langView('title_select', App.Langs),
            'disabled': false,
        });
        this.settings = $.extend({
            data: [],
            default_value: null,
            fn_change: null,
            check: null,
        }, options);
        this.init = function () {
            this.update(this.settings.data, this.settings.default_value);
            if (typeof this.settings.fn_change === 'function') {
                this.$element.on("change", function (event) {
                    //this.settings.fn_change.bind(this);
                    if (typeof this.settings.fn_change) {
                        this.settings.fn_change(event);
                    }
                    if (typeof this.settings.check === 'function') {
                        this.settings.check(element.val());
                    };
                }.bind(this));
            }
        };
        this.val = function (value) {
            if (value !== undefined) {
                var disabled = this.$element.prop("disabled");
                if (disabled) {
                    this.$element.prop("disabled", false);
                }
                this.$element.val(value);
                if (disabled) {
                    this.$element.prop("disabled", true);
                }
            } else {
                return this.$element.val();
            };
        };
        this.getNumber = function () {
            return this.$element.val() === null ? null : Number(this.$element.val());
        };
        this.getNumberNull = function () {
            return this.$element.val() === null || Number(this.$element.val()) === -1 ? null : Number(this.$element.val());
        };
        this.text = function (text) {
            if (text !== undefined) {
                var disabled = this.$element.prop("disabled");
                if (disabled) {
                    this.$element.prop("disabled", false);
                }
                this.$element.val(text === null ? '' : text);
                if (disabled) {
                    this.$element.prop("disabled", true);
                }
            } else {
                return this.$element.text();
            };
        };
        this.update = function (data, default_value) {
            this.$element.empty();
            element.append($default_option);
            //if (default_value === -1) {
            //    element.append($default_option);
            //}
            if (data) {
                $.each(data, function (i, el) {
                    // Преобразовать формат
                    if (el) {
                        var $option = $('<option></option>', {
                            'value': el.value,
                            'text': el.text,
                            'disabled': el.disabled,
                        });
                        this.$element.append($option);
                    }
                }.bind(this));
            };
            this.$element.val(default_value);
        };
        this.show = function () {
            this.$element.show();
        };
        this.hide = function () {
            this.$element.hide();
        };
        this.enable = function () {
            this.$element.prop("disabled", false);
        };
        this.disable = function (clear) {
            if (clear) this.$element.val(-1);
            this.$element.prop("disabled", true);
        };
        this.init();
    };

    form_element.prototype.select = function (options) {
        this.settings = $.extend({
            id: null,
            class: null,
            title: null,
            placeholder: null,
            required: null,
            size: null,
            multiple: null,
            readonly: false,
        }, options);
        this.$select = $('<select></select>');
        if (!this.$select || this.$select.length === 0) {
            throw new Error('Не удалось создать элемент <select></select>');
        } else {
            add_class(this.$select, this.settings.class);
            add_id(this.$select, this.settings.id);
            add_tag(this.$select, 'name', this.settings.id);
            add_tag(this.$select, 'title', this.settings.title);
            add_tag(this.$select, 'placeholder', this.settings.placeholder);
            add_tag(this.$select, 'required', this.settings.required);
            if (this.settings.size !== null || this.settings.size !== '') {
                add_class(this.$select, 'form-control-' + this.settings.size);
            }
            if (this.settings.multiple) {
                add_tag(this.$select, 'multiple', 'multiple');
            }
            this.$select.prop('readonly', this.settings.readonly);
        }
    };

    App.form_element = form_element;


    window.App = App;

})(window);