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
//==============================================================================================
/* ----------------------------------------------------------
    DataTables Вывод текста согласно региональных настроек
-------------------------------------------------------------*/
// Список слов для описания таблиц
$.Text_Table =
{
    'default':  //default language: ru
    {
        "dt_decimal": "",
        "dt_emptyTable": "Нет данных в таблице",
        "dt_info": "Отображение _START_ по _END_ из _TOTAL_ записей",
        "dt_infoEmpty": "Отображение 0 to 0 of 0 записей",
        "dt_infoFiltered": "(отфильтровано из _MAX_ всего записей)",
        "dt_infoPostFix": "",
        "dt_thousands": ".",
        "dt_lengthMenu": "Показать  _MENU_ записей",
        "dt_loadingRecords": "Загрузка...",
        "dt_processing": "Обработка ...",
        "dt_search": "Найти:",
        "dt_zeroRecords": "Не найдено совпадающих записей",
        "dt_paginate": {
            "first": "Первая",
            "last": "Последняя",
            "next": "Следующая",
            "previous": "Предыдущая"
        },
        "dt_aria": {
            "sortAscending": ": активировать сортировку столбца по возрастанию",
            "sortDescending": ": активировать сортировку колонки по убыванию"
        }

    },
    'en':  //default language: English
    {
        "dt_decimal": "",
        "dt_emptyTable": "No data available in table",
        "dt_info": "Showing _START_ to _END_ of _TOTAL_ entries",
        "dt_infoEmpty": "Showing 0 to 0 of 0 entries",
        "dt_infoFiltered": "(filtered from _MAX_ total entries)",
        "dt_infoPostFix": "",
        "dt_thousands": ",",
        "dt_lengthMenu": "Show _MENU_ entries",
        "dt_loadingRecords": "Loading...",
        "dt_processing": "Processing...",
        "dt_search": "Search:",
        "dt_zeroRecords": "No matching records found",
        "dt_paginate": {
            "first": "First",
            "last": "Last",
            "next": "Next",
            "previous": "Previous"
        },
        "dt_aria": {
            "sortAscending": ": activate to sort column ascending",
            "sortDescending": ": activate to sort column descending"
        }

    }

};
// Настройка language(DataTables)
var language_table = function (langs) {
    return {
        "decimal": langView('dt_decimal', langs),
        "emptyTable": langView('dt_emptyTable', langs),
        "info": langView('dt_info', langs),
        "infoEmpty": langView('dt_infoEmpty', langs),
        "infoFiltered": langView('dt_infoFiltered', langs),
        "infoPostFix": langView('dt_infoPostFix', langs),
        "thousands": langView('dt_thousands', langs),
        "lengthMenu": langView('dt_lengthMenu', langs),
        "loadingRecords": langView('dt_loadingRecords', langs),
        "processing": langView('dt_processing', langs),
        "search": langView('dt_search', langs),
        "zeroRecords": langView('dt_zeroRecords', langs),
        "paginate": langView('dt_paginate', langs),
        "aria": langView('dt_aria', langs),
    };
};

var init_columns = function (collums_name, list_collums) {
    var collums = [];
    if (collums_name && collums_name.length > 0) {
        $.each(collums_name, function (i, el) {
            var field = list_collums.find(function (o) {
                return o.field === el;
            });
            // Если поле не найдено, создадим по умолчанию (чтобы небыло ошибки)
            if (!field) {
                field = {
                    field: el,
                    data: function (row, type, val, meta) {
                        return "Field_error";
                    },
                    title: el, width: "100px", orderable: false, searchable: false
                };
            }
            field.className += ' fl-' + el;
            collums.push(field);
        });
    }
    return collums;
};

