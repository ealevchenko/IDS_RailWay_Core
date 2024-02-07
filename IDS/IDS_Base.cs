namespace IDS_
{
    public enum errors_base : int
    {
        global = -1,
        cancel_save_changes = -2,                   // Отмена сохранений изменений в базе данных (были ошибки по ходу выполнения всей операции)
        error_save_changes = -3,                    // Были ошибки по ходу выполнения всей операций


        // Обшие входные переменые -100...
        not_input_value = -100,
        not_input_list_wagons = -101,               // Ошибка, нет списка вагонов
        error_date = -102,                          // Ошибка, дата не прошла валидацию
        input_position_error = -103,                // Ошибка, позиция указана не правильно


        // таблица wir -200...
        not_wir_db = -201,                          // В базе данных нет записи по WagonInternalRoutes (Внутреннее перемещение вагонов)
        close_wir = -202,                           // Записи по WagonInternalRoutes - закрыта
        outgoing_cars_wir = -203,                   // Записи по WagonInternalRoutes - уже имеет ссылку на отправку
        arrival_cars_wir = -204,                    // Записи по WagonInternalRoutes - уже имеет ссылку на прибытие (Состав уже принят)
        not_open_wir = -205,                        // В базе данных нет открытой записи по WagonInternalRoutes (Внутреннее перемещение вагонов)
        open_wir = -206,                            // Записи по WagonInternalRoutes - открыта

        // таблица wim -300...
        not_wim_db = -301,                          // В базе данных нет записи по WagonInternalMovement (Внутренняя дислокация вагонов)
        wagon_not_way = -302,                       // Вагон не стоит на пути
        wagon_not_outerway = -303,                  // Вагон не стоит на перегоне
        err_create_wim_db = -304,                   // Ошибка создания новой позиции вагона.

        // таблица wio -400...
        not_wio_db = -401,                          // В базе данных нет записи по WagonInternalOperation (Внутренняя операция по вагону)
        look_operation = -402,                      // Операция над вагонами заблокирована (Вагон предъявлен на УЗ)
        wagon_not_operation = -403,                 // Операция вагона не соответсвует выбраной
        wagon_lock_operation = -404,                // Операция вагона имеет статус блокировки
        err_create_wio_db = -405,                   // Ошибка создания новой операции над вагоном.
        already_wio = -406,                         // Операция уже применена.
        not_arrival_operation = -407,               // Операция вагона текущая операция вагона не "Прибытие с УЗ"

        // таблицы прибытие -500..
        not_arrival_sostav_db = -501,              // В базе данных нет записи состава для оправки
        error_status_arrival_sostav = -502,        // Ошибка статуса состава (Статус не позволяет сделать эту операцию)
        not_arrival_cars_db = -505,                // Ошибка, нет записи вагона по прибытию 
        arrival_cars_arrival = -506,               // Запрет операции вагон уже принят
        not_arrival_cars_arrival = -507,           // Запрет операции вагон еще не принят
        arrival_cars_adoption = -508,              // Запрет операции вагон(ы) прибывшего состава уже приняты
        arrival_cars_num_main_doc = -509,          // По вагону неопределен основной докумен уз
        arrival_cars_num_doc = -510,               // По вагону неопределен досылочный докумен уз
        not_arrival_uz_vagon = -511,               // Ошибка, нет записи или ссылки на документ прибывшего вагона  
        not_last_manual_epd = -512,                // Ошибка не получен последний номер автоматического ручного документа MA:xxxxx 
        exist_manual_epd = -513,                   // Ошибка ЭПД автоматического ручного документа MA:xxxxx - уже существует
        not_main_uz_doc_db = -514,                 // Ошибка в базе данных отсутсвует основной ЭПД 
        not_uz_doc_db = -515,                      // Ошибка в базе данных отсутсвует досылочный ЭПД 
        exist_not_manual_uz_doc_db = -516,         // Ошибка в базе данных указаный ЭПД введен не ручном режиме
        exist_not_manual_main_uz_doc_db = -517,    // Ошибка в базе данных указаный основной ЭПД введен не ручном режиме


        // документы по прибытию
        not_inp_uz_vag_db = -551,                   // В базе данных нет записи документа на вагон.
        not_inp_uz_doc_db = -552,                   // В базе данных нет записи документа на состав.

        // документы по прибытию
        error_update_arr_doc_pay = -561,            // Ошибка обновления документов (платильщики)
        error_update_arr_doc_act = -562,            // Ошибка обновления документов (акты)
        error_update_arr_doc_doc = -563,            // Ошибка обновления документов (документы)
        error_update_arr_vag = -564,                // Ошибка обновления документов (на вагон)
        error_update_arr_vag_pay = -565,            // Ошибка обновления документов (платежки на вагон)
        error_update_arr_vag_acts = -566,           // Ошибка обновления документов (акты на вагон)
        error_update_arr_vag_cont = -567,           // Ошибка обновления документов (контейнера на вагон)
        error_update_arr_cont_pay = -568,           // Ошибка обновления документов (контейнера на вагон)

        // таблицы отправки -600..      
        not_outgoing_sostav_db = -601,              // В базе данных нет записи состава для оправки
        error_status_outgoing_sostav = -602,        // Ошибка статуса состава (Статус не позволяет сделать эту операцию)
        not_outgoing_cars_db = -605,                // В базе данных нет записи по вагонам для отправки
        outgoing_cars_outgoing = -606,              // Запрет операции вагон уже отправлен
        not_doc_uz_outgoing_cars = -607,            // В вагоне на отправку неопределен id документа УЗ СМС

        not_outgoing_detention_return_db = -610,    // В базе данных нет записи OutgoingDetentionReturn
        close_outgoing_detention_return = -611,     // В базе данных нет запись OutgoingDetentionReturn закрыта
        not_uz_doc_out_db = -612,                   // В базе данных нет записи документа ЭПД по отправке

        // документы на отправку
        exist_out_uz_vag = -651,                    // Запрет операции, строка по вагону уже создана.
        not_out_uz_vag_db = -652,                   // В базе данных нет записи документа на вагон.

        error_update_out_doc = -660,                        // Ошибка обновления документов (документ на группу вагонов)
        error_update_out_doc_pay = -661,                    // Ошибка обновления документов (платежки на документ на группу вагонов)                                                // 
        error_update_out_vag = -662,                        // Ошибка обновления документов (на вагон)
        error_update_out_vag_pay = -663,                    // Ошибка обновления документов (платежки на вагон)
        error_update_out_vag_acts = -664,                   // Ошибка обновления документов (акты на вагон)
        error_update_out_vag_cont = -665,                   // Ошибка обновления документов (контейнера на вагон)
        error_update_out_cont_pay = -666,                   // Ошибка обновления документов (контейнера на вагон)

        // Таблицы Положение парка -700..
        not_park_station_station_of_db = -701,              // Ошибка, в базе данных нет строки положения парка по станции
        not_way_park_station_station_of_db = -702,          // Ошибка, в базе данных нет строки пути положения парка по станции
        not_list_way_park_station_station_of_db = -703,     // Ошибка, в базе данных нет списка путей положения парка по станции
        error_date_park_station_station = -704,             // Ошибка даты, попытка создать положение парка с неправильной датой
        error_delete_park_station_apply = -705,             // Отмена удаления, состояние парка уже применили
        error_change_park_station_lock_wagon = -706,        // Отмена изменения положения парка, вагоны имеют отметку заблокирован (операция предъявления)

        // Таблицы SAP -800..
        not_sap_is_db = -801,                               // В базе данных нет записи по SAPIncomingSupply (SAP Входящая поставка)
        not_sap_os_db = -802,                               // В базе данных нет записи по SAPOutgoingSupply (SAP Исходящая поставка)

        // СМС
        error_connect_sms = -900,                           // Ошибка Подкллючения к модулю согласования

        error_convert_epd = -950,                           // Ошибка конвертации данных ЭПД
        not_epd_document = -951,                            // Нет ЭПД
        not_vagon_epd_document = -952,                      // Указанного вагона нет в ЭПД

        // Справочники -1000.....

        // Directory_Ways -1100..
        not_dir_way_of_db = -1101,                          // В базе данных нет записи указанной строки пути
        not_dir_park_station_of_db = -1102,                 // В базе данных нет записи указанного парка по указаной станции
        way_not_crossing_uz = -1103,                        // Путь неимеет выход на УЗ
        way_is_not_null = -1104,                            // На пути стоят вагоны
        way_is_close = -1105,                               // Путь закрыт
        way_is_delete = -1106,                              // Путь удален и неиспользуется

        // Directory_ParkWays -1200..
        not_dir_park_of_db = -1201,                         // В базе данных нет указаного парка

        // Directory_Station -1300..
        not_dir_station_of_db = -1301,                      // В базе данных нет указаной станции

        // Directory_Wagons -1400..
        not_dir_wagon_of_db = -1401,                        // В базе данных нет записи указанной строки вагона
        error_sys_numeration_wagon = -1402,                // Ошибка системной нумерации вагона
        error_numeration_wagon = -1403,                    // Ошибка нумерации вагона (- или =0)
        exists_dir_wagon_of_db = -1404,                     // В базе данных уже есть запись по указаному вагону

        // Directory_OuterWays -1500..
        not_dir_outerways_of_db = -1501,                    // В базе данных нет записи указаного перегона


    }

    public class ChangeID
    {
        public long id_old { get; set; }
        public long id_new { get; set; }
    }
    /// <summary>
    /// 
    /// </summary>
    public class ResultID
    {
        public long id { get; set; }
        public int result { get; set; }
        public int? type { get; set; }
    }
    public class OperationResultID
    {
        public int result { get; set; } // Глобальный ресурс выполнения всего переноса
        public int error { get; set; } // количество ошибок
        public List<ResultID> listResult = new List<ResultID>();


        public OperationResultID()
        {
            this.result = 0;
            this.error = 0;
        }

        public void SetResult(int code)
        {
            this.result = code;
        }

        public void AddError()
        {
            this.error++;
        }

        public void SetResultOperation(int result, long id)
        {
            this.listResult.Add(new ResultID() { id = id, result = result });
            if (result < 0) { AddError(); }
        }
    }
    /// <summary>
    /// 
    /// </summary>
    public class ResultWagon
    {
        public int num { get; set; }
        public int result { get; set; }
    }
    /// <summary>
    /// Класс данных результата выполнения обнавлений
    /// </summary>
    public class ResultUpdateWagon
    {
        public int result { get; set; } // Глобальный ресурс выполнения всего переноса
        public int count { get; set; }
        public int update { get; set; }
        public int skip { get; set; }
        public int error { get; set; }
        public int close { get; set; }
        public int add { get; set; }
        public List<ResultWagon> listResult = new List<ResultWagon>();

        public ResultUpdateWagon(int count)
        {
            this.count = count;
            this.result = 0;
            this.update = 0;
            this.skip = 0;
            this.error = 0;
            this.close = 0;
            this.add = 0;
            this.listResult.Clear();
        }

        public void SetUpdateResult(int result)
        {
            if (result < 0)
            {
                AddError(result); return;
            }
            if (result > 0)
            {
                AddUpdate(); return;
            }
            AddSkip();
            return;
        }
        public void SetUpdateResult(int result, int num)
        {
            listResult.Add(new ResultWagon() { num = num, result = result });

            if (result < 0)
            {
                AddError(result); return;
            }
            if (result > 0)
            {
                AddUpdate(); return;
            }
            AddSkip(); return;
        }
        public void SetInsertResult(int result, int num)
        {
            listResult.Add(new ResultWagon() { num = num, result = result });

            if (result < 0)
            {
                AddError(result); return;
            }
            if (result > 0)
            {
                AddInsert(); return;
            }
            AddSkip(); return;
        }
        public void SetSkipResult(int result, int num)
        {
            listResult.Add(new ResultWagon() { num = num, result = result });

            if (result < 0)
            {
                AddError(result); return;
            }
            AddSkip(); return;
        }
        public void SetResult(int code)
        {
            this.result = code;
        }
        public void AddUpdate()
        {
            this.update++;
        }
        public void AddSkip()
        {
            this.skip++;
        }
        public void AddError(int err_code)
        {
            this.error++;
        }
        public void AddError()
        {
            this.error++;
        }
        public void AddClose()
        {
            this.close++;
        }
        public void AddInsert()
        {
            this.add++;
        }
    }
    public class IDS_Base
    {

    }
}