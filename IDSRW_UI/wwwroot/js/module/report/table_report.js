/*Модуль библиотека таблиц для отчета ТД*/
(function (window) {
    'use strict';

    var App = window.App || {};
    var $ = window.jQuery;

    var format_date = "YYYY-MM-DD";
    var format_time = "HH:mm:ss";
    var format_datetime = "YYYY-MM-DD HH:mm:ss";

    // Определим язык
    App.Lang = ($.cookie('lang') === undefined ? 'ru' : $.cookie('lang'));


    // Массив текстовых сообщений 
    $.Text_View =
    {
        'default':  //default language: ru
        {
            't_mtr_field_numeration': '№ п.п.',
            't_mtr_field_gruz_detali_etsng': 'Код ЕТСНГ',
            't_mtr_field_gruz_detali_nvs': 'Груз ПРИБ',
            't_mtr_field_km': 'Расстояние от станции дислокации поезда до станции назначения (км).',
            't_mtr_field_ves_gruz': 'Вес груза (в кг)',
            't_mtr_field_mnkua_opv': 'Мнемокод операции с вагоном вне поезда',
            't_mtr_field_kol_vag': 'Количество вагонов в поезде ст. отправления',
            't_mtr_field_st_otpr_detali_esr_otpr': 'Код ст. отправления',
            't_mtr_field_st_otpr_detali_n_rpus': 'Наименование ст. отправления',
            't_mtr_field_kod_grotp': 'Код грузоотправителя',
            't_mtr_field_mname_rv': 'Род вагона (мнемокод)',
            't_mtr_field_esr_form': 'Первая составляющая индекса поезда (код ЕСР станции формирования текущего индекса поезда)',
            't_mtr_field_st_nazn_detali_n_rpus': 'Станция назначения',
            't_mtr_field_st_nazn_detali_esr_nazn_vag': 'Код станции назначения',
            't_mtr_field_n_dorus': 'Наименование железной дороги (сокращенное)',
            't_mtr_field_date_pogr': 'Дата погрузки',
            't_mtr_field_kod_grp': 'Код грузополучателя',
            't_mtr_field_nom_sost': 'Вторая составляющая индекса поезда (Номер склада текущего индекса поезда)',
            't_mtr_field_date_op': 'Время совершения операции с объектом',
            't_mtr_field_stan_detali_n_rpus': 'Станция совершения операции',
            't_mtr_field_stan_detali_esr_op': 'Код ЕСР станции совершения операции',
            't_mtr_field_prog_cha_prib': 'Прогнозное время прибытия',
            't_mtr_field_kod_dor': 'Код железной дороги',
            't_mtr_field_rod_vag': 'Код рода вагона',
            't_mtr_field_esr_nazn': 'Третья составляющая индекса поезда (код ЕСР станции назначения текущего индекса поезда)',
            't_mtr_field_pr_nrp': 'Признак вагонов нерабочего парка',

            't_mtr_mess_init_module': 'Инициализация модуля (table_report) ...',
            't_mtr_mess_view_report': 'Показать отчет ...',


            't_mtr_title_all': 'Все',
            't_mtr_title_yes': 'Да',

            't_mtr_title_button_export': 'Экспорт',
            't_mtr_title_button_buffer': 'Буфер',
            't_mtr_title_button_excel': 'Excel',
            't_mtr_title_excel_sheet_name': 'Отчет',
            't_mtr_title_button_field': 'Поля',
            't_mtr_title_button_field_select': 'Выбрать',
            't_mtr_title_button_field_view_all': 'Показать все',
            't_mtr_title_button_field_clear': 'Сбросить',
        },
        'en':  //default language: English
        {
        }
    };
    // Определлим список текста для этого модуля
    App.Langs = $.extend(true, App.Langs, getLanguages($.Text_View, App.Lang));

    //var wsd = App.ids_wsd;
    // Модуль инициализаии компонентов формы
    var FE = App.form_element;

    var VICR = App.view_incoming_report; // Модуль отчетов по прибытию

    // Перечень полей
    var list_collums = [
        // Поля составы принятые
        {
            field: 'numeration',
            data: function (row, type, val, meta) {
                return ++meta.row;
            },
            className: 'dt-body-center',
            title: langView('t_mtr_field_numeration', App.Langs), width: "30px", orderable: true, searchable: false
        },
        {
            field: 'gruz_detali_etsng',
            data: function (row, type, val, meta) {
                return row.gruz!==null ? row.gruz.etsng : null;
            },
            className: 'dt-body-center',
            title: langView('t_mtr_field_gruz_detali_etsng', App.Langs), width: "50px", orderable: true, searchable: true
        },
        {
            field: 'gruz_detali_nvs',
            data: function (row, type, val, meta) {
                return row.gruz !== null ? row.gruz.nvs : null;
            },
            className: 'dt-body-left shorten mw-100',
            title: langView('t_mtr_field_gruz_detali_nvs', App.Langs), width: "100px", orderable: true, searchable: true
        },
        {
            field: 'km',
            data: function (row, type, val, meta) {
                return row.km;
            },
            className: 'dt-body-center',
            title: langView('t_mtr_field_km', App.Langs), width: "50px", orderable: true, searchable: true
        },
        {
            field: 'ves_gruz',
            data: function (row, type, val, meta) {
                return row.ves_gruz;
            },
            className: 'dt-body-center',
            title: langView('t_mtr_field_ves_gruz', App.Langs), width: "50px", orderable: true, searchable: true
        },
        {
            field: 'mnkua_opv',
            data: function (row, type, val, meta) {
                return row.mnkua_opv;
            },
            className: 'dt-body-left shorten mw-100',
            title: langView('t_mtr_field_mnkua_opv', App.Langs), width: "100px", orderable: true, searchable: true
        },
        {
            field: 'kol_vag',
            data: function (row, type, val, meta) {
                return row.kol_vag;
            },
            className: 'dt-body-center',
            title: langView('t_mtr_field_kol_vag', App.Langs), width: "50px", orderable: true, searchable: true
        },
        {
            field: 'st_otpr_detali_esr_otpr',
            data: function (row, type, val, meta) {
                return row.st_otpr !== null ? row.st_otpr.esr_otpr : null;
            },
            className: 'dt-body-center',
            title: langView('t_mtr_field_st_otpr_detali_esr_otpr', App.Langs), width: "50px", orderable: true, searchable: true
        },
        {
            field: 'st_otpr_detali_n_rpus',
            data: function (row, type, val, meta) {
                return row.st_otpr !== null ? row.st_otpr.n_rpus : null;
            },
            className: 'dt-body-left shorten mw-100',
            title: langView('t_mtr_field_st_otpr_detali_n_rpus', App.Langs), width: "100px", orderable: true, searchable: true
        },
        {
            field: 'kod_grotp',
            data: function (row, type, val, meta) {
                return row.kod_grotp;
            },
            className: 'dt-body-center',
            title: langView('t_mtr_field_kod_grotp', App.Langs), width: "50px", orderable: true, searchable: true
        },
        {
            field: 'mname_rv',
            data: function (row, type, val, meta) {
                return row.mname_rv;
            },
            className: 'dt-body-left shorten mw-50',
            title: langView('t_mtr_field_mname_rv', App.Langs), width: "50px", orderable: true, searchable: true
        },
        {
            field: 'esr_form',
            data: function (row, type, val, meta) {
                return row.esr_form;
            },
            className: 'dt-body-center',
            title: langView('t_mtr_field_esr_form', App.Langs), width: "50px", orderable: true, searchable: true
        },
        {
            field: 'st_nazn_detali_n_rpus',
            data: function (row, type, val, meta) {
                return row.st_nazn !== null ? row.st_nazn.n_rpus : null;
            },
            className: 'dt-body-left shorten mw-100',
            title: langView('t_mtr_field_st_nazn_detali_n_rpus', App.Langs), width: "100px", orderable: true, searchable: true
        },
        {
            field: 'st_nazn_detali_esr_nazn_vag',
            data: function (row, type, val, meta) {
                return row.st_nazn !== null ? row.st_nazn.esr_nazn_vag : null;
            },
            className: 'dt-body-center',
            title: langView('t_mtr_field_st_nazn_detali_esr_nazn_vag', App.Langs), width: "50px", orderable: true, searchable: true
        },
        {
            field: 'n_dorus',
            data: function (row, type, val, meta) {
                return row.n_dorus;
            },
            className: 'dt-body-left shorten mw-50',
            title: langView('t_mtr_field_n_dorus', App.Langs), width: "50px", orderable: true, searchable: true
        },
        {
            field: 'date_pogr',
            data: function (row, type, val, meta) {
                return row.date_pogr;
            },
            className: 'dt-body-left shorten mw-100',
            title: langView('t_mtr_field_date_pogr', App.Langs), width: "100px", orderable: true, searchable: true
        },
        {
            field: 'kod_grp',
            data: function (row, type, val, meta) {
                return row.kod_grp;
            },
            className: 'dt-body-center',
            title: langView('t_mtr_field_kod_grp', App.Langs), width: "50px", orderable: true, searchable: true
        },
        {
            field: 'nom_sost',
            data: function (row, type, val, meta) {
                return row.nom_sost;
            },
            className: 'dt-body-center',
            title: langView('t_mtr_field_nom_sost', App.Langs), width: "50px", orderable: true, searchable: true
        },
        {
            field: 'date_op',
            data: function (row, type, val, meta) {
                return row.date_op;
            },
            className: 'dt-body-left shorten mw-100',
            title: langView('t_mtr_field_date_op', App.Langs), width: "100px", orderable: true, searchable: true
        },
        {
            field: 'stan_detali_n_rpus',
            data: function (row, type, val, meta) {
                return row.stan !== null ? row.stan.n_rpus : null;
            },
            className: 'dt-body-left shorten mw-100',
            title: langView('t_mtr_field_stan_detali_n_rpus', App.Langs), width: "100px", orderable: true, searchable: true
        },
        {
            field: 'stan_detali_esr_op',
            data: function (row, type, val, meta) {
                return row.stan !== null ? row.stan.esr_op : null;
            },
            className: 'dt-body-center',
            title: langView('t_mtr_field_stan_detali_esr_op', App.Langs), width: "50px", orderable: true, searchable: true
        },
        {
            field: 'prog_cha_prib',
            data: function (row, type, val, meta) {
                return row.prog_cha_prib;
            },
            className: 'dt-body-left shorten mw-100',
            title: langView('t_mtr_field_prog_cha_prib', App.Langs), width: "100px", orderable: true, searchable: true
        },
        {
            field: 'kod_dor',
            data: function (row, type, val, meta) {
                return row.kod_dor;
            },
            className: 'dt-body-center',
            title: langView('t_mtr_field_kod_dor', App.Langs), width: "50px", orderable: true, searchable: true
        },
        {
            field: 'rod_vag',
            data: function (row, type, val, meta) {
                return row.rod_vag;
            },
            className: 'dt-body-center',
            title: langView('t_mtr_field_rod_vag', App.Langs), width: "50px", orderable: true, searchable: true
        },
        {
            field: 'esr_nazn',
            data: function (row, type, val, meta) {
                return row.esr_nazn;
            },
            className: 'dt-body-center',
            title: langView('t_mtr_field_esr_nazn', App.Langs), width: "50px", orderable: true, searchable: true
        },
        {
            field: 'pr_nrp',
            data: function (row, type, val, meta) {
                return row.pr_nrp;
            },
            className: 'dt-body-center',
            title: langView('t_mtr_field_pr_nrp', App.Langs), width: "50px", orderable: true, searchable: true
        },
    ];
    // Перечень кнопок
    var list_buttons = [
        {
            button: 'export',
            extend: 'collection',
            text: langView('t_mtr_title_button_export', App.Langs),
            buttons: [
                {
                    text: langView('t_mtr_title_button_buffer', App.Langs),
                    extend: 'copyHtml5',
                },
                {
                    text: langView('t_mtr_title_button_excel', App.Langs),
                    extend: 'excelHtml5',
                    sheetName: langView('t_mtr_title_excel_sheet_name', App.Langs),
                    messageTop: function () {
                        return '';
                    }
                },
            ],
            autoClose: true
        },
        {
            button: 'field',
            extend: 'collection',
            text: langView('t_mtr_title_button_field', App.Langs),
            buttons: [
                {
                    extend: 'colvis',
                    text: langView('t_mtr_title_button_field_select', App.Langs),
                    collectionLayout: 'fixed two-column',
                },
                {
                    extend: 'colvisGroup',
                    text: langView('t_mtr_title_button_field_view_all', App.Langs),
                    show: ':hidden'
                },
                {
                    text: langView('t_mtr_title_button_field_clear', App.Langs),
                    action: function (e, dt, node, conf) {
                        this.colReorder.reset();
                    }
                },
            ],
            autoClose: true
        },
        {
            button: 'print',
            extend: 'print',
        },
        {
            button: 'refresh',
            text: '<i class="fas fa-retweet"></i>',
        },
        {
            button: 'page_length',
            extend: 'pageLength',
        },
    ];
    //-----------------------------------------------------------------------------------------
    // Конструктор
    function table_report(selector) {
        if (!selector) {
            throw new Error('Не указан селектор');
        }
        this.$td_report = $(selector);
        if (this.$td_report.length === 0) {
            throw new Error('Не удалось найти элемент с селектором: ' + selector);
        }
        //this.fc_ui = new FC();
        this.fe_ui = new FE();
        this.selector = this.$td_report.attr('id');
    }
    //==========================================================================================
    //------------------------------- ПОЛЯ ----------------------------------------------------
    // инициализация полей по умолчанию
    table_report.prototype.init_columns_default= function () {
        var collums = [];
        collums.push({ field: 'numeration', title: null, class: null });
        // gruz
        return init_columns(collums, list_collums);
    };
    // инициализация полей adoption_sostav
    table_report.prototype.init_columns_req1892 = function () {
        var collums = [];
        collums.push({ field: 'numeration', title: null, class: null });
        collums.push({ field: 'gruz_detali_etsng', title: null, class: null });
        collums.push({ field: 'gruz_detali_nvs', title: null, class: null });
        collums.push({ field: 'km', title: null, class: null });
        collums.push({ field: 'ves_gruz', title: null, class: null });
        collums.push({ field: 'mnkua_opv', title: null, class: null });
        collums.push({ field: 'kol_vag', title: null, class: null });
        collums.push({ field: 'st_otpr_detali_esr_otpr', title: null, class: null });
        collums.push({ field: 'st_otpr_detali_n_rpus', title: null, class: null });
        collums.push({ field: 'kod_grotp', title: null, class: null });
        collums.push({ field: 'mname_rv', title: null, class: null });
        collums.push({ field: 'esr_form', title: null, class: null });
        collums.push({ field: 'st_nazn_detali_n_rpus', title: null, class: null });
        collums.push({ field: 'st_nazn_detali_esr_nazn_vag', title: null, class: null });
        collums.push({ field: 'n_dorus', title: null, class: null });
        collums.push({ field: 'date_pogr', title: null, class: null });
        collums.push({ field: 'kod_grp', title: null, class: null });
        collums.push({ field: 'nom_sost', title: null, class: null });
        collums.push({ field: 'date_op', title: null, class: null });
        collums.push({ field: 'stan_detali_n_rpus', title: null, class: null });
        collums.push({ field: 'stan_detali_esr_op', title: null, class: null });
        collums.push({ field: 'prog_cha_prib', title: null, class: null });
        collums.push({ field: 'kod_dor', title: null, class: null });
        collums.push({ field: 'rod_vag', title: null, class: null });
        collums.push({ field: 'esr_nazn', title: null, class: null });
        collums.push({ field: 'pr_nrp', title: null, class: null });
        return init_columns_detali(collums, list_collums);
    };
    //------------------------------- КНОПКИ ----------------------------------------------------
    // инициализация кнопок по умолчанию
    table_report.prototype.init_button_default = function () {
        var buttons = [];
        //buttons.push({ name: 'export', action: null });
        //buttons.push({ name: 'field', action: null });
        /*        buttons.push({ name: 'page_length', action: null });*/
        return init_buttons(buttons, list_buttons);
    };
    // инициализация кнопок adoption_sostav
    table_report.prototype.init_button_req1892 = function () {
        var buttons = [];
        buttons.push({ name: 'export', action: null });
        buttons.push({ name: 'print', action: null });
        buttons.push({ name: 'field', action: null });
        buttons.push({
            name: 'refresh',
            action: function (e, dt, node, config) {
                //this.action_refresh();
            }.bind(this)
        });
        buttons.push({ name: 'page_length', action: null });
        return init_buttons(buttons, list_buttons);
    };
    //-------------------------------------------------------------------------------------------
    // Инициализация тип отчета
    table_report.prototype.init_type_report = function () {
        switch (this.settings.type_report) {
            case 'req1892': {
                this.lengthMenu = [[10, 20, 50, 100, -1], [10, 20, 50, 100, langView('t_mtr_title_all', App.Langs)]];
                this.pageLength = 10;
                this.deferRender = true;
                this.paging = true;
                this.searching = true;
                this.ordering = true;
                this.info = true;
                this.fixedHeader = false;            // вкл. фикс. заголовка
                this.leftColumns = 0;
                this.columnDefs = null;
                this.order_column = [0, 'asc'];
                this.type_select_rows = 1; // Выбирать одну
                this.table_select = false;
                this.autoWidth = true;
                this.table_columns = this.init_columns_req1892();
                this.table_buttons = this.init_button_req1892();
                this.dom = 'Bfrtip';
                break;
            };
            // Таблица составы по умолчанию (если не выставят тип отчета)
            default: {
                this.fixedHeader = false;            // вкл. фикс. заголовка
                this.leftColumns = 0;
                this.order_column = [0, 'asc'];
                this.type_select_rows = 1; // Выбирать одну
                this.table_select = true;
                this.table_columns = this.init_columns_default();
                this.table_buttons = this.init_button_default();
                break;
            };
        }
    };
    // Инициализация
    table_report.prototype.init = function (options, fn_init_ok) {
        this.result_init = true;
        LockScreen(langView('t_mtr_mess_init_module', App.Langs));
        // теперь выполним инициализацию
        // Определим основные свойства
        this.settings = $.extend({
            alert: null,
            detali_table: false,
            type_report: null,     // 
            link_num: false,
        //    ids_wsd: null,
            fn_init: null,
            fn_select_rows: null,
            fn_action_view_wagons: null,
        }, options);
        //
        //this.ids_wsd = this.settings.ids_wsd ? this.settings.ids_wsd : new wsd();
        // Настройки отчета
        this.lengthMenu = null;
        this.pageLength = null;
        this.deferRender = true;
        this.paging = false;
        this.searching = false;
        this.ordering = false;
        this.info = false;
        this.fixedHeader = false;            // вкл. фикс. заголовка
        this.leftColumns = 0;
        this.columnDefs = null;
        this.order_column = [0, 'asc'];
        this.type_select_rows = 0; // не показывать
        this.table_select = false;
        this.drawCallback = null;
        this.footerCallback = null;
        this.autoWidth = false;
        this.table_columns = [];
        this.table_buttons = [];
        this.dom = 'Bfrtip';

        this.init_type_report();
        this.data = [];
        this.selected_rows = null;
        this.tables_detali = [];                    // Массив таблиц детально
        //----------------------------------
        // Создать макет таблицы
        var table_report = new this.fe_ui.table({
            id: 'tab-tr-' + this.selector,
            class: 'display compact cell-border row-border hover',
            title: null,
        });
        if (this.settings.type_report === 'req1892') {
            //this.$table_report = table_report.$table.append($('<tfoot><tr><th class="dt-right">ИТОГО:</th><td class="dt-centr"></td><td class="dt-centr"></td><td class="dt-centr"></td><td class="dt-centr"></td></tr></tfoot>'));
        }
        this.$table_report = table_report.$table;
        this.$td_report.addClass('table-report').append(this.$table_report);
        // Инициализируем таблицу
        this.obj_t_report = this.$table_report.DataTable({
            "lengthMenu": this.lengthMenu,
            "pageLength": this.pageLength,
            "deferRender": this.deferRender,
            "paging": this.paging,
            "searching": this.searching,
            "ordering": this.ordering,
            "info": this.info,
            "keys": true,
            columnDefs: this.columnDefs,
            colReorder: true,                       // вкл. перетаскивание полей
            fixedHeader: this.fixedHeader,          // вкл. фикс. заголовка
            fixedColumns: {
                leftColumns: this.leftColumns,
            },
            select: this.table_select,
            "autoWidth": this.autoWidth,
            //"filter": true,
            //"scrollY": "600px",
            //sScrollX: "100%",
            scrollX: true,
            /*            sScrollXInner: "100%",*/
            //"responsive": true,
            //"bAutoWidth": false,
            //order: this.order_column,
            language: language_table(App.Langs),
            jQueryUI: false,
            drawCallback: this.drawCallback,
            "createdRow": function (row, data, index) {
                switch (this.settings.type_report) {
                    case 'adoption_sostav': {
                        if (data.type === 0) {

                        } else {
                            $(row).addClass('yellow');
                        }
                        break;
                    };
                    case 'outgoing_sostav': {
                        if (data.type === 0) {

                        } else {
                            $(row).addClass('yellow');
                        }
                        break;
                    };
                    case 'incoming_outgoing_car': {

                        if (data.arrival_uz_vagon_cargo_returns) {
                            $(row).addClass('blue');
                        }
                        if (data.wir_highlight_color !== null) {
                            $(row).addClass('red');
                            //$(row).attr('style', 'background-color:' + data.wir_highlight_color + ' !important;');
                        }
                        break;
                    };
                    case 'outgoing_common_detali': {
                        if (data.arrival_uz_vagon_cargo_returns) {
                            $(row).addClass('red');
                        }
                        break;
                    };
                    case 'usage_fee_outgoing_cars': {
                        $(row).attr('id', data.outgoing_car_id);
                        break;
                    };
                };
            }.bind(this),
            footerCallback: this.footerCallback,
            columns: this.table_columns,
            dom: this.dom,
            stateSave: true,
            buttons: this.table_buttons,
        });
        // Обработка события выбора
        switch (this.settings.type_report) {
            case 'req1892': {
                this.obj_t_report.on('select deselect', function (e, dt, type, indexes) {
                    this.select_rows(); // определим строку
                    this.enable_button();
                    // Обработать событие выбрана строка
                    if (typeof this.settings.fn_select_rows === 'function') {
                        this.settings.fn_select_rows(this.selected_rows);
                    }
                }.bind(this));
                break;
            };
        };
        // На проверку окончания инициализации
        //----------------------------------
        if (typeof this.settings.fn_init === 'function') {
            this.settings.fn_init(this.result_init);
        }
        //----------------------------------
    };
    // Выбрано
    table_report.prototype.select_rows = function () {
        var index = this.obj_t_report.rows({ selected: true });
        var rows = this.obj_t_report.rows(index && index.length > 0 ? index[0] : null).data().toArray();
        this.selected_rows = rows;
        this.id_sostav = this.select_rows_sostav && this.select_rows_sostav.length > 0 ? this.select_rows_sostav[0].id : null;
    };
    // Отображение кнопки добавить
    table_report.prototype.enable_button = function () {
        switch (this.settings.type_report) {
            //case 'adoption_sostav': {
            //    if (this.select_rows_sostav && this.select_rows_sostav.length > 0) {
            //        this.obj_t_sostav.button(5).enable(true);
            //        if (this.select_rows_sostav[0].status < 1) {
            //            this.obj_t_sostav.button(3).enable(true);
            //            this.obj_t_sostav.button(4).enable(true); // отмена сдачи состава
            //            this.obj_t_sostav.button(5).text(langView('tis_title_button_wagon_accept', App.Langs));
            //        } else {
            //            // Если статус в работе принят или удален 
            //            this.obj_t_sostav.button(3).enable(true);
            //            this.obj_t_sostav.button(4).enable(false);
            //            //if (this.select_rows_sostav[0].status === 2) { this.obj_t_sostav.button(4).enable(true); } else { this.obj_t_sostav.button(4).enable(false); }
            //            this.obj_t_sostav.button(5).text(langView('tis_title_button_wagon_view', App.Langs));
            //        }
            //    } else {
            //        this.obj_t_sostav.button(3).enable(false);
            //        this.obj_t_sostav.button(4).enable(false);
            //        this.obj_t_sostav.button(5).enable(false);
            //    }
            //    break;
            //};
        };
    };
    // Показать данные
    table_report.prototype.view = function (data, id_select) {
        this.data = data;
        this.id_select = id_select;
        this.out_clear();
        LockScreen(langView('t_mtr_mess_view_report', App.Langs));
        this.obj_t_report.clear();
        this.obj_t_report.rows.add(data);
        this.obj_t_report.order(this.order_column);
        this.obj_t_report.draw();
        if (id_select !== null) {
            this.id_select = id_select
            this.obj_t_report.row('#' + this.id_select).select();
        } else {
            this.id_select = null;
        }
        this.view_footer(data);
        this.select_rows();
        this.enable_button();
    };
    //
    table_report.prototype.view_footer = function (data) {
        switch (this.settings.type_report) {
            case 'adoption_sostav': {
                if (data) {
                    var sum_count_wagon = 0;
                    var sum_count_wagon_all = 0;
                    var sum_count_return_wagon = 0;
                    var sum_count_return_wagon_all = 0;
                    var sum_count_account_balance = 0;
                    var sum_count_account_balance_all = 0;
                    var sum_count_not_operator = 0;
                    var sum_count_not_operator_all = 0;
                    $.each(data, function (i, el) {
                        if (el.type === 0) {
                            sum_count_wagon += el.count_wagon;
                            sum_count_account_balance += el.count_account_balance;
                            sum_count_not_operator += el.count_not_operator;
                            sum_count_return_wagon += el.count_return_wagon;
                        }
                        sum_count_wagon_all += el.count_wagon;
                        sum_count_account_balance_all += el.count_account_balance;
                        sum_count_not_operator_all += el.count_not_operator;
                        sum_count_return_wagon_all += el.count_return_wagon;
                    });
                }
                this.obj_t_report.columns('.sum_count_wagon').every(function () {
                    $(this.footer()).html(sum_count_wagon_all + '(' + sum_count_wagon + ')');
                });
                this.obj_t_report.columns('.sum_count_account_balance').every(function () {
                    $(this.footer()).html(sum_count_account_balance_all + '(' + sum_count_account_balance + ')');
                });
                this.obj_t_report.columns('.sum_count_not_operator').every(function () {
                    $(this.footer()).html(sum_count_not_operator_all + '(' + sum_count_not_operator + ')');
                });
                this.obj_t_report.columns('.sum_count_return_wagon').every(function () {
                    $(this.footer()).html(sum_count_return_wagon_all + '(' + sum_count_return_wagon + ')');
                });
                break;
            };
            case 'outgoing_sostav': {
                if (data) {
                    var sum_count_wagon = 0;
                    var sum_count_wagon_all = 0;
                    var sum_count_account_balance = 0;
                    var sum_count_account_balance_all = 0;
                    var sum_count_return_wagon = 0;
                    var sum_count_return_wagon_all = 0;
                    $.each(data, function (i, el) {
                        if (el.type === 0) {
                            sum_count_wagon += el.count_wagon;
                            sum_count_return_wagon += el.count_return_wagon;
                            sum_count_account_balance += el.count_account_balance;
                        }
                        sum_count_wagon_all += el.count_wagon;
                        sum_count_return_wagon_all += el.count_return_wagon;
                        sum_count_account_balance_all += el.count_account_balance;
                    });
                }
                this.obj_t_report.columns('.sum_count_wagon').every(function () {
                    $(this.footer()).html(sum_count_wagon_all + '(' + sum_count_wagon + ')');
                });
                this.obj_t_report.columns('.sum_count_account_balance').every(function () {
                    $(this.footer()).html(sum_count_account_balance_all + '(' + sum_count_account_balance + ')');
                });
                this.obj_t_report.columns('.sum_count_return_wagon').every(function () {
                    $(this.footer()).html(sum_count_return_wagon_all + '(' + sum_count_return_wagon + ')');
                });
                break;
            };
            case 'adoption_sostav_detali': {
                if (data) {
                    var sum_count_wagon = 0;
                    var sum_count_account_balance = 0;
                    var sum_count_not_operator = 0;
                    $.each(data, function (i, el) {
                        sum_count_wagon += el.count_wagon;
                        sum_count_account_balance += el.count_account_balance;
                        sum_count_not_operator += el.count_not_operator;
                    });
                }
                this.obj_t_report.columns('.sum_count_wagon').every(function () {
                    $(this.footer()).html(sum_count_wagon);
                });
                this.obj_t_report.columns('.sum_count_account_balance').every(function () {
                    $(this.footer()).html(sum_count_account_balance);
                });
                this.obj_t_report.columns('.sum_count_not_operator').every(function () {
                    $(this.footer()).html(sum_count_not_operator);
                });
                break;
            };
            case 'outgoing_sostav_detali': {
                if (data) {
                    var sum_count_wagon = 0;
                    var sum_count_account_balance = 0;
                    $.each(data, function (i, el) {
                        sum_count_wagon += el.count_wagon;
                        sum_count_account_balance += el.count_account_balance;
                    });
                }
                this.obj_t_report.columns('.sum_count_wagon').every(function () {
                    $(this.footer()).html(sum_count_wagon);
                });
                this.obj_t_report.columns('.sum_count_account_balance').every(function () {
                    $(this.footer()).html(sum_count_account_balance);
                });
                break;
            };
            case 'adoption_common_detali': {
                if (data) {
                    var sum_count_wagon = 0;
                    var sum_vesg = 0;
                    var sum_vesg_reweighing = 0;
                    var sum_vesg_deff = 0;
                    //var sum_gruzp = 0;

                    //var sum_count_account_balance = 0;
                    $.each(data, function (i, el) {
                        sum_count_wagon++;
                        sum_vesg += el.arrival_uz_vagon_vesg;
                        sum_vesg_reweighing += el.arrival_uz_vagon_vesg_reweighing;
                        sum_vesg_deff += el.arrival_uz_vagon_vesg && el.arrival_uz_vagon_vesg_reweighing ? Number(Number(Number(el.arrival_uz_vagon_vesg) - Number(el.arrival_uz_vagon_vesg_reweighing)) / 1000) : 0;
                        //sum_gruzp += el.arrival_uz_vagon_gruzp;
                    });
                    //var avg_vesg = sum_vesg / sum_count_wagon;
                    //var avg_gruzp = sum_gruzp / sum_count_wagon;
                }
                this.obj_t_report.columns('.fl-num').every(function () {
                    $(this.footer()).html(sum_count_wagon);
                });
                this.obj_t_report.columns('.fl-incoming_cars_arrival_uz_vagon_vesg').every(function () {
                    $(this.footer()).html(sum_vesg ? Number(sum_vesg / 1000).toFixed(2) : Number(0).toFixed(2));
                });
                this.obj_t_report.columns('.fl-incoming_cars_arrival_uz_vagon_vesg_reweighing').every(function () {
                    $(this.footer()).html(sum_vesg_reweighing ? Number(sum_vesg_reweighing / 1000).toFixed(2) : Number(0).toFixed(2));
                });
                this.obj_t_report.columns('.fl-incoming_cars_arrival_uz_vagon_deff_vesg').every(function () {
                    $(this.footer()).html(sum_vesg_deff ? Number(sum_vesg_deff / 1000).toFixed(2) : Number(0).toFixed(2));
                });
                break;
            };
            case 'outgoing_common_detali': {
                if (data) {
                    var sum_count_wagon = 0;
                    //var sum_vesg = 0;
                    var sum_vesg1 = 0;
                    var sum_idle_time = 0;
                    //var sum_idle_time_act = 0;
                    var downtime = 0;
                    var fee_amount = 0;
                    $.each(data, function (i, el) {
                        sum_vesg1 += el.outgoing_uz_vagon_vesg;
                        sum_count_wagon++;
                        //if (el && el.otpr && el.otpr.vagon && el.otpr.vagon.length > 0 && el.otpr.vagon[0].collect_v && el.otpr.vagon[0].collect_v.length > 0 && el.otpr.vagon[0].collect_v[0].vesg) {
                        //    sum_vesg += el.otpr.vagon[0].collect_v[0].vesg;
                        //}
                        sum_idle_time += el.idle_time !== null ? el.idle_time : 0;
                        //sum_idle_time_act += el.idle_time_act !== null ? el.idle_time_act : 0;
                        downtime += el.wagon_usage_fee_downtime !== null ? el.wagon_usage_fee_downtime : 0;
                        fee_amount += el.wagon_usage_fee_manual_fee_amount !== null ? el.wagon_usage_fee_manual_fee_amount : (el.wagon_usage_fee_calc_fee_amount !== null ? el.wagon_usage_fee_calc_fee_amount : 0);
                    });
                }
                this.obj_t_report.columns('.fl-num').every(function () {
                    $(this.footer()).html(sum_count_wagon);
                });
                //this.obj_t_report.columns('.fl-outgoing_cars_epd_vagon_collect_v_vesg').every(function () {
                //    $(this.footer()).html(sum_vesg ? Number(sum_vesg / 1000).toFixed(2) : Number(0).toFixed(2));
                //});
                this.obj_t_report.columns('.fl-outgoing_cars_outgoing_uz_vagon_vesg').every(function () {
                    $(this.footer()).html(sum_vesg1 ? Number(sum_vesg1 / 1000).toFixed(2) : Number(0).toFixed(2));
                });
                this.obj_t_report.columns('.fl-outgoing_cars_idle_time').every(function () {
                    $(this.footer()).html(sum_idle_time ? getTimeFromMins(sum_idle_time) : '00:00');
                });
                //this.obj_t_report.columns('.fl-outgoing_cars_idle_time_act').every(function () {
                //    $(this.footer()).html(sum_idle_time_act ? getTimeFromMins(sum_idle_time_act) : '00:00');
                //});
                this.obj_t_report.columns('.fl-outgoing_cars_wagon_usage_fee_downtime').every(function () {
                    $(this.footer()).html(downtime ? getTimeFromMins(downtime) : '0:00');
                });
                this.obj_t_report.columns('.fl-outgoing_cars_wagon_usage_fee_calc_fee_amount_final').every(function () {
                    $(this.footer()).html(fee_amount ? Number(fee_amount).toFixed(2) : Number(0).toFixed(2));
                });
                break;
            };
            case 'adoption_cargo_operation_amkr': {
                if (data) {
                    var sum_count_wagon = 0;
                    var sum_vesg = 0;
                    var sum_vesg_reweighing = 0;
                    var sum_vesg_deff = 0;

                    //var sum_count_account_balance = 0;
                    $.each(data, function (i, el) {
                        sum_count_wagon += el.count_wagon;
                        sum_vesg += el.sum_vesg;
                        sum_vesg_reweighing += el.sum_vesg_reweighing;
                        sum_vesg_deff += el.sum_vesg_deff;
                    });
                }
                this.obj_t_report.columns('.fl-total_count_wagon').every(function () {
                    $(this.footer()).html(sum_count_wagon);
                });
                this.obj_t_report.columns('.fl-total_sum_vesg').every(function () {
                    $(this.footer()).html(sum_vesg ? Number(sum_vesg / 1000).toFixed(2) : Number(0).toFixed(2));
                });
                this.obj_t_report.columns('.fl-total_sum_vesg_reweighing').every(function () {
                    $(this.footer()).html(sum_vesg_reweighing ? Number(sum_vesg_reweighing / 1000).toFixed(2) : Number(0).toFixed(2));
                });
                this.obj_t_report.columns('.fl-total_sum_vesg_deff').every(function () {
                    $(this.footer()).html(sum_vesg_deff ? Number(sum_vesg_deff / 1000).toFixed(2) : Number(0).toFixed(2));
                });
                break;
            };
            case 'adoption_operator_to_arr': {
                if (data) {
                    var sum_count_wagon = 0;
                    var sum_perent_wagon = 0;
                    $.each(data, function (i, el) {
                        sum_count_wagon += el.count_wagon;
                        sum_perent_wagon += (el.perent_wagon ? Number(el.perent_wagon) : 0);
                    });
                }
                this.obj_t_report.columns('.fl-total_count_wagon').every(function () {
                    $(this.footer()).html(sum_count_wagon);
                });
                this.obj_t_report.columns('.fl-total_perent_wagon').every(function () {
                    $(this.footer()).html(sum_perent_wagon ? Number(sum_perent_wagon).toFixed(1) : Number(0).toFixed(2));
                });
                break;
            };
            case 'adoption_cargo_to_arr': {
                if (data) {
                    var sum_count_wagon = 0;
                    var sum_vesg = 0;
                    var sum_vesg_reweighing = 0;
                    var sum_vesg_deff = 0;
                    //var sum_count_account_balance = 0;
                    $.each(data, function (i, el) {
                        sum_count_wagon += el.count_wagon;
                        sum_vesg += el.sum_vesg;
                        sum_vesg_reweighing += el.sum_vesg_reweighing;
                        sum_vesg_deff += el.sum_vesg_deff;
                    });
                }
                this.obj_t_report.columns('.fl-total_count_wagon').every(function () {
                    $(this.footer()).html(sum_count_wagon);
                });
                this.obj_t_report.columns('.fl-total_sum_vesg').every(function () {
                    $(this.footer()).html(sum_vesg ? Number(sum_vesg / 1000).toFixed(2) : Number(0).toFixed(2));
                });
                this.obj_t_report.columns('.fl-total_sum_vesg_reweighing').every(function () {
                    $(this.footer()).html(sum_vesg_reweighing ? Number(sum_vesg_reweighing / 1000).toFixed(2) : Number(0).toFixed(2));
                });
                this.obj_t_report.columns('.fl-total_sum_vesg_deff').every(function () {
                    $(this.footer()).html(sum_vesg_deff ? Number(sum_vesg_deff / 1000).toFixed(2) : Number(0).toFixed(2));
                });
                break;
            };
            case 'adoption_group_cargo_to_arr': {
                if (data) {
                    var sum_count_wagon = 0;
                    var sum_vesg = 0;
                    var sum_vesg_reweighing = 0;
                    var sum_vesg_deff = 0;
                    //var sum_count_account_balance = 0;
                    $.each(data, function (i, el) {
                        sum_count_wagon += el.count_wagon;
                        sum_vesg += el.sum_vesg;
                        sum_vesg_reweighing += el.sum_vesg_reweighing;
                        sum_vesg_deff += el.sum_vesg_deff;
                    });
                }
                this.obj_t_report.columns('.fl-total_count_wagon').every(function () {
                    $(this.footer()).html(sum_count_wagon);
                });
                this.obj_t_report.columns('.fl-total_sum_vesg').every(function () {
                    $(this.footer()).html(sum_vesg ? Number(sum_vesg / 1000).toFixed(2) : Number(0).toFixed(2));
                });
                this.obj_t_report.columns('.fl-total_sum_vesg_reweighing').every(function () {
                    $(this.footer()).html(sum_vesg_reweighing ? Number(sum_vesg_reweighing / 1000).toFixed(2) : Number(0).toFixed(2));
                });
                this.obj_t_report.columns('.fl-total_sum_vesg_deff').every(function () {
                    $(this.footer()).html(sum_vesg_deff ? Number(sum_vesg_deff / 1000).toFixed(2) : Number(0).toFixed(2));
                });
                break;
            };
            case 'adoption_genus_to_arr': {
                if (data) {
                    var sum_count_wagon = 0;
                    var sum_perent_wagon = 0;
                    //var sum_count_account_balance = 0;
                    $.each(data, function (i, el) {
                        sum_count_wagon += el.count_wagon;
                        sum_perent_wagon += (el.perent_wagon ? Number(el.perent_wagon) : 0);
                    });
                }
                this.obj_t_report.columns('.fl-total_count_wagon').every(function () {
                    $(this.footer()).html(sum_count_wagon);
                });
                this.obj_t_report.columns('.fl-total_perent_wagon').every(function () {
                    $(this.footer()).html(sum_perent_wagon ? Number(sum_perent_wagon).toFixed(1) : Number(0).toFixed(2));
                });
                break;
            };
            case 'adoption_cargo_sap_to_arr': {
                if (data) {
                    var sum_count_wagon = 0;
                    var sum_vesg = 0;
                    var sum_vesg_reweighing = 0;
                    var sum_vesg_deff = 0;
                    //var sum_count_account_balance = 0;
                    $.each(data, function (i, el) {
                        sum_count_wagon += el.count_wagon;
                        sum_vesg += el.sum_vesg;
                        sum_vesg_reweighing += el.sum_vesg_reweighing;
                        sum_vesg_deff += el.sum_vesg_deff;
                    });
                }
                this.obj_t_report.columns('.fl-total_count_wagon').every(function () {
                    $(this.footer()).html(sum_count_wagon);
                });
                this.obj_t_report.columns('.fl-total_sum_vesg').every(function () {
                    $(this.footer()).html(sum_vesg ? Number(sum_vesg / 1000).toFixed(2) : Number(0).toFixed(2));
                });
                this.obj_t_report.columns('.fl-total_sum_vesg_reweighing').every(function () {
                    $(this.footer()).html(sum_vesg_reweighing ? Number(sum_vesg_reweighing / 1000).toFixed(2) : Number(0).toFixed(2));
                });
                this.obj_t_report.columns('.fl-total_sum_vesg_deff').every(function () {
                    $(this.footer()).html(sum_vesg_deff ? Number(sum_vesg_deff / 1000).toFixed(2) : Number(0).toFixed(2));
                });
                break;
            };
            case 'adoption_station_to_arr': {
                if (data) {
                    var sum_count_wagon = 0;
                    var sum_vesg = 0;
                    var sum_vesg_reweighing = 0;
                    var sum_vesg_deff = 0;
                    //var sum_count_account_balance = 0;
                    $.each(data, function (i, el) {
                        sum_count_wagon += el.count_wagon;
                        sum_vesg += el.sum_vesg;
                        sum_vesg_reweighing += el.sum_vesg_reweighing;
                        sum_vesg_deff += el.sum_vesg_deff;
                    });
                }
                this.obj_t_report.columns('.fl-total_count_wagon').every(function () {
                    $(this.footer()).html(sum_count_wagon);
                });
                this.obj_t_report.columns('.fl-total_sum_vesg').every(function () {
                    $(this.footer()).html(sum_vesg ? Number(sum_vesg / 1000).toFixed(2) : Number(0).toFixed(2));
                });
                this.obj_t_report.columns('.fl-total_sum_vesg_reweighing').every(function () {
                    $(this.footer()).html(sum_vesg_reweighing ? Number(sum_vesg_reweighing / 1000).toFixed(2) : Number(0).toFixed(2));
                });
                this.obj_t_report.columns('.fl-total_sum_vesg_deff').every(function () {
                    $(this.footer()).html(sum_vesg_deff ? Number(sum_vesg_deff / 1000).toFixed(2) : Number(0).toFixed(2));
                });
                break;
            };
            case 'adoption_division_to_arr': {
                if (data) {
                    var sum_count_wagon = 0;
                    var sum_vesg = 0;
                    var sum_vesg_reweighing = 0;
                    var sum_vesg_deff = 0;
                    //var sum_count_account_balance = 0;
                    $.each(data, function (i, el) {
                        sum_count_wagon += el.count_wagon;
                        sum_vesg += el.sum_vesg;
                        sum_vesg_reweighing += el.sum_vesg_reweighing;
                        sum_vesg_deff += el.sum_vesg_deff;
                    });
                }
                this.obj_t_report.columns('.fl-total_count_wagon').every(function () {
                    $(this.footer()).html(sum_count_wagon);
                });
                this.obj_t_report.columns('.fl-total_sum_vesg').every(function () {
                    $(this.footer()).html(sum_vesg ? Number(sum_vesg / 1000).toFixed(2) : Number(0).toFixed(2));
                });
                this.obj_t_report.columns('.fl-total_sum_vesg_reweighing').every(function () {
                    $(this.footer()).html(sum_vesg_reweighing ? Number(sum_vesg_reweighing / 1000).toFixed(2) : Number(0).toFixed(2));
                });
                this.obj_t_report.columns('.fl-total_sum_vesg_deff').every(function () {
                    $(this.footer()).html(sum_vesg_deff ? Number(sum_vesg_deff / 1000).toFixed(2) : Number(0).toFixed(2));
                });
                break;
            };
            case 'adoption_to_gs': {
                if (data) {
                    var sum_count_wagon = 0;
                    var sum_vesg = 0;
                    var sum_vesg_reweighing = 0;
                    var sum_vesg_deff = 0;
                    //var sum_count_account_balance = 0;
                    $.each(data, function (i, el) {
                        sum_count_wagon += el.count_wagon;
                        sum_vesg += el.sum_vesg;
                        sum_vesg_reweighing += el.sum_vesg_reweighing;
                        sum_vesg_deff += el.sum_vesg_deff;
                    });
                }
                this.obj_t_report.columns('.fl-total_count_wagon').every(function () {
                    $(this.footer()).html(sum_count_wagon);
                });
                this.obj_t_report.columns('.fl-total_sum_vesg').every(function () {
                    $(this.footer()).html(sum_vesg ? Number(sum_vesg / 1000).toFixed(2) : Number(0).toFixed(2));
                });
                this.obj_t_report.columns('.fl-total_sum_vesg_reweighing').every(function () {
                    $(this.footer()).html(sum_vesg_reweighing ? Number(sum_vesg_reweighing / 1000).toFixed(2) : Number(0).toFixed(2));
                });
                this.obj_t_report.columns('.fl-total_sum_vesg_deff').every(function () {
                    $(this.footer()).html(sum_vesg_deff ? Number(sum_vesg_deff / 1000).toFixed(2) : Number(0).toFixed(2));
                });
                break;
            };
            case 'outgoing_cargo_operator': {
                if (data) {
                    var sum_count_wagon = 0;
                    var sum_vesg = 0;
                    //var sum_count_account_balance = 0;
                    $.each(data, function (i, el) {
                        sum_count_wagon += el.count_wagon;
                        sum_vesg += el.sum_vesg;
                    });
                }
                this.obj_t_report.columns('.fl-total_count_wagon').every(function () {
                    $(this.footer()).html(sum_count_wagon);
                });
                this.obj_t_report.columns('.fl-total_sum_vesg').every(function () {
                    $(this.footer()).html(sum_vesg ? Number(sum_vesg / 1000).toFixed(2) : Number(0).toFixed(2));
                });
                break;
            };
            case 'outgoing_cargo_ext_station': {
                if (data) {
                    var sum_count_wagon = 0;
                    var sum_vesg = 0;
                    //var sum_count_account_balance = 0;
                    $.each(data, function (i, el) {
                        sum_count_wagon += el.count_wagon;
                        sum_vesg += el.sum_vesg;
                    });
                }
                this.obj_t_report.columns('.fl-total_count_wagon').every(function () {
                    $(this.footer()).html(sum_count_wagon);
                });
                this.obj_t_report.columns('.fl-total_sum_vesg').every(function () {
                    $(this.footer()).html(sum_vesg ? Number(sum_vesg / 1000).toFixed(2) : Number(0).toFixed(2));
                });
                break;
            };
            case 'outgoing_total_division_metall': {
                if (data) {
                    var sum_count_wagon = 0;
                    var sum_vesg = 0;
                    //var sum_count_account_balance = 0;
                    $.each(data, function (i, el) {
                        sum_count_wagon += el.count_wagon;
                        sum_vesg += el.sum_vesg;
                    });
                }
                this.obj_t_report.columns('.fl-total_count_wagon').every(function () {
                    $(this.footer()).html(sum_count_wagon);
                });
                this.obj_t_report.columns('.fl-total_sum_vesg').every(function () {
                    $(this.footer()).html(sum_vesg ? Number(sum_vesg / 1000).toFixed(2) : Number(0).toFixed(2));
                });
                break;
            };
            case 'outgoing_total_division_cargo': {
                if (data) {
                    var sum_count_wagon = 0;
                    var sum_vesg = 0;
                    //var sum_count_account_balance = 0;
                    $.each(data, function (i, el) {
                        sum_count_wagon += el.count_wagon;
                        sum_vesg += el.sum_vesg;
                    });
                }
                this.obj_t_report.columns('.fl-total_count_wagon').every(function () {
                    $(this.footer()).html(sum_count_wagon);
                });
                this.obj_t_report.columns('.fl-total_sum_vesg').every(function () {
                    $(this.footer()).html(sum_vesg ? Number(sum_vesg / 1000).toFixed(2) : Number(0).toFixed(2));
                });
                break;
            };
            case 'outgoing_total_cargo_metall': {
                if (data) {
                    var sum_count_wagon = 0;
                    var sum_vesg = 0;
                    //var sum_count_account_balance = 0;
                    $.each(data, function (i, el) {
                        sum_count_wagon += el.count_wagon;
                        sum_vesg += el.sum_vesg;
                    });
                }
                this.obj_t_report.columns('.fl-total_count_wagon').every(function () {
                    $(this.footer()).html(sum_count_wagon);
                });
                this.obj_t_report.columns('.fl-total_sum_vesg').every(function () {
                    $(this.footer()).html(sum_vesg ? Number(sum_vesg / 1000).toFixed(2) : Number(0).toFixed(2));
                });
                break;
            };
            case 'outgoing_total_operators': {
                if (data) {
                    var sum_count_wagon = 0;
                    var sum_vesg = 0;
                    var sum_perent_wagon = 0;
                    var sum_idle_time = 0;
                    //var sum_count_account_balance = 0;
                    $.each(data, function (i, el) {
                        sum_count_wagon += el.count_wagon;
                        sum_vesg += el.sum_vesg;
                        sum_perent_wagon += (el.perent_wagon ? Number(el.perent_wagon) : 0);
                        sum_idle_time += (el.sum_idle_time ? Number(el.sum_idle_time) : 0);
                    });
                }
                this.obj_t_report.columns('.fl-total_count_wagon').every(function () {
                    $(this.footer()).html(sum_count_wagon);
                });
                this.obj_t_report.columns('.fl-total_sum_vesg').every(function () {
                    $(this.footer()).html(sum_vesg ? Number(sum_vesg / 1000).toFixed(2) : Number(0).toFixed(2));
                });
                this.obj_t_report.columns('.fl-total_perent_wagon').every(function () {
                    $(this.footer()).html(sum_perent_wagon ? Number(sum_perent_wagon).toFixed(1) : Number(0).toFixed(2));
                });
                this.obj_t_report.columns('.fl-total_sum_idle_time').every(function () {
                    $(this.footer()).html(getTimeFromMins(sum_idle_time));
                });
                this.obj_t_report.columns('.fl-total_wagon_idle_time').every(function () {
                    $(this.footer()).html(getTimeFromMins(Number(Number(sum_idle_time / sum_count_wagon).toFixed(0))));
                });
                break;
            };
            case 'outgoing_total_operators_cargo': {
                if (data) {
                    var sum_count_wagon = 0;
                    var sum_vesg = 0;
                    //var sum_perent_wagon = 0;
                    //var sum_count_account_balance = 0;
                    $.each(data, function (i, el) {
                        sum_count_wagon += el.count_wagon;
                        sum_vesg += el.sum_vesg;
                        //sum_perent_wagon += (el.perent_wagon ? Number(el.perent_wagon) : 0);
                    });
                }
                this.obj_t_report.columns('.fl-total_count_wagon').every(function () {
                    $(this.footer()).html(sum_count_wagon);
                });
                this.obj_t_report.columns('.fl-total_sum_vesg').every(function () {
                    $(this.footer()).html(sum_vesg ? Number(sum_vesg / 1000).toFixed(2) : Number(0).toFixed(2));
                });
                //this.obj_t_report.columns('.fl-total_perent_wagon').every(function () {
                //    $(this.footer()).html(sum_perent_wagon ? Number(sum_perent_wagon).toFixed(1) : Number(0).toFixed(2));
                //});
                break;
            };
            case 'outgoing_total_ext_station': {
                if (data) {
                    var sum_count_wagon = 0;
                    var sum_vesg = 0;
                    //var sum_count_account_balance = 0;
                    $.each(data, function (i, el) {
                        sum_count_wagon += el.count_wagon;
                        sum_vesg += el.sum_vesg;
                    });
                }
                this.obj_t_report.columns('.fl-total_count_wagon').every(function () {
                    $(this.footer()).html(sum_count_wagon);
                });
                this.obj_t_report.columns('.fl-total_sum_vesg').every(function () {
                    $(this.footer()).html(sum_vesg ? Number(sum_vesg / 1000).toFixed(2) : Number(0).toFixed(2));
                });
                break;
            };
            case 'usage_fee_cargo': {
                if (data) {
                    var sum_count_wagon = 0;
                    var sum_usage_fee_sum_calc_time = 0;
                    var sum_usage_fee_sum_calc_fee_amount = 0;
                    var sum_usage_fee_wagon_persent_fee_amount = 0;
                    var usage_fee_wagon_calc_time = 0;
                    var usage_fee_wagon_calc_fee_amount = 0;

                    //var sum_count_account_balance = 0;
                    $.each(data, function (i, el) {
                        sum_count_wagon += el.count_wagon;
                        sum_usage_fee_sum_calc_time += el.sum_calc_time;
                        sum_usage_fee_sum_calc_fee_amount += el.sum_calc_fee_amount;
                        sum_usage_fee_wagon_persent_fee_amount += el.persent;
                    });
                }
                usage_fee_wagon_calc_time = sum_count_wagon > 0 ? getHourFromMins(Number(sum_usage_fee_sum_calc_time / sum_count_wagon)).toFixed(0) : 0;
                usage_fee_wagon_calc_fee_amount = sum_count_wagon > 0 ? Number(sum_usage_fee_sum_calc_fee_amount / sum_count_wagon).toFixed(2) : 0.00;
                this.obj_t_report.columns('.fl-total_count_wagon').every(function () {
                    $(this.footer()).html(sum_count_wagon);
                });
                this.obj_t_report.columns('.fl-usage_fee_sum_calc_time').every(function () {
                    $(this.footer()).html(getHourFromMins(sum_usage_fee_sum_calc_time));
                });
                this.obj_t_report.columns('.fl-usage_fee_wagon_calc_time').every(function () {
                    $(this.footer()).html(usage_fee_wagon_calc_time);
                });
                this.obj_t_report.columns('.fl-usage_fee_sum_calc_fee_amount').every(function () {
                    $(this.footer()).html(Number(sum_usage_fee_sum_calc_fee_amount).toFixed(2));
                });
                this.obj_t_report.columns('.fl-usage_fee_wagon_calc_fee_amount').every(function () {
                    $(this.footer()).html(usage_fee_wagon_calc_fee_amount);
                });
                this.obj_t_report.columns('.fl-usage_fee_wagon_persent_fee_amount').every(function () {
                    $(this.footer()).html(Number(sum_usage_fee_wagon_persent_fee_amount).toFixed(0));
                });
                break;
            };
            case 'usage_fee_cargo_not_derailment': {
                if (data) {
                    var sum_count_wagon = 0;
                    var sum_usage_fee_sum_calc_time = 0;
                    var sum_usage_fee_sum_calc_fee_amount = 0;
                    var sum_usage_fee_wagon_persent_fee_amount = 0;
                    var usage_fee_wagon_calc_time = 0;
                    var usage_fee_wagon_calc_fee_amount = 0;

                    //var sum_count_account_balance = 0;
                    $.each(data, function (i, el) {
                        sum_count_wagon += el.count_wagon;
                        sum_usage_fee_sum_calc_time += el.sum_calc_time;
                        sum_usage_fee_sum_calc_fee_amount += el.sum_calc_fee_amount;
                        sum_usage_fee_wagon_persent_fee_amount += el.persent_not_derailment;
                    });
                }
                usage_fee_wagon_calc_time = sum_count_wagon > 0 ? getHourFromMins(Number(sum_usage_fee_sum_calc_time / sum_count_wagon)).toFixed(0) : 0;
                usage_fee_wagon_calc_fee_amount = sum_count_wagon > 0 ? Number(sum_usage_fee_sum_calc_fee_amount / sum_count_wagon).toFixed(2) : 0.00;
                this.obj_t_report.columns('.fl-total_count_wagon').every(function () {
                    $(this.footer()).html(sum_count_wagon);
                });
                this.obj_t_report.columns('.fl-usage_fee_sum_calc_time').every(function () {
                    $(this.footer()).html(getHourFromMins(sum_usage_fee_sum_calc_time));
                });
                this.obj_t_report.columns('.fl-usage_fee_wagon_calc_time').every(function () {
                    $(this.footer()).html(usage_fee_wagon_calc_time);
                });
                this.obj_t_report.columns('.fl-usage_fee_sum_calc_fee_amount').every(function () {
                    $(this.footer()).html(Number(sum_usage_fee_sum_calc_fee_amount).toFixed(2));
                });
                this.obj_t_report.columns('.fl-usage_fee_wagon_calc_fee_amount').every(function () {
                    $(this.footer()).html(usage_fee_wagon_calc_fee_amount);
                });
                this.obj_t_report.columns('.fl-usage_fee_wagon_persent_not_derailment_fee_amount').every(function () {
                    $(this.footer()).html(Number(sum_usage_fee_wagon_persent_fee_amount).toFixed(0));
                });
                break;
            };
            case 'usage_fee_operator_amkr': {
                if (data) {
                    var sum_count_wagon = 0;
                    var sum_usage_fee_sum_calc_time = 0;
                    var sum_usage_fee_sum_calc_fee_amount = 0;
                    var sum_usage_fee_wagon_persent_fee_amount = 0;
                    var usage_fee_wagon_calc_time = 0;
                    var usage_fee_wagon_calc_fee_amount = 0;

                    //var sum_count_account_balance = 0;
                    $.each(data, function (i, el) {
                        sum_count_wagon += el.count_wagon;
                        sum_usage_fee_sum_calc_time += el.sum_calc_time;
                        sum_usage_fee_sum_calc_fee_amount += el.sum_calc_fee_amount;
                        sum_usage_fee_wagon_persent_fee_amount += el.persent;
                    });
                }
                usage_fee_wagon_calc_time = sum_count_wagon > 0 ? getHourFromMins(Number(sum_usage_fee_sum_calc_time / sum_count_wagon)).toFixed(0) : 0;
                usage_fee_wagon_calc_fee_amount = sum_count_wagon > 0 ? Number(sum_usage_fee_sum_calc_fee_amount / sum_count_wagon).toFixed(2) : 0.00;
                this.obj_t_report.columns('.fl-total_count_wagon').every(function () {
                    $(this.footer()).html(sum_count_wagon);
                });
                this.obj_t_report.columns('.fl-usage_fee_sum_calc_time').every(function () {
                    $(this.footer()).html(getHourFromMins(sum_usage_fee_sum_calc_time));
                });
                this.obj_t_report.columns('.fl-usage_fee_wagon_calc_time').every(function () {
                    $(this.footer()).html(usage_fee_wagon_calc_time);
                });
                this.obj_t_report.columns('.fl-usage_fee_sum_calc_fee_amount').every(function () {
                    $(this.footer()).html(Number(sum_usage_fee_sum_calc_fee_amount).toFixed(2));
                });
                this.obj_t_report.columns('.fl-usage_fee_wagon_calc_fee_amount').every(function () {
                    $(this.footer()).html(usage_fee_wagon_calc_fee_amount);
                });
                this.obj_t_report.columns('.fl-usage_fee_wagon_persent_fee_amount').every(function () {
                    $(this.footer()).html(Number(sum_usage_fee_wagon_persent_fee_amount).toFixed(0));
                });
                break;
            };
            case 'usage_fee_operator_amkr_derailment': {
                if (data) {
                    var sum_count_wagon = 0;
                    var sum_usage_fee_sum_calc_time = 0;
                    var sum_usage_fee_sum_calc_fee_amount = 0;
                    var sum_usage_fee_wagon_persent_fee_amount = 0;
                    var usage_fee_wagon_calc_time = 0;
                    var usage_fee_wagon_calc_fee_amount = 0;

                    //var sum_count_account_balance = 0;
                    $.each(data, function (i, el) {
                        sum_count_wagon += el.count_wagon;
                        sum_usage_fee_sum_calc_time += el.sum_calc_time;
                        sum_usage_fee_sum_calc_fee_amount += el.sum_calc_fee_amount;
                        sum_usage_fee_wagon_persent_fee_amount += el.persent_derailment;
                    });
                }
                usage_fee_wagon_calc_time = sum_count_wagon > 0 ? getHourFromMins(Number(sum_usage_fee_sum_calc_time / sum_count_wagon)).toFixed(0) : 0;
                usage_fee_wagon_calc_fee_amount = sum_count_wagon > 0 ? Number(sum_usage_fee_sum_calc_fee_amount / sum_count_wagon).toFixed(2) : 0.00;
                this.obj_t_report.columns('.fl-total_count_wagon').every(function () {
                    $(this.footer()).html(sum_count_wagon);
                });
                this.obj_t_report.columns('.fl-usage_fee_sum_calc_time').every(function () {
                    $(this.footer()).html(getHourFromMins(sum_usage_fee_sum_calc_time));
                });
                this.obj_t_report.columns('.fl-usage_fee_wagon_calc_time').every(function () {
                    $(this.footer()).html(usage_fee_wagon_calc_time);
                });
                this.obj_t_report.columns('.fl-usage_fee_sum_calc_fee_amount').every(function () {
                    $(this.footer()).html(Number(sum_usage_fee_sum_calc_fee_amount).toFixed(2));
                });
                this.obj_t_report.columns('.fl-usage_fee_wagon_calc_fee_amount').every(function () {
                    $(this.footer()).html(usage_fee_wagon_calc_fee_amount);
                });
                this.obj_t_report.columns('.fl-usage_fee_wagon_persent_derailment_fee_amount').every(function () {
                    $(this.footer()).html(Number(sum_usage_fee_wagon_persent_fee_amount).toFixed(0));
                });
                break;
            };
            case 'residue_total_operators': {
                if (data) {
                    var sum_start = 0;
                    var sum_arrival = 0;
                    var sum_outgoing = 0;
                    var sum_stop = 0;
                    //var sum_count_account_balance = 0;
                    $.each(data, function (i, el) {
                        sum_start += el.start;
                        sum_arrival += el.arrival;
                        sum_outgoing += el.outgoing;
                        sum_stop += el.stop;
                    });
                }
                this.obj_t_report.columns('.fl-residue_total_operators_start').every(function () {
                    $(this.footer()).html(sum_start);
                });
                this.obj_t_report.columns('.fl-residue_total_operators_arrival').every(function () {
                    $(this.footer()).html(sum_arrival);
                });
                this.obj_t_report.columns('.fl-residue_total_operators_outgoing').every(function () {
                    $(this.footer()).html(sum_outgoing);
                });
                this.obj_t_report.columns('.fl-residue_total_operators_stop').every(function () {
                    $(this.footer()).html(sum_stop);
                });
                break;
            };
            case 'residue_total_common': {
                if (data) {
                    var sum_total = 0;
                    var sum_external = 0;
                    var sum_paid = 0;
                    var sum_accounting = 0;
                    var sum_amkr = 0;
                    var count = 0
                    //var sum_count_account_balance = 0;
                    $.each(data, function (i, el) {
                        sum_total += el.total;
                        sum_external += el.external;
                        sum_paid += el.paid;
                        sum_accounting += el.accounting;
                        sum_amkr += el.amkr;
                        count++;
                    });
                }
                this.obj_t_report.columns('.fl-residue_total_common_total').every(function () {
                    $(this.footer()).html(count > 0 ? Number(sum_total / count).toFixed(0) : 0);
                });
                this.obj_t_report.columns('.fl-residue_total_common_external').every(function () {
                    $(this.footer()).html(count > 0 ? Number(sum_external / count).toFixed(0) : 0);
                });
                this.obj_t_report.columns('.fl-residue_total_common_paid').every(function () {
                    $(this.footer()).html(count > 0 ? Number(sum_paid / count).toFixed(0) : 0);
                });
                this.obj_t_report.columns('.fl-residue_total_common_accounting').every(function () {
                    $(this.footer()).html(count > 0 ? Number(sum_accounting / count).toFixed(0) : 0);
                });
                this.obj_t_report.columns('.fl-residue_total_common_amkr').every(function () {
                    $(this.footer()).html(count > 0 ? Number(sum_amkr / count).toFixed(0) : 0);
                });
                break;
            };

            case 'residue_total_markup_arr':
            case 'residue_total_markup_curr':
            case 'residue_total_genus': {
                if (data) {
                    var sum_count_wagon = 0;
                    var sum_perent_wagon = 0;
                    $.each(data, function (i, el) {
                        sum_count_wagon += el.count_wagon;
                        sum_perent_wagon += (el.perent_wagon ? Number(el.perent_wagon) : 0);
                    });
                }
                this.obj_t_report.columns('.fl-total_count_wagon').every(function () {
                    $(this.footer()).html(sum_count_wagon);
                });
                this.obj_t_report.columns('.fl-total_perent_wagon').every(function () {
                    $(this.footer()).html(sum_perent_wagon ? Number(sum_perent_wagon).toFixed(1) : Number(0).toFixed(2));
                });
                break;
            };
            case 'residue_total_markup_operator':
            case 'residue_total_genus_station_amkr':
            case 'residue_total_station_out': {
                if (data) {
                    var sum_count_wagon = 0;
                    var sum_perent_wagon = 0;
                    //var sum_count_account_balance = 0;
                    $.each(data, function (i, el) {
                        sum_count_wagon += el.count_wagon;
                        sum_perent_wagon += (el.perent_wagon !== null ? Number(el.perent_wagon) : 0);
                    });
                }
                this.obj_t_report.columns('.fl-total_count_wagon').every(function () {
                    $(this.footer()).html(sum_count_wagon);
                });
                this.obj_t_report.columns('.fl-total_perent_wagon').every(function () {
                    $(this.footer()).html(Number(sum_perent_wagon).toFixed(0));
                });
                break;
            };
        };
    };
    //-------------------------------------------------------------------------------------------
    // Очистить сообщения
    table_report.prototype.out_clear = function () {
        if (this.settings.alert) {
            this.settings.alert.clear_message()
        }
    }
    // Показать ошибки
    table_report.prototype.out_error = function (message) {
        if (this.settings.alert) {
            this.settings.alert.out_error_message(message)
        }
    };
    // Показать предупреждения
    table_report.prototype.out_warning = function (message) {
        if (this.settings.alert) {
            this.settings.alert.out_warning_message(message)
        }
    };
    // Показать сообщения о выполнении действий
    table_report.prototype.out_info = function (message) {
        if (this.settings.alert) {
            this.settings.alert.out_info_message(message)
        }
    };
    // Очистить объект
    table_report.prototype.destroy = function () {
        //
        if (this.obj_t_report) {
            this.obj_t_report.destroy(true);
            this.obj_t_report = null;
        }
        this.$td_report.empty(); // empty in case the columns change
    };
    // Очистить детали по указаному пути
    table_report.prototype.destroy_detali = function (data) {
        if (this.tables_detali[data.id]) {
            this.tables_detali[data.id].destroy();
            delete this.tables_detali[data.id];
        }
    };
    // Очистить все детали
    table_report.prototype.destroy_all_detali = function () {
        $.each(this.tables_detali, function (i, el) {
            if (el) {
                el.destroy();
            }
        }.bind(this));
        this.tables_detali = {};
    };
    //
    App.table_report = table_report;

    window.App = App;
})(window);