var init_columns_detali = function (collums_detali, list_collums) {
    var collums = [];
    if (collums_detali && collums_detali.length > 0) {
        $.each(collums_detali, function (i, el) {
            var field = list_collums.find(function (o) {
                return o.field === el.field;
            });
            // Если поле не найдено, создадим по умолчанию (чтобы небыло ошибки)
            if (!field) {
                field = {
                    field: el,
                    data: function (row, type, val, meta) {
                        return "Field_error";
                    },
                    title: el, width: "100px", orderable: false, searchable: false
                };
            }
            field.className += ' fl-' + el.field;
            // Добавим детали
            if (el.title !== null) {
                field.title = el.title;
            }
            if (el.class !== null) {
                field.className += ' ' + el.class;
            }
            collums.push(field);
        });
    }
    return collums;
};

var init_buttons = function (buttons_name, list_buttons) {
    var buttons = [];
    if (buttons_name && buttons_name.length > 0) {
        $.each(buttons_name, function (i, el) {
            var button = list_buttons.find(function (o) {
                return o.button === el.name;
            });
            // Если кнопка не найдена, создадим по умолчанию (чтобы небыло ошибки)
            if (!button) {
                button = {
                    button: el.name,
                    text: button_error,
                    action: function (e, dt, node, config) {

                    },
                    enabled: false
                };
            }
            if (el.action) {
                button.action = el.action;
            }
            buttons.push(button);
        });
    }
    return buttons;
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
    App.Langs = $.extend(true, App.Langs, getLanguages($.Text_View, App.Lang), getLanguages($.Text_Common, App.Lang), getLanguages($.Text_Table, App.Lang));

    //================================================================================
    // Класс для создания объектов 
    //--------------------------------Конструктор и инициализация---------------
    // создать класс
    function form_element() {

    };

    var add_tag = function (element, tag_name, tag_value) {
        if (element && tag_name && tag_name !== '' && tag_value !== null) {
            element.attr(tag_name, tag_value);
        }
    }

    var add_class = function (element, tag) {
        if (element && tag && tag !== '') {
            element.addClass(tag);
        }
    }

    var add_id = function (element, tag) {
        if (element && tag && tag !== '') {
            element.attr('id', tag);
        }
    }

    var add_for = function (element, tag) {
        if (element && tag && tag !== '') {
            element.attr('for', tag);
        }
    }

    var add_title = function (element, tag) {
        if (element && tag && tag !== '') {
            element.attr('title', tag);
        }
    }

    var add_value = function (element, value) {
        if (element && value && value !== '') {
            element.attr('value', value);
        }
    }

    var add_val = function (element, value) {
        if (element && value && value !== '') {
            element.val(value);
        }
    }

    var append_label = function (element, label) {
        if (element && label && label !== '') {
            element.append(label);
        }
    };

    var add_click = function (element, fn) {
        if (element && typeof fn === 'function') {
            element.on('click', fn);
        }
    };

    form_element.prototype.init_select = function ($element, options) {
        //TODO: создать и настроить SELECT сделать надпись выберите через placeholder, чтобы работала required
        if (!$element) {
            throw new Error('Не указан элемент $element');
        }
        this.$element = $element;
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
                        this.settings.check(this.$element.val());
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
            if (default_value === -1) {
                this.$element.append($default_option);
            }
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
    // Инициализация текстового поля "INPUT"
    form_element.prototype.init_input = function ($element, options) {
        this.settings = $.extend({
            default_value: null,
            fn_change: null,
        }, options);
        this.type = element.attr('type');
        this.$element = $element;
        this.init = function () {
            this.update(this.settings.default_value);
            if (typeof this.settings.fn_change === 'function') {
                this.$element.on("change", this.settings.fn_change.bind(this));
            }
        };
        this.val = function (value) {
            if (value !== undefined) {
                this.$element.val(value);
                //this.$element.change();
            } else {
                if (this.type === 'number') {
                    return this.$element.val() !== '' ? Number(this.$element.val()) : null;
                }
                if (this.type === 'text') {
                    return this.$element.val() !== '' ? $.trim(String(this.$element.val())) : null;
                }
                if (this.type === 'date') {
                    return this.$element.val() !== '' ? moment(this.$element.val()) : null;
                }
                return this.$element.val();
            };
        };
        this.update = function (default_value) {
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
            if (clear) this.$element.val('');
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

    form_element.prototype.table = function (options) {
        this.settings = $.extend({
            id: null,
            class: null,
            title: null,
        }, options);
        this.$table = $('<table></table>');

        if (!this.$table || this.$table.length === 0) {
            throw new Error('Не удалось создать элемент <table></table>');
        } else {
            add_class(this.$table, this.settings.class);
            add_id(this.$table, this.settings.id);
            add_tag(this.$table, 'title', this.settings.title);
        }
    };

    App.form_element = form_element;


    //================================================================================
    // Класс валидации элементов формы
    function validation_form() {

    }

    validation_form.prototype.init = function (options) {
        this.settings = $.extend({
            alert: null,
            elements: null,
        }, options);
        this.type_message = 0; // 0- ок 1-warning 2-error
        this.$alert = null;
        if (this.settings.alert && this.settings.alert.$alert) {
            this.$alert = this.settings.alert.$alert;
        }
    };

    validation_form.prototype.clear_all = function (not_clear_message) {
        if (!not_clear_message) this.clear_message();
        this.clear_error();
    };
    // Очистить все ошибки
    validation_form.prototype.clear_error = function (obj) {
        if (obj) {
            obj.removeClass('is-valid is-invalid');
        } else {
            if (this.settings.elements && this.settings.elements.length > 0) {
                this.settings.elements.each(function () {
                    $(this).removeClass('is-valid is-invalid').nextAll(".invalid-feedback").text('');
                });
            };
        };
    };
    // Очистить сообщения
    validation_form.prototype.clear_message = function () {
        if (this.$alert) {
            this.$alert.hide().text('').removeClass('alert-success alert-warning alert-danger');
            this.type_message = 0;
        }
    };
    // Вывести сообщение об ошибке
    validation_form.prototype.out_error_message = function (message) {
        if (this.$alert) {
            if (this.type_message <= 1) {
                this.$alert.show().removeClass('alert-success alert-warning').addClass('alert-danger');
                this.type_message = 2;
            }
            if (message) {
                this.$alert.append(message).append($('<br />'));
            }
        }
    };
    // Вывести сообщение внимание
    validation_form.prototype.out_warning_message = function (message) {
        if (this.$alert) {
            if (this.type_message <= 0) {
                this.$alert.show().removeClass('alert-success alert-danger').addClass('alert-warning');
                this.type_message = 1;
            }
            if (message) {
                this.$alert.append(message).append($('<br />'));
            }
        }
    };
    // Вывести информационное сообщение
    validation_form.prototype.out_info_message = function (message) {
        if (this.$alert) {
            if (this.type_message === 0) {
                this.$alert.show().removeClass('alert-warning alert-danger').addClass('alert-success');
            }
            if (message) {
                this.$alert.text(message).append($('<br />'));
            }
        }
    };
    //
    validation_form.prototype.set_control_error = function (o, message) {
        o.removeClass('is-valid').addClass('is-invalid');
        if (message) {
            o.nextAll(".invalid-feedback").text(message);
        } else { o.nextAll(".invalid-feedback").text('') };
    };
    // Установить признак Ok
    validation_form.prototype.set_control_ok = function (o, message) {
        o.removeClass('is-invalid').addClass('is-valid');
        if (message) {
            o.nextAll(".valid-feedback").text(message);
        } else { o.nextAll(".invalid-feedback").text('') };
    };
    // Установить признак ошибка
    validation_form.prototype.set_object_error = function (o, mes_error) {
        this.set_control_error(o, mes_error);
        this.out_error_message(mes_error);
        return false;
    };
    // Установить признак ок
    validation_form.prototype.set_object_ok = function (o, mes_ok) {
        this.set_control_ok(o, mes_ok);
        this.out_info_message(mes_ok);
        return true;
    };
    // --------------------------------------------------------------------------
    // Установить признак ошибка
    validation_form.prototype.set_form_element_error = function (o, mes_error, out_message) {
        this.set_control_error(o.$element, mes_error);
        if (out_message) this.out_error_message(mes_error);
        return false;
    };
    // Установить признак ок
    validation_form.prototype.set_form_element_ok = function (o, mes_ok, out_message) {
        this.set_control_ok(o.$element, mes_ok);
        if (out_message) this.out_info_message(mes_ok);
        return true;
    };
    // Проверка на условие если true-Ок, false - error
    validation_form.prototype.check_control_condition = function (result, o, mes_error, mes_ok, out_message) {
        if (result) {
            this.set_control_ok(o.$element, mes_ok);
            if (out_message) this.out_info_message(mes_ok);
            return true;
        } else {
            this.set_control_error(o.$element, mes_error);
            if (out_message) this.out_error_message(mes_error);
            return false;
        }
    };
    // Проверка на пустое значение "INPUT"
    validation_form.prototype.check_control_input_not_null = function (o, mes_error, mes_ok, out_message) {
        var val = o.val();
        if (o.val() !== null && o.val() !== '') {
            this.set_control_ok(o.$element, mes_ok);
            if (out_message) this.out_info_message(mes_ok);
            return true;
        } else {
            this.set_control_error(o.$element, mes_error);
            if (out_message) this.out_error_message(mes_error);
            return false;
        }
    };
    // Проверим Input введенное значение входит в диапазон (пустое значение - не допускается)
    validation_form.prototype.checkInputOfRange = function (o, min, max, mes_error, mes_ok, out_message) {
        if (o.val() !== '' && o.val() !== null) {
            var value = Number(o.val());
            if (isNaN(value) || value > max || value < min) {
                this.set_control_error(o.$element, mes_error);
                if (out_message) this.out_error_message(mes_error);
                return false;
            } else {
                this.set_control_ok(o.$element, mes_ok);
                if (out_message) this.out_info_message(mes_ok);
                return true;
            }
        } else {
            this.set_control_error(o.$element, mes_error);
            if (out_message) this.out_error_message(mes_error);
            return false;
        }
    };
    // Проверим Input введенное значение входит в диапазон (пустое значение - допускается)
    validation_form.prototype.checkInputOfRange_IsNull = function (o, min, max, mes_error, mes_ok, out_message) {
        if (o.val() !== '' && o.val() !== null) {
            var value = Number(o.val());
            if (isNaN(value) || value > max || value < min) {
                this.set_control_error(o.$element, mes_error);
                if (out_message) this.out_error_message(mes_error);
                return false;
            } else {
                this.set_control_ok(o.$element, mes_ok);
                if (out_message) this.out_info_message(mes_ok);
                return true;
            }
        } else {
            this.set_control_ok(o.$element, mes_ok);
            if (out_message) this.out_info_message(mes_ok);
            return true;
        }
    };
    // Проверка на пустое значение "SELECT"
    validation_form.prototype.check_control_select_not_null = function (o, mes_error, mes_ok, out_message) {
        if (Number(o.val()) >= 0) {
            this.set_control_ok(o.$element, mes_ok);
            if (out_message) this.out_info_message(mes_ok);
            return true;
        } else {
            this.set_control_error(o.$element, mes_error);
            if (out_message) this.out_error_message(mes_error);
            return false;
        }
    };
    // Проверить элемент "autocomplete" на введенное значение
    validation_form.prototype.check_control_autocomplete = function (o, mes_error, mes_ok, mes_null, out_message) {
        if (o.text()) {
            var s = o.val();
            var s1 = o.text();
            if (o.val() !== null) {
                this.set_control_ok(o.$element, mes_ok);
                if (out_message) this.out_info_message(mes_ok);
                return true;
            } else {
                this.set_control_error(o.$element, mes_error);
                if (out_message) this.out_error_message(mes_error);
                return false;
            }
        } else {
            this.set_control_error(o.$element, mes_null);
            if (out_message) this.out_error_message(mes_null);
            return false;
        }
    };
    // Проверить элемент "autocomplete" на введенное значение (c учетом value = null)
    validation_form.prototype.check_control_autocomplete_is_value_null = function (o, mes_error, mes_ok, mes_null, out_message) {
        if (o.text()) {
            var s = o.val();
            if (o.val() !== undefined) {
                this.set_control_ok(o.$element, mes_ok);
                if (out_message) this.out_info_message(mes_ok);
                return true;
            } else {
                this.set_control_error(o.$element, mes_error);
                if (out_message) this.out_error_message(mes_error);
                return false;
            }
        } else {
            this.set_control_error(o.$element, mes_null);
            if (out_message) this.out_error_message(mes_null);
            return false;
        }
    };
    // Проверить элемент "autocomplete" на введенное значение
    validation_form.prototype.check_control_autocomplete_null = function (o, mes_error, mes_ok, out_message) {
        if (o.text()) {
            if (o.val()) {
                this.set_control_ok(o.$element, mes_ok);
                if (out_message) this.out_info_message(mes_ok);
                return true;
            } else {
                this.set_control_error(o.$element, mes_error);
                if (out_message) this.out_error_message(mes_error);
                return false;
            }
        } else {
            this.set_control_ok(o.$element, mes_ok);
            if (out_message) this.out_info_message(mes_ok);
            return true;
        }
    };
    // Проверить элемент "datetime_input" на введенное значение
    validation_form.prototype.check_control_datetime_input = function (o, mes_error, mes_ok, out_message) {
        var datetime = moment(o.val());
        var element = o.$element ? o.$element : o;
        if (!datetime.isValid()) {
            this.set_control_error(element, mes_error);
            if (out_message) this.out_error_message(mes_error);
            return false;
        } else {
            this.set_control_ok(element, mes_ok);
            if (out_message) this.out_info_message(mes_ok);
            return true;
        }
    };
    // Проверить элемент "datetime_input" на введенное значение (с подержкой пустого значения)
    validation_form.prototype.check_control_datetime_input_null = function (o, mes_error, mes_ok, out_message) {
        if (o.val() !== null && o.val() !== '') {
            var datetime = moment(o.val());
            if (!datetime.isValid()) {
                this.set_control_error(o.$element, mes_error);
                if (out_message) this.out_error_message(mes_error);
                return false;
            } else {
                this.set_control_ok(o.$element, mes_ok);
                if (out_message) this.out_info_message(mes_ok);
                return true;
            }
        } else {
            this.set_control_ok(o.$element, mes_ok);
            if (out_message) this.out_info_message(mes_ok);
            return true;
        }

    };

    App.validation_form = validation_form;

    //================================================================================
    // Класс вывода сообщений (Алерт)
    var alert_form = function ($alert) {
        if (!$alert) {
            throw new Error('Не указан элемент $alert');
        }
        if ($alert.length === 0) {
            throw new Error('Элемент $alert - неопределен');
        }
        this.$alert = $alert;
        //this.selector = this.$alert.attr('id');
        this.clear_message();
    };
    // Очистить сообщения
    alert_form.prototype.clear_message = function () {
        this.$alert.hide().text('').removeClass('alert-success alert-warning alert-danger');
    };
    // Вывести сообщение об ошибке
    alert_form.prototype.out_error_message = function (message) {
        this.$alert.show().removeClass('alert-success alert-warning').addClass('alert-danger');
        if (message) {
            this.$alert.append(message).append($('<br />'));
        }
    };
    // Вывести сообщение об ошибке
    alert_form.prototype.out_warning_message = function (message) {
        this.$alert.show().removeClass('alert-success alert-danger').addClass('alert-warning');
        if (message) {
            this.$alert.append(message).append($('<br />'));
        }
    };
    // Вывести информационное сообщение
    alert_form.prototype.out_info_message = function (message) {
        this.$alert.show().removeClass('alert-danger alert-warning').addClass('alert-success');
        if (message) {
            this.$alert.append(message).append($('<br />'));
        }
    };

    App.alert_form = alert_form;

    window.App = App;

})(window);