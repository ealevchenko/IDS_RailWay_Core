using EF_IDS.Concrete;
using EF_IDS.Entities;
using EFIDS.Functions;
using IDS_;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static IDS_.IDS_WIR;

namespace IDS.Helper
{
    public static class wir_library
    {
        // Операции
        public static int oper_arrival_from_uz = 1;
        public static int oper_departure_to_uz = 2;
        public static int oper_dislocation = 3;
        public static int oper_dissolution = 4;
        public static int oper_sending = 5;
        public static int oper_arrival = 6;
        public static int oper_transportation = 7;
        public static int oper_manual_placement = 8;
        public static int oper_presentation_for_uz = 9;
        public static int oper_return_uz = 10;
        public static int oper_return_outer_way = 11;
        public static int oper_cancel = 12;
        public static int oper_unload_uz = 13;
        public static int oper_unload_vz = 14;
        public static int oper_load_uz = 15;
        public static int oper_load_vz = 16;
        public static int oper_cleaning = 17;
        public static int oper_processing = 18;
        // Статус груза
        public static int status_load_empty = 0;
        public static int status_load_loaded_arr = 1;
        public static int status_load_loaded_ip = 2;
        public static int status_load_dirty = 3;
        public static int status_load_frozen = 4;
        public static int status_load_tech_malfunction = 5;
        public static int status_load_loaded_uz = 6;
        public static int status_load_re_edging = 7;
        public static int status_load_empty_clean = 8;
        public static bool IsEmpty(this int? id_status_load)
        {
            return (id_status_load == status_load_empty || id_status_load == status_load_dirty || id_status_load == status_load_empty_clean) ? true : false;
        }
        public static bool IsEmpty(this int id_status_load)
        {
            return (id_status_load == status_load_empty || id_status_load == status_load_dirty || id_status_load == status_load_empty_clean) ? true : false;
        }
        #region Методы работы с вагонами

        #region WIR
        /// <summary>
        /// Найти последнюю запись внутреннего перемещения вагона
        /// </summary>
        /// <param name="context"></param>
        /// <param name="num"></param>
        /// <returns></returns>
        public static WagonInternalRoute GetLastWagon(this EFDbContext context, int num)
        {
            return context.WagonInternalRoutes.Where(r => r.Num == num).OrderByDescending(w => w.Id).FirstOrDefault();
        }
        /// <summary>
        /// Закрыть внутренее перемещение вагона wir
        /// </summary>
        /// <param name="wir"></param>
        /// <param name="context"></param>
        /// <param name="date_end"></param>
        /// <param name="note"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public static long? CloseWagon(this WagonInternalRoute wir, EFDbContext context, DateTime date_end, string note, string user)
        {
            if (wir == null) return null;
            if (wir.Close == null)
            {
                wir.Note = note != null ? note : wir.Note;
                wir.Close = DateTime.Now;
                wir.CloseUser = user;
                wir.GetLastMovement(ref context).CloseMovement(date_end, note, user);
                wir.GetLastOperation(ref context).CloseOperation(date_end, note, null, null, user);
                // Далее добавить закрытие перемещений по требованию
            }
            return wir.Id;
        }
        /// <summary>
        /// Поиск id_wir возвратного вагона
        /// </summary>
        /// <param name="context"></param>
        /// <param name="id_wir"></param>
        /// <returns></returns>
        public static long GetIDWIR(this EFDbContext context, long id_wir)
        {
            WagonInternalRoute? wir = context.WagonInternalRoutes.Where(r => r.Id == id_wir).FirstOrDefault();
            if (wir == null) return (int)errors_base.not_wir_db;
            ArrivalCar? arr_car = context.ArrivalCars.Where(c => c.Id == wir.IdArrivalCar).FirstOrDefault();
            if (arr_car == null) return (int)errors_base.not_arrival_cars_db;
            ArrivalUzVagon? arr_uz_vag = context.ArrivalUzVagons.Where(v => v.Id == arr_car.IdArrivalUzVagon).FirstOrDefault();
            if (arr_uz_vag == null) return (int)errors_base.not_arrival_uz_vagon;
            if (arr_uz_vag.CargoReturns != null && arr_uz_vag.CargoReturns == true)
            {
                if (wir.ParentId == null) return wir.Id;
                return context.GetIDWIR((long)wir.ParentId);
            }
            else
            {
                return wir.Id;
            }
        }
        #endregion

        #region WIM
        /// <summary>
        /// Установить вагон на путь станции
        /// </summary>
        /// <param name="wir"></param>
        /// <param name="context"></param>
        /// <param name="id_station"></param>
        /// <param name="id_way"></param>
        /// <param name="date_start"></param>
        /// <param name="position"></param>
        /// <param name="note"></param>
        /// <param name="user"></param>
        /// <param name="check_replay"></param>
        /// <returns></returns>
        public static WagonInternalMovement SetStationWagon(this WagonInternalRoute wir, ref EFDbContext context, int id_station, int id_way, DateTime date_start, int position, string note, string user, bool check_replay)
        {
            WagonInternalMovement? wim_new = null;
            if (wir != null && wir.Close == null)
            {
                // Получим последнее положение
                WagonInternalMovement? wim = wir.GetLastMovement(ref context);
                wim_new = wim.SetStationWagon(ref context, id_station, id_way, date_start, position, note, user, check_replay);
            }
            return wim_new;
        }
        /// <summary>
        /// Установить вагон на путь станции
        /// </summary>
        /// <param name="wim"></param>
        /// <param name="context"></param>
        /// <param name="id_station"></param>
        /// <param name="id_way"></param>
        /// <param name="date_start"></param>
        /// <param name="position"></param>
        /// <param name="note"></param>
        /// <param name="user"></param>
        /// <param name="check_replay"></param>
        /// <returns></returns>
        public static WagonInternalMovement SetStationWagon(this WagonInternalMovement wim, ref EFDbContext context, int id_station, int id_way, DateTime date_start, int position, string note, string user, bool check_replay)
        {
            if (wim == null) return null;
            WagonInternalMovement? wim_new = null;
            // Исключим попытку поставить дублирования записи постановки на путь
            if (wim == null || (wim != null && (wim.IdStation != id_station || wim.IdWay != id_way || wim.Position != position || wim.IdOuterWay != null || wim.IdFiling != null || wim.IdFilingNavigation != null)))
            {
                long? ParentId = wim.CloseMovement(date_start, null, user);
                wim_new = new WagonInternalMovement()
                {
                    Id = 0,
                    IdStation = id_station,
                    IdWay = id_way,
                    WayStart = date_start,
                    WayEnd = null,
                    Position = position,
                    IdOuterWay = null,
                    OuterWayStart = null,
                    OuterWayEnd = null,
                    Create = DateTime.Now,
                    CreateUser = user,
                    NumSostav = null,
                    Note = note,
                    ParentId = ParentId != 0 ? ParentId : null,
                };
                if (ParentId == 0)
                {
                    wim_new.Parent = wim;
                }
                wim.IdWagonInternalRoutesNavigation.WagonInternalMovements.Add(wim_new);
            }
            return wim_new;
        }
        /// <summary>
        /// Установить вагон на путь отправки
        /// </summary>
        /// <param name="wir"></param>
        /// <param name="id_outer_ways"></param>
        /// <param name="date_start"></param>
        /// <param name="position"></param>
        /// <param name="num_sostav"></param>
        /// <param name="note"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public static WagonInternalMovement SetSendingWagon(this WagonInternalRoute wir, ref EFDbContext context, int id_outer_ways, DateTime date_start, int position, string num_sostav, string note, string user)
        {
            WagonInternalMovement wim_new = null;
            if (wir != null && wir.Close == null)
            {
                // Получим последнее положение
                WagonInternalMovement wim = wir.GetLastMovement(ref context);
                // Исключим попытку поставить дублирования записи постановки на путь
                if (wim != null && wim.IdOuterWay != id_outer_ways)
                {
                    wim_new = new WagonInternalMovement()
                    {
                        Id = 0,
                        IdStation = wim.IdStation,
                        IdWay = wim.IdWay,
                        WayStart = wim.WayStart,
                        WayEnd = wim.WayEnd == null ? date_start : wim.WayEnd,
                        Position = position,
                        IdOuterWay = (int?)id_outer_ways,
                        OuterWayStart = date_start,
                        OuterWayEnd = null,
                        Create = DateTime.Now,
                        CreateUser = user,
                        NumSostav = num_sostav,
                        Note = note,
                        ParentId = wim.CloseMovement(date_start, null, user),
                    };
                    wir.WagonInternalMovements.Add(wim_new);
                }

            }
            return wim_new;
        }
        /// <summary>
        /// Закрыть запись позиции вагона
        /// </summary>
        /// <param name="wim"></param>
        /// <param name="date_end"></param>
        /// <param name="note"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public static long? CloseMovement(this WagonInternalMovement wim, DateTime date_end, string? note, string user)
        {
            if (wim == null) return null;
            if (wim.Close == null)
            {
                // Определим какой путь закрывать Внутрений или внешний
                if (wim.IdOuterWay == null)
                {
                    // Закроем внутрений
                    wim.WayEnd = wim.WayEnd == null ? date_end : wim.WayEnd;
                }
                else
                {
                    // Закроем внешний путь
                    wim.OuterWayEnd = wim.OuterWayEnd == null ? date_end : wim.OuterWayEnd;
                }

                wim.Note = note != null ? note : wim.Note;
                wim.Close = DateTime.Now;
                wim.CloseUser = user;
            }
            return wim.Id;
        }
        ///// <summary>
        ///// Проверка подача закрыта
        ///// </summary>
        ///// <param name="wf"></param>
        ///// <param name="wim"></param>
        ///// <returns></returns>
        //public static long IsFreeFiling_old(this WagonFiling wf, WagonInternalMovement wim, bool admin = false)
        //{
        //    long result = wf.IsOpenFiling(admin);
        //    if (result < 0) return result;
        //    //if (wf == null) return (int)errors_base.not_wf_db; // подача пуста            
        //    if (wim == null) return (int)errors_base.not_wim_db; // запись пуста
        //    if (wim.Close != null || wim.WayEnd != null) return (int)errors_base.close_wim; // запись закрыта
        //    if (wim.IdOuterWay != null) return (int)errors_base.wagon_not_way; // Вагон не стоит на пути
        //    if (wim.FilingEnd != null || wf.EndFiling != null) return (int)errors_base.close_wf; // подача закрыта
        //    if (wim.IdFiling != null && wf.Id > 0 && wim.IdFiling != wf.Id) return (int)errors_base.wim_lock_wf; // Вагон пренадлежит другой подаче
        //    // Проверим внутренее перемещение вагона, существует? открыто?
        //    WagonInternalRoute wir = wim.IdWagonInternalRoutesNavigation;
        //    if (wir == null) return (int)errors_base.not_wir_db; // нет wir
        //    // Внутренее перемещение существует
        //    if (wir.Close != null) return (int)errors_base.close_wir; // wir закрыт
        //    return wim.Id; // 
        //}
        // Проверка есть открытые операции
        public static int IsCountOpenOperationFiling(this WagonFiling wf)
        {
            int result = 0;
            if (wf == null) return (int)errors_base.not_wf_db; // Подачи нет
            if (wf.WagonInternalMovements != null && wf.WagonInternalMovements.Count() > 0)
            {
                result = wf.WagonInternalMovements.Count(w => w.IdWioNavigation != null);
            }
            return result;
        }
        ///// <summary>
        ///// Открыть операцию в подаче
        ///// </summary>
        ///// <param name="wim"></param>
        ///// <param name="context"></param>
        ///// <param name="wf"></param>
        ///// <param name="id_wagon_operations"></param>
        ///// <param name="id_organization_service"></param>
        ///// <param name="date_start"></param>
        ///// <param name="note"></param>
        ///// <param name="user"></param>
        ///// <returns></returns>
        //public static long SetOpenOperationFiling_old(this WagonInternalMovement wim, ref EFDbContext context, WagonFiling wf, int? id_wagon_operations, int? id_organization_service, DateTime? date_start, string note, string user)
        //{
        //    if (wim.IdFiling != null && wf.Id > 0 && wim.IdFiling == wf.Id && wim.FilingStart != null) return (int)errors_base.wagon_open_operation; // Вагон операция уже применена
        //    WagonInternalRoute wir = wim.IdWagonInternalRoutesNavigation;
        //    WagonInternalOperation? wio = wim.IdWioNavigation; // Последняя операция над вагоном
        //    if (id_wagon_operations != null)
        //    {
        //        if (wio == null || wio.Close != null)
        //        {
        //            if (date_start != null)
        //            {
        //                // Создать операцию
        //                WagonInternalOperation new_operation = wir.SetOpenOperation(ref context, (int)id_wagon_operations, (DateTime)date_start, null, null, id_organization_service, null, null, note, user, true); // id_wagon_operations != oper_processing
        //                wim.FilingStart = date_start;
        //                wim.IdWioNavigation = new_operation; // добавим новую операцию
        //                long res_of = wf.SetOpenFiling(user); // Обновим общее начало в подаче + обновим кто произвел обновление
        //                return res_of < 0 ? res_of : wim.Id;
        //            }
        //            else
        //            {
        //                return 0; // Указана операция но нет старта
        //            }
        //        }
        //        else
        //        {
        //            if (wio.IdOperation == id_wagon_operations && wio.Close == null)
        //            {
        //                if (date_start != null && wio.OperationEnd == null)
        //                {
        //                    wio.OperationStart = (DateTime)date_start;
        //                    wim.FilingStart = date_start;
        //                    long res_of = wf.SetOpenFiling(user); // Обновим общее начало в подаче + обновим кто произвел обновление
        //                    return res_of < 0 ? res_of : wim.Id;
        //                }
        //                else
        //                {
        //                    return 0; // Указана операция уже закрыта или нет данных начала
        //                }
        //            }
        //            else
        //            {
        //                return (int)errors_base.wagon_not_operation; // Операция вагона не соответсвует выбраной
        //            }
        //        }
        //    }
        //    else
        //    {
        //        if (wio != null && wio.OperationEnd == null && date_start == null)
        //        {
        //            // Удалим операцию если id_операции не указано wio есть
        //            wim.FilingStart = null;
        //            context.WagonInternalOperations.Remove(wio);
        //            long res_of = wf.SetOpenFiling(user); // Обновим общее начало в подаче + обновим кто произвел обновление
        //            return res_of < 0 ? res_of : wim.Id;
        //        }
        //        else
        //        {
        //            return 0; // Указана операция уже закрыта или ее еще нет
        //        }
        //    }
        //}
        ///// <summary>
        ///// Закрыть операцию в подаче (проверить если все закрыты закрыть подачу и обновить время закрытия подачи)
        ///// </summary>
        ///// <param name="wim"></param>
        ///// <param name="context"></param>
        ///// <param name="wf"></param>
        ///// <param name="date_stop"></param>
        ///// <param name="id_status_load"></param>
        ///// <param name="note"></param>
        ///// <param name="user"></param>
        ///// <returns></returns>
        //public static long SetCloseOperationFiling_old(this WagonInternalMovement wim, ref EFDbContext context, WagonFiling wf, DateTime date_stop, int? id_status_load, int? id_organization_service, string note, string user)
        //{
        //    long result = wf.IsFreeFiling_old(wim);
        //    if (result < 0) return result;// Ошибка
        //    if (wim.IdFiling != null && wf.Id > 0 && wim.IdFiling == wf.Id && wim.FilingEnd != null) return (int)errors_base.wagon_close_operation; // Вагон операция закрыта
        //    WagonInternalRoute wir = wim.IdWagonInternalRoutesNavigation;
        //    //WagonInternalMovement wim_last = wir.GetLastMovement(ref context);
        //    //if (wim_last != null && wim_last.Id != wim.Id) return (int)errors_base.err_last_wim_db; // Ошибка позиция вагона несоответсвует последней позиции в базе
        //    WagonInternalOperation? wio = wim.IdWioNavigation;
        //    if (wio == null) return (int)errors_base.not_wio_db; // В базе данных нет записи по WagonInternalOperation (Внутренняя операция по вагону)
        //    //WagonInternalOperation wio_last = wir.GetLastOperation(ref context);
        //    //if (wio != null && wio.Id > 0 && wio_last.Id != wio.Id) return (int)errors_base.wagon_not_operation; // Ошибка операция вагона не соответствует последней
        //    // Закроем операцию и позицию создадим новую строку
        //    wio.SetCloseOperation((DateTime)date_stop, null, id_status_load, id_organization_service, user);
        //    wim.FilingEnd = date_stop;
        //    //wim.CloseMovement((DateTime)date_stop, note, user);
        //    // Создать новую позицию закрыв старую
        //    //WagonInternalMovement? wim_new = wir.SetStationWagon(ref context, wim.IdStation, wim.IdWay, (DateTime)date_stop, wim.Position, note, user, true);
        //    WagonInternalMovement? wim_new = wim.SetStationWagon(ref context, wim.IdStation, wim.IdWay, (DateTime)date_stop, wim.Position, note, user, true);

        //    wf.Change = DateTime.Now;
        //    wf.ChangeUser = user;
        //    if (wim_new != null)
        //    {
        //        return wim.Id;
        //    }
        //    else
        //    {
        //        return (int)errors_base.err_create_wim_db; // Ошибка создания новой позиции вагона.
        //    }
        //}
        ///// <summary>
        ///// Закрыть уже созданную операцию в подаче (проверить если подача закрыта или введен документ тогда запрет)
        ///// </summary>
        ///// <param name="wim"></param>
        ///// <param name="context"></param>
        ///// <param name="wf"></param>
        ///// <param name="date_start"></param>
        ///// <param name="date_stop"></param>
        ///// <param name="id_status_load"></param>
        ///// <param name="note"></param>
        ///// <param name="user"></param>
        ///// <returns></returns>
        //public static long SetUpdateOperationFiling_old(this WagonInternalMovement wim, ref EFDbContext context, WagonFiling wf, DateTime? date_start, DateTime? date_stop, int? id_status_load, int? id_organization_service, string note, string user)
        //{
        //    if (wf.Close != null) return (int)errors_base.close_wf; // подача закрыта
        //    if (wim.IdFiling != null && wf.Id > 0 && wim.IdFiling == wf.Id && (wim.FilingStart == null || wim.FilingEnd == null)) return (int)errors_base.wagon_not_operation; // По вагону нет операций
        //    WagonInternalRoute wir = wim.IdWagonInternalRoutesNavigation;
        //    if (wir == null) return (int)errors_base.not_wir_db; // нет wir
        //    // Внутренее перемещение существует
        //    if (wir.Close != null) return (int)errors_base.close_wir; // wir закрыт
        //    WagonInternalOperation? wio = wim.IdWioNavigation;
        //    if (wio == null) return (int)errors_base.not_wio_db; // В базе данных нет записи по WagonInternalOperation (Внутренняя операция по вагону)
        //    wio.UpdateOperation(date_start, date_stop, null, id_status_load, id_organization_service, user);
        //    wim.FilingStart = date_start != null ? date_start : wim.FilingStart;
        //    wim.FilingEnd = date_stop != null ? date_stop : wim.FilingEnd;
        //    wf.Change = DateTime.Now;
        //    wf.ChangeUser = user;
        //    return wim.Id;
        //}

        /// <summary>
        /// * Открыть операцию подачи (создать новую или править старую-адм) (Возвращает 1-обновил, 0-не обновил, <0-ошибка)
        /// </summary>
        /// <param name="wim"></param>
        /// <param name="context"></param>
        /// <param name="wf"></param>
        /// <param name="id_wagon_operations"></param>
        /// <param name="id_organization_service"></param>
        /// <param name="date_start"></param>
        /// <param name="note"></param>
        /// <param name="user"></param>
        /// <param name="result"></param>
        /// <param name="admin"></param>
        /// <returns></returns>
        public static WagonInternalOperation? SetOpenOperationFiling(this WagonInternalMovement wim, ref EFDbContext context, WagonFiling wf, int? id_wagon_operations, int? id_organization_service, DateTime? date_start, string? note, string user, out long result, bool admin = false)
        {
            result = 0;
            WagonInternalOperation? wio = wim.IdWioNavigation; // Последняя операция над вагоном;
            WagonInternalRoute wir = wim.IdWagonInternalRoutesNavigation;
            if ((wim.IdFiling == wf.Id) && (wim.FilingStart == null || (wim.FilingStart != null && admin)))
            {
                if (id_wagon_operations != null && date_start != null)
                {
                    if (wio == null)
                    {
                        // Создать операцию
                        wio = wir.SetOpenOperation(ref context, (int)id_wagon_operations, (DateTime)date_start, null, null, id_organization_service, null, null, note, user, true);
                        wim.IdWioNavigation = wio; // добавим новую операцию
                    }
                    else if (wio.IdOperation == id_wagon_operations && admin)
                    {
                        // Правим время начала если adm
                        wio.OperationStart = (DateTime)date_start;
                    }
                    else
                    {
                        result = (int)errors_base.err_create_wio_db; // Ошибка создания новой операции над вагоном.
                        return wio;
                    }
                    wim.FilingStart = date_start;
                    int res_open = wf.SetOpenFiling(user); // Обновим общее начало в подаче + обновим кто произвел обновление
                    result = res_open < 0 ? res_open : 1;
                }
                else
                {
                    result = (int)errors_base.err_wio_error_input_value; // Ошибка входных параметров
                }
            }
            else
            {
                result = (int)errors_base.wagon_open_operation; // Вагон операция уже применена             
            }
            return wio;
        }
        /// <summary>
        /// * Закрыть операцию подачи (закрыть старую или править старую-адм) (Возвращает 1-обновил, 0-не обновил, <0-ошибка)
        /// </summary>
        /// <param name="wim"></param>
        /// <param name="context"></param>
        /// <param name="wf"></param>
        /// <param name="date_stop"></param>
        /// <param name="id_status_load"></param>
        /// <param name="id_organization_service"></param>
        /// <param name="note"></param>
        /// <param name="user"></param>
        /// <param name="result"></param>
        /// <param name="admin"></param>
        /// <returns></returns>
        public static WagonInternalOperation? SetCloseOperationFiling(this WagonInternalMovement wim, ref EFDbContext context, WagonFiling wf, DateTime date_stop, int? id_status_load, int? id_organization_service, string? note, string user, out long result, bool admin = false)
        {
            WagonInternalOperation? wio = wim.IdWioNavigation; // Последняя операция над вагоном;
            result = wf.IsOpenFiling(admin); // подача существует и открыта (если закрыта проверка на адм)
            if (result >= 0)
            {
                if ((wim.IdFiling == wf.Id) && (wim.FilingEnd == null || (wim.FilingEnd != null && admin)))
                {
                    if (wio != null)
                    {
                        if (wio.OperationEnd == null || (wio.OperationEnd != null && admin))
                        {
                            wio.SetCloseOperation(date_stop, null, id_status_load, id_organization_service, user);
                            wim.FilingEnd = date_stop;
                            //WagonInternalMovement? wim_new = wim.SetStationWagon(ref context, wim.IdStation, wim.IdWay, (DateTime)date_stop, wim.Position, note, user, true);
                            wf.Change = DateTime.Now;
                            wf.ChangeUser = user;
                            //result = (wim_new == null ? (int)errors_base.err_create_wim_db : 1);
                            result = 1;
                        }
                        else
                        {
                            result = (int)errors_base.wagon_close_operation; // Операция закрыта
                        }
                    }
                    else
                    {
                        result = (int)errors_base.not_wio_db; // В базе данных нет записи по WagonInternalOperation (Внутренняя операция по вагону)
                    }
                }
                else
                {
                    result = (int)errors_base.wagon_open_operation; // Вагон операция уже применена             
                }
            }
            return wio;
        }
        /// <summary>
        /// * Обновить уже созданную операцию (обновить текущую строку или операцию по которой уже закрыта строка с админ правами)
        /// </summary>
        /// <param name="wim"></param>
        /// <param name="context"></param>
        /// <param name="wf"></param>
        /// <param name="date_start"></param>
        /// <param name="date_stop"></param>
        /// <param name="id_status_load"></param>
        /// <param name="id_organization_service"></param>
        /// <param name="note"></param>
        /// <param name="user"></param>
        /// <param name="result"></param>
        /// <param name="admin"></param>
        /// <returns></returns>
        public static WagonInternalOperation? SetUpdateOperationFiling(this WagonInternalMovement wim, ref EFDbContext context, WagonFiling wf, DateTime? date_start, DateTime? date_stop, int? id_status_load, int? id_organization_service, string? note, string user, out long result, bool admin = false)
        {
            WagonInternalOperation? wio = wim.IdWioNavigation; // Последняя операция над вагоном;
            result = wf.IsOpenFiling(admin); // подача существует и открыта (если закрыта проверка на адм)
            if (result >= 0)
            {
                if (wim.IdFiling == wf.Id && wim.FilingStart != null)
                {
                    if (wio != null && (wf.Close == null || (wf.Close == null && admin)))
                    {
                        wio.UpdateOperation(date_start, date_stop, null, id_status_load, id_organization_service, user);
                        wim.FilingStart = date_start != null ? date_start : wim.FilingStart;
                        wim.FilingEnd = date_stop != null ? date_stop : wim.FilingEnd;
                        wf.Change = DateTime.Now;
                        wf.ChangeUser = user;
                        result = 1;
                    }
                    else
                    {
                        result = (int)errors_base.not_wio_db; // В базе данных нет записи по WagonInternalOperation (Внутренняя операция по вагону)
                    }
                }
                else
                {
                    result = (int)errors_base.wagon_not_operation; // По вагону нет операций
                }
            }
            return wio;
        }
        /// <summary>
        /// * Обновить статус операции 
        /// </summary>
        /// <param name="wim"></param>
        /// <param name="context"></param>
        /// <param name="wf"></param>
        /// <param name="vag"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public static long SetUpdateStatusOperation(this WagonInternalMovement wim, ref EFDbContext context, WagonFiling wf, IOperationWagons vag, string user)
        {
            long result = 0;
            if (vag is LoadingWagons)
            {
                if (((LoadingWagons)vag).id_status_load.IsEmpty())
                {
                    // Возвращаем порожний груз
                    // Найдем груженный не закрытый груз и предыдущий груз (предыдущий откроем а груз груженный удалим)
                    WagonInternalMoveCargo? wimc = context.WagonInternalMoveCargos.FirstOrDefault(w => w.IdWimLoad == wim.Id && w.Close == null && w.Empty != true);
                    if (wimc != null)
                    {
                        WagonInternalMoveCargo? wimc_old = context.WagonInternalMoveCargos.FirstOrDefault(w => w.Id == wimc.ParentId);
                        context.WagonInternalMoveCargos.Remove(wimc);
                        if (wimc_old != null)
                        {
                            wimc_old.Close = null;
                            wimc_old.CloseUser = null;
                            context.WagonInternalMoveCargos.Update(wimc_old);
                        }
                        result = 1;
                    }
                }
                else
                {
                    long res_load = wim.SetLoadInternalMoveCargo(ref context, wf, (LoadingWagons)vag, true, user);
                    if (res_load < 0) return (int)res_load; // Ошибка                     
                    result = 1;
                }
            }
            return result;
        }


        #endregion

        #region WagonFiling
        /// <summary>
        /// * Проверка подача существует и открыта (игнор если админ) (Возвращает >=0 Ок, <0-ошибка)
        /// </summary>
        /// <param name="wf"></param>
        /// <param name="admin"></param>
        /// <returns></returns>
        public static long IsOpenFiling(this WagonFiling wf, bool admin = false)
        {
            if (wf == null) return (int)errors_base.not_wf_db;      // подача отсутсвует
            if (wf.Close != null && !admin) return (int)errors_base.close_wf; // подача закрыта
            return wf.Id;
        }
        /// <summary>
        /// * Закрыть подачу
        /// </summary>
        /// <param name="wf"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public static long SetCloseFiling(this WagonFiling wf, string user, bool admin = false)
        {
            if (wf == null) return (int)errors_base.not_wf_db; // подача пуста   
            if (wf.Close != null && !admin) return (int)errors_base.close_wf; // подача закрыта
            int count = wf.WagonInternalMovements.Count();
            int count_close = wf.WagonInternalMovements.Where(m => m.FilingEnd != null).Count();

            if ((wf.TypeFiling == 1 || wf.TypeFiling == 3 || wf.TypeFiling == 4) && count == count_close)
            {
                WagonInternalMovement? wim_close_max = wf.WagonInternalMovements.Where(m => m.FilingEnd != null).OrderByDescending(c => c.FilingEnd).FirstOrDefault();
                DateTime? close = wim_close_max != null ? wim_close_max.FilingEnd : null;
                wf.EndFiling = close;
                wf.Close = close;
                wf.CloseUser = user;
                return wf.Id;
            }
            if (wf.TypeFiling == 2 && count == count_close)
            {
                // Проверим на закрытый документ внутри подачи
                bool document = true;
                foreach (WagonInternalMovement wim in wf.WagonInternalMovements)
                {
                    WagonInternalOperation? wio = wim.IdWioNavigation;

                    //WagonInternalMoveCargo? wimc = wim.WagonInternalMoveCargoIdWimLoadNavigations.FirstOrDefault(w => w.Close == null);
                    WagonInternalMoveCargo? wimc = wim.WagonInternalMoveCargoIdWimLoadNavigations.FirstOrDefault(w => w.IdWimLoad == wim.Id);
                    //if (wimc == null) wimc = wir.GetLastMoveCargo(ref context);
                    if ((wimc == null && wio != null && !wio.IdLoadingStatus.IsEmpty()) || (wimc != null && wio != null && wimc.DocReceived == null && !wio.IdLoadingStatus.IsEmpty()))
                    {
                        document = false; break;
                    }
                }
                // Закроем
                if (wf.DocReceived != null || (wf.DocReceived == null && document))
                {
                    WagonInternalMovement? wim_close_max = wf.WagonInternalMovements.Where(m => m.FilingEnd != null).OrderByDescending(c => c.FilingEnd).FirstOrDefault();
                    DateTime? close = wim_close_max != null ? wim_close_max.FilingEnd : null;
                    wf.EndFiling = close;
                    wf.Close = close;
                    wf.CloseUser = user;
                    return wf.Id;
                }
            }


            return 0; // не все позиции закрыты
        }
        /// <summary>
        /// * Начать или сбросить начало операций в подаче (Возвращает 1-обновил, 0-не обновил, <0-ошибка)
        /// </summary>
        /// <param name="wf"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public static int SetOpenFiling(this WagonFiling wf, string user)
        {
            if (wf.Close != null) return (int)errors_base.close_wf; // подача закрыта
            WagonInternalMovement? wim_open_min = wf.WagonInternalMovements.Where(m => m.FilingStart != null).OrderBy(c => c.FilingStart).FirstOrDefault();
            DateTime? start = wim_open_min != null ? wim_open_min.FilingStart : null;
            if (wf.StartFiling != start)
            {
                wf.StartFiling = start;
                wf.Change = DateTime.Now;
                wf.ChangeUser = user;
                //return wf.Id;
                return 1;
            }
            return 0;
        }
        /// <summary>
        /// * Внутренее перемещение существует и открыто
        /// </summary>
        /// <param name="wim"></param>
        /// <returns></returns>
        public static long IsOpenWIR(this WagonInternalMovement wim)
        {
            WagonInternalRoute? wir = wim.IdWagonInternalRoutesNavigation;
            // Внутренее перемещение существует и открыто
            if (wir != null && wir.Close != null) return (int)errors_base.close_wir; // wir закрыт
            return wim.Id; // 
        }


        /// <summary>
        /// * По вагону возможна операция добавить вагон в подачу
        /// </summary>
        /// <param name="wf"></param>
        /// <param name="wim"></param>
        /// <param name="admin"></param>
        /// <returns></returns>
        public static long IsСheckAddWagonFiling(this WagonFiling wf, WagonInternalMovement? wim, bool admin = false)
        {
            long result = wf.IsOpenFiling(admin); if (result < 0) return result;
            if (wim == null) return (int)errors_base.not_wim_db;    // запись wim не существует  
            result = wim.IsOpenWIR(); if (result < 0) return result;
            if ((wim.Close != null || wim.WayEnd != null) && !admin) return (int)errors_base.close_wim; // запись закрыта вагон не стоит на пути или строка закрыта
            if (wim.IdOuterWay != null) return (int)errors_base.wagon_not_way; // Вагон стоит на перегоне
            if (wim.IdFiling != null && wf.Id != 0 && wim.IdFiling == wf.Id && wim.FilingEnd != null && wf.EndFiling != null && !admin) return (int)errors_base.close_wf; // подача закрыта
            if (wim.IdFiling != null && wf.Id > 0 && wim.IdFiling != wf.Id && wim.FilingEnd == null) return (int)errors_base.wim_lock_wf; // Вагон пренадлежит другой подаче и операция в подаче не закрыта
            return wim.Id; // 
        }
        /// <summary>
        /// * По вагону возможна операция обновить вагон в подаче
        /// </summary>
        /// <param name="wf"></param>
        /// <param name="wim"></param>
        /// <param name="admin"></param>
        /// <returns></returns>
        public static long IsСheckUpdateWagonFiling(this WagonFiling wf, WagonInternalMovement? wim, bool admin = false)
        {
            long result = wf.IsOpenFiling(admin); if (result < 0) return result;
            if (wim == null) return (int)errors_base.not_wim_db;    // запись wim не существует  
            result = wim.IsOpenWIR(); if (result < 0) return result;
            //if ((wim.Close != null || wim.WayEnd != null) && !admin) return (int)errors_base.close_wim; // запись закрыта вагон не стоит на пути или строка закрыта
            if (wf.Id == 0 || wim.IdFiling == null || (wf.Id > 0 && wim.IdFiling != null && wim.IdFiling != wf.Id)) return (int)errors_base.wim_lock_wf; // Вагон пренадлежит другой подаче и операция в подаче не закрыта
            return wim.Id; // 
        }
        /// <summary>
        /// * Метод добавить вагон в подачу (по id_wim)
        /// </summary>
        /// <param name="wf"></param>
        /// <param name="context"></param>
        /// <param name="id_wim"></param>
        /// <param name="user"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static WagonInternalMovement? SetAddUpdateWagonFiling(this WagonFiling wf, ref EFDbContext context, long id_wim, bool update, string user, out long result, bool admin = false)
        {
            result = wf.IsOpenFiling(admin); // подача существует и открыта (добавление вагонов в закрытую подачу нет - АДМ)
            if (result >= 0)
            {
                // Найдем строку тек позиции в подаче
                WagonInternalMovement? wim = wf.WagonInternalMovements.Where(m => m.Id == id_wim).FirstOrDefault();
                if (wim == null)
                {
                    // найдем строку тек позиции в системе
                    wim = context.WagonInternalMovements
                        .Include(wir => wir.IdWagonInternalRoutesNavigation)
                        .Include(wio => wio.IdWioNavigation)
                        .Where(m => m.Id == id_wim).FirstOrDefault();
                }
                return wf.SetAddUpdateWagonFiling(ref context, wim, update, user, out result, admin);
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// * Метод добавить вагон в подачу (по строке WagonInternalMovement)
        /// </summary>
        /// <param name="wf"></param>
        /// <param name="context"></param>
        /// <param name="wim"></param>
        /// <param name="user"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static WagonInternalMovement? SetAddUpdateWagonFiling(this WagonFiling wf, ref EFDbContext context, WagonInternalMovement? wim, bool update, string user, out long result, bool admin = false)
        {
            result = wf.IsOpenFiling(admin); // подача существует и открыта (игнор закрыта если админка)
            if (result >= 0)
            {
                // Проверим тек позиция - открыта, не стоит на перегоне, не пренадлежит не закрытому внутренему перемещению
                // и возможено обновление(удаление) или добавление
                result = update ? IsСheckUpdateWagonFiling(wf, wim, admin) : IsСheckAddWagonFiling(wf, wim, admin);
                if (result >= 0)
                {
                    WagonInternalRoute wir = wim.IdWagonInternalRoutesNavigation;
                    string note = "Подача:" + wf.Id.ToString() + "-" + wf.TypeFiling.ToString();
                    // проверка на завершение подачи если предыдущее положение подача тогда берем время окончания подачи
                    DateTime start_date = wim.FilingEnd != null ? (DateTime)wim.FilingEnd : wim.WayStart;

                    // Проверим обновить или добавить позицию вагона
                    if (wf.Id != wim.IdFiling)
                    {
                        // создадим строку 
                        WagonInternalMovement wim_new = new WagonInternalMovement()
                        {
                            Id = 0,
                            IdWagonInternalRoutes = wir.Id,
                            IdStation = wim.IdStation,
                            IdWay = wim.IdWay,
                            WayStart = start_date,
                            WayEnd = null,
                            Position = wim.Position,
                            IdOuterWay = null,
                            OuterWayStart = null,
                            OuterWayEnd = null,
                            NumSostav = null,
                            Note = note,
                            IdFiling = wf.Id,
                            IdWio = null,
                            FilingEnd = null,
                            FilingStart = null,
                            Create = DateTime.Now,
                            CreateUser = user,
                            Close = null,
                            CloseUser = null,
                            ParentId = wim.CloseMovement(start_date, null, user),
                        };
                        context.WagonInternalMovements.Add(wim_new);
                        if (wf.Id == 0) { wim_new.IdFilingNavigation = wf; }
                        wf.WagonInternalMovements.Add(wim_new);
                        wf.Change = DateTime.Now;
                        wf.ChangeUser = user;
                        result = 1;
                        return wim_new;
                    }
                    else
                    {
                        result = wim.Id;
                        return wim;
                    }
                }
                else
                {
                    return wim;
                }
            }
            else
            {
                return null;
            }
        }
        ///// <summary>
        ///// Добавить вагон в подачу
        ///// </summary>
        ///// <param name="wf"></param>
        ///// <param name="wim"></param>
        ///// <param name="user"></param>
        ///// <param name="result"></param>
        ///// <returns></returns>
        //public static WagonInternalMovement SetAddWagonFiling_old(this WagonFiling wf, WagonInternalMovement wim, string user, out long result)
        //{
        //    result = wf.IsFreeFiling(wim);
        //    if (result >= 0)
        //    {
        //        string note = "Подача:" + wf.Id.ToString() + "-" + wf.TypeFiling.ToString();
        //        // Проверим если есть ссылка на операцию, тогда делаем копию wim для подачи
        //        if (wim.IdWio != null)
        //        {
        //            WagonInternalMovement wim_new = new WagonInternalMovement()
        //            {
        //                Id = 0,
        //                IdStation = wim.IdStation,
        //                IdWay = wim.IdWay,
        //                WayStart = wim.WayStart,
        //                WayEnd = null,
        //                Position = wim.Position,
        //                IdOuterWay = null,
        //                OuterWayStart = null,
        //                OuterWayEnd = null,
        //                NumSostav = null,
        //                Note = note,
        //                IdFiling = wf.Id,
        //                IdWio = null,
        //                FilingEnd = null,
        //                FilingStart = null,
        //                Create = DateTime.Now,
        //                CreateUser = user,
        //                Close = null,
        //                CloseUser = null,
        //                ParentId = wim.CloseMovement(wim.WayStart, null, user),
        //            };
        //            wim_new.IdWagonInternalRoutes = wim.IdWagonInternalRoutes;
        //            wim.IdWagonInternalRoutesNavigation.WagonInternalMovements.Add(wim_new);
        //            wim_new.IdWagonInternalRoutesNavigation = wim.IdWagonInternalRoutesNavigation;
        //            wim_new.IdFilingNavigation = wf;
        //            wf.WagonInternalMovements.Add(wim_new);
        //            wf.Change = DateTime.Now;
        //            wf.ChangeUser = user;
        //            return wim_new;
        //        }
        //        else
        //        {
        //            wim.Note = note;
        //            wim.IdFilingNavigation = wf;
        //            wf.WagonInternalMovements.Add(wim);
        //            wf.Change = DateTime.Now;
        //            wf.ChangeUser = user;
        //            return wim;
        //        }
        //    }
        //    else
        //    {
        //        return wim;
        //    }


        //}
        /// <summary>
        /// Убрать вагон из подачи
        /// </summary>
        /// <param name="wf"></param>
        /// <param name="wim"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        //public static long SetDeleteWagonFiling_old(this WagonFiling wf, ref EFDbContext context, WagonInternalMovement wim, string user)
        //{
        //    long result = wf.IsFreeFiling_old(wim);
        //    if (result < 0) return result;
        //    WagonInternalOperation? wio = null;
        //    // Если wim не пренадлежит подаче, тогда добавим в подачу
        //    if (wf.Id != wim.IdFiling) return (int)errors_base.wim_lock_wf;
        //    // если подача открыта, тогда прочтем операцию
        //    if (wim.FilingStart != null)
        //    {
        //        wio = wim.IdWioNavigation;
        //        // Операция закрыта
        //        if (wio != null && wio.OperationStart != null && wio.OperationEnd != null) return (int)errors_base.wagon_close_operation;
        //    }
        //    // Подача на вагон закрыта?
        //    if (wim.FilingStart != null && wim.FilingEnd != null) return (int)errors_base.wim_close_wf;                                 // Вагон заблокирован, операция в подаче по вагону закрыта

        //    // Погрузка текущая
        //    if (wim != null)
        //    {
        //        WagonInternalMoveCargo? wimc = context.WagonInternalMoveCargos.Where(w => w.IdWimLoad == wim.Id).FirstOrDefault();
        //        // Проверим есть погрузка текущая
        //        if (wimc != null && wimc.Close == null)
        //        {
        //            WagonInternalMoveCargo? wimc_parent = wimc.ParentId != null ? context.WagonInternalMoveCargos.Where(w => w.Id == wimc.ParentId).FirstOrDefault() : null;
        //            if (wimc_parent != null)
        //            {
        //                wimc_parent.Close = null;
        //                wimc_parent.CloseUser = null;
        //                context.WagonInternalMoveCargos.Update(wimc_parent);
        //            }
        //            context.WagonInternalMoveCargos.Remove(wimc);
        //        }
        //        // удалим ссылку на подачу
        //        wim.IdFiling = null;
        //        wim.FilingStart = null;
        //        wim.FilingEnd = null;
        //        wim.Note = null;
        //        wim.IdWio = wio != null ? null : wim.IdWio; // сбросим если есть операция
        //        context.WagonInternalMovements.Update(wim);
        //        wf.WagonInternalMovements.Remove(wim);
        //        context.WagonFilings.Update(wf);
        //        // удалим операцию подачи
        //        if (wio != null) context.WagonInternalOperations.Remove(wio);
        //    }
        //    // Проверка на пустую подачу
        //    if (wf.WagonInternalMovements == null || wf.WagonInternalMovements.Count() == 0)
        //    {
        //        context.WagonFilings.Remove(wf); // удалить
        //        return wim.Id;
        //    }
        //    else
        //    {
        //        // проверим на открытые операции (если нет удалим начало подачи)
        //        int count_open_operation = wf.IsCountOpenOperationFiling();
        //        wf.StartFiling = count_open_operation == 0 ? null : wf.StartFiling;
        //        wf.EndFiling = count_open_operation == 0 ? null : wf.EndFiling;
        //        wf.Change = DateTime.Now;
        //        wf.ChangeUser = user;
        //        long res = wf.SetCloseFiling(user);
        //        if (res < 0)
        //        {
        //            return res;
        //        }
        //        else
        //        {
        //            return wim.Id;
        //        }
        //    }
        //}
        /// <summary>
        /// Убрать вагон из подачи (если указан админ удалим из закрытой с проверкой на наличие следующей подачи)
        /// </summary>
        /// <param name="wf"></param>
        /// <param name="context"></param>
        /// <param name="id_wim"></param>
        /// <param name="user"></param>
        /// <param name="admin"></param>
        /// <returns></returns>
        public static long SetDeleteWagonFiling(this WagonFiling wf, ref EFDbContext context, long id_wim, List<ViewFilingNext> list_filing_next, string user, bool admin = false)
        {
            long result = wf.IsOpenFiling(admin); // подача существует и открыта (если закрыта и админнка тогда пропустит)
            if (result >= 0)
            {
                // Найдем строку тек позиции в подаче
                WagonInternalMovement? wim = wf.WagonInternalMovements.FirstOrDefault(m => m.Id == id_wim);
                // Проверим тек позиция - открыта, не стоит на перегоне, не пренадлежит не закрытой или это админка
                //result = IsСheckAddWagonFiling(wf, wim, admin);
                result = IsСheckUpdateWagonFiling(wf, wim, admin);
                if (result >= 0)
                {
                    ViewFilingNext? fn = list_filing_next.FirstOrDefault(n => n.IdWim == id_wim);
                    // Проверим, следущая операция не существует или это новая подача
                    if (fn == null || (fn != null && fn.IdWimNext == null))
                    {
                        // Скорректируем груз если подача не очистка
                        if (wf.TypeFiling != 3)
                        {
                            // Подача не очистка, правим груз
                            WagonInternalMoveCargo? wimc = context.WagonInternalMoveCargos.Where(o => o.IdWimLoad == id_wim).FirstOrDefault();
                            // текущий груз
                            if (wimc != null)
                            {
                                // Проверим есть изменения по следующему грузу
                                WagonInternalMoveCargo? wimc_next = context.WagonInternalMoveCargos.Where(o => o.ParentId == wimc.Id).FirstOrDefault();
                                // проверка предыдущего груза
                                if (wimc.ParentId != null)
                                {
                                    WagonInternalMoveCargo? wimc_old = context.WagonInternalMoveCargos.Where(o => o.Id == wimc.ParentId).FirstOrDefault();
                                    if (wimc_next != null)
                                    {
                                        wimc_next.ParentId = wimc_old != null ? wimc_old.Id : null;
                                        context.WagonInternalMoveCargos.Update(wimc_next);
                                    }
                                    else
                                    {
                                        if (wimc_old != null)
                                        {
                                            wimc_old.Close = null;
                                            wimc_old.CloseUser = null;
                                            context.WagonInternalMoveCargos.Update(wimc_old);
                                        }
                                    }
                                }
                                context.WagonInternalMoveCargos.Remove(wimc);
                            }
                        }
                        // Правим текущую операцию
                        //WagonInternalOperation? wio = context.WagonInternalOperations.Where(o => o.Id == wim.IdWio).FirstOrDefault();
                        WagonInternalOperation? wio = wim.IdWioNavigation;
                        if (wio != null)
                        {
                            // смотрим на предыдущую операцию 
                            WagonInternalOperation? wio_old = context.WagonInternalOperations.Where(o => o.Id == wio.ParentId).FirstOrDefault();
                            if (wio_old != null)
                            {
                                // скорректируем статусы по операциям
                                WagonInternalOperation? wio_next = context.WagonInternalOperations.Where(o => o.ParentId == wio.Id).FirstOrDefault();
                                if (wio_next != null)
                                {
                                    int ls_curr = wio.IdLoadingStatus;
                                    int ls_old = wio_old.IdLoadingStatus;
                                    // Скорректируем статусы
                                    wio_next.CorrectLoadingStatus(ref context, ls_curr, ls_old);
                                    wio_next.ParentId = wio_old.Id;
                                    context.WagonInternalOperations.Update(wio_next);
                                }
                            }
                        }
                        // Удалим положение подачи
                        // смотрим на предыдущую операцию 
                        WagonInternalMovement? wim_old = context.WagonInternalMovements.FirstOrDefault(o => o.Id == wim.ParentId);
                        if (wim_old != null)
                        {
                            WagonInternalMovement? wim_next = context.WagonInternalMovements.FirstOrDefault(o => o.ParentId == wim.Id);
                            if (wim_next != null)
                            {
                                wim_next.WayStart = wim_old != null ? (DateTime)wim_old.WayEnd : wim_next.WayStart;
                                wim_next.ParentId = wim_old != null ? wim_old.Id : null;
                                context.WagonInternalMovements.Update(wim_next);
                            }
                            else
                            {
                                // текущая позиция последняя (откроем предыдущую)
                                wim_old.WayEnd = null;
                                wim_old.Close = null;
                                wim_old.CloseUser = null;
                                context.WagonInternalMovements.Update(wim_old);
                            }
                        }
                        if (wio != null) context.WagonInternalOperations.Remove(wio);
                        wf.WagonInternalMovements.Remove(wim);
                        context.WagonInternalMovements.Remove(wim);
                        context.WagonFilings.Update(wf);
                        return id_wim;
                    }
                    else
                    {
                        return (int)errors_base.err_wf_del_wagon; // Ошибка, запрет удаления вагона из подачи (по вагону открыта слежующая подача)
                    }
                }
                else { return result; }
            }
            else { return result; }
        }
        #endregion

        #region WagonInternalMoveCargo
        /// <summary>
        /// Получить последнюю запись внутризаводского груза 
        /// </summary>
        /// <param name="wir"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public static WagonInternalMoveCargo? GetLastMoveCargo(this WagonInternalRoute wir, ref EFDbContext context)
        {
            if (wir.WagonInternalMovements == null) return null;
            WagonInternalMoveCargo? wimc = context.WagonInternalMoveCargos.Where(m => m.IdWagonInternalRoutes == wir.Id).OrderByDescending(c => c.Id).FirstOrDefault();
            return wimc;
        }
        /// <summary>
        /// Создать или обновить погрузку внутреннего перемещения 
        /// </summary>
        /// <param name="wim"></param>
        /// <param name="context"></param>
        /// <param name="wf"></param>
        /// <param name="wagon"></param>
        /// <param name="update"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public static long SetLoadInternalMoveCargo(this WagonInternalMovement wim, ref EFDbContext context, WagonFiling wf, LoadingWagons wagon, bool update, string user)
        {
            // Проверим вагон и подачу на открытость для операции, и добавим в подачу если небыл добавлен
            //if (wim.IdFiling != null && wf.Id > 0 && wim.IdFiling == wf.Id && wim.FilingStart != null) return (int)errors_base.wagon_open_operation; // Вагон операция уже применена
            WagonInternalRoute wir = wim.IdWagonInternalRoutesNavigation;
            // Проверим wir
            if (wir == null) return (int)errors_base.not_wir_db;    // В базе данных нет записи по WagonInternalRoutes (Внутреннее перемещение вагонов)
            // Определим груз порожний или нет
            bool? Empty = null;
            if (wagon.id_cargo != null)
            {
                DirectoryCargo? cargo = context.DirectoryCargos.Where(c => c.Id == wagon.id_cargo).FirstOrDefault();
                Empty = cargo != null ? cargo.EmptyWeight : null;
            }
            if (wagon.id_internal_cargo != null)
            {
                DirectoryInternalCargo? cargo = context.DirectoryInternalCargos.Where(c => c.Id == wagon.id_internal_cargo).FirstOrDefault();
                Empty = cargo != null ? cargo.EmptyWeight : null;
            }
            // Проверка мы грузим не порожний груз
            if (Empty == true && wagon.start != null) return (int)errors_base.error_input_cargo; // Ошибка, неправильно задан груз
            if (wagon.id_status_load != null && wagon.id_status_load.IsEmpty() && Empty != true) return (int)errors_base.error_input_cargo; // Ошибка, неправильно задан груз
            if (wagon.id_status_load != null && !wagon.id_status_load.IsEmpty() && Empty == true) return (int)errors_base.error_input_cargo; // Ошибка, неправильно задан груз

            // Проверим если есть дата документа тогда проверим все необходимые входные данные
            if (wagon.doc_received != null || wf.DocReceived != null)
            {
                if (wagon.id_wagon_operations == oper_load_vz || wagon.id_wagon_operations == oper_load_uz)
                {
                    // операция вз
                    if (wagon.id_wagon_operations == oper_load_vz && (
                        (wagon.doc_received != null && (String.IsNullOrWhiteSpace(wagon.num_nakl) || (wagon.vesg == null && !wagon.id_status_load.IsEmpty()) || wagon.id_internal_cargo == null || wagon.id_station_amkr_on == null || wagon.id_devision_on == null))
                        ||
                        (wf.DocReceived != null && (String.IsNullOrWhiteSpace(wf.NumFiling) || wf.Vesg == null || wagon.id_internal_cargo == null || wagon.id_station_amkr_on == null || wagon.id_devision_on == null))
                        ))
                    {
                        return (int)errors_base.error_value_load_vz;  // Ошибка, неверный формат или не все праметры заданы для создания загрузки ВЗ
                    }
                    // операция уз
                    //if (wagon.id_wagon_operations == oper_load_uz && (wagon.id_cargo == null || wagon.code_station_uz == null))
                    if (wagon.id_wagon_operations == oper_load_uz && (wagon.id_cargo == null))
                    {
                        return (int)errors_base.error_value_load_uz;  // Ошибка, неверный формат или не все праметры заданы для создания загрузки УЗ
                    }
                }
                else return (int)errors_base.error_value_operation;  // Ошибка, неверный код операции
            }
            // Получим запись груза по IdWimLoad == wim.Id
            WagonInternalMoveCargo? wimc = context.WagonInternalMoveCargos.Where(w => w.IdWimLoad == wim.Id).FirstOrDefault();
            // Если записи нет получим последнюю запись груза перемещаемого на предприятии
            if (wimc == null) wimc = wir.GetLastMoveCargo(ref context);

            if (wimc == null || wim.Id == 0 || (wimc != null && wimc.IdWimLoad != wim.Id && wimc.Empty == true && !wagon.id_status_load.IsEmpty()))
            {
                // Закроем груз с признаком пустой груз (Вагоны порожние)
                if (wimc != null && wimc.Empty == true)
                {
                    wimc.Close = DateTime.Now;
                    wimc.CloseUser = user;
                }
                // Создать новый груз с весом
                WagonInternalMoveCargo new_wimc = new WagonInternalMoveCargo()
                {
                    Id = 0,
                    IdWagonInternalRoutes = wir.Id,
                    InternalDocNum = wagon.num_nakl,
                    IdWeighingNum = null,
                    DocReceived = wagon.doc_received,
                    IdCargo = wagon.id_cargo,
                    IdInternalCargo = wagon.id_internal_cargo,
                    Empty = Empty,
                    Vesg = wagon.vesg,
                    IdStationFromAmkr = wim.IdStation,
                    IdDivisionFrom = wf.IdDivision,
                    IdWimLoad = wim.Id,
                    CodeExternalStation = wagon.code_station_uz,
                    IdStationOnAmkr = wagon.id_station_amkr_on,
                    IdDivisionOn = wagon.id_devision_on,
                    Create = DateTime.Now,
                    CreateUser = user,
                    ParentId = wimc != null ? wimc.Id : null,
                };
                if (wim.Id == 0)
                {
                    wim.WagonInternalMoveCargoIdWimLoadNavigations.Add(new_wimc);
                }
                context.WagonInternalMoveCargos.Add(new_wimc);
                return wim.Id;
            }
            else
            {
                if (wimc != null && wimc.IdWimLoad != null && wimc.IdWimLoad == wim.Id && wimc.IdWimRedirection == null && wimc.DocReceived == null && wimc.Close == null)
                {
                    // Перемещение груза есть, и операция погрузки совподает
                    wimc.InternalDocNum = String.IsNullOrWhiteSpace(wf.NumFiling) ? wagon.num_nakl : null;
                    wimc.IdWeighingNum = null;
                    //wimc.DocReceived = wf.DocReceived == null ? wagon.doc_received : wf.DocReceived;
                    wimc.DocReceived = wagon.doc_received != null ? wagon.doc_received : null;
                    wimc.IdCargo = wagon.id_cargo;
                    wimc.IdInternalCargo = wagon.id_internal_cargo;
                    wimc.Empty = Empty;
                    wimc.Vesg = wf.Vesg == null ? wagon.vesg : null;
                    wimc.IdStationFromAmkr = wim.IdStation;
                    wimc.IdDivisionFrom = wf.IdDivision;
                    wimc.CodeExternalStation = wagon.code_station_uz;
                    wimc.IdStationOnAmkr = wagon.id_station_amkr_on;
                    wimc.IdDivisionOn = wagon.id_devision_on;
                    wimc.Change = DateTime.Now;
                    wimc.ChangeUser = user;
                    context.WagonInternalMoveCargos.Update(wimc);
                    return wim.Id;
                }
                else
                {
                    return (int)errors_base.error_update_load; // Ошибка, обновления операции погрузки
                }
            }
        }
        /// <summary>
        /// Закрыть строку выгрузки (Возврат <0-ошибка, >=0-ok)
        /// </summary>
        /// <param name="wim"></param>
        /// <param name="context"></param>
        /// <param name="wf"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public static long SetUnloadInternalMoveCargo(this WagonInternalMovement wim, ref EFDbContext context, WagonFiling wf, UnloadingWagons wagon, string user)
        {
            // Проверим вагон и подачу на открытость для операции, и добавим в подачу если небыл добавлен
            //if (wim.IdFiling != null && wf.Id > 0 && wim.IdFiling == wf.Id && wim.FilingStart != null) return (int)errors_base.wagon_open_operation; // Вагон операция уже применена
            WagonInternalRoute wir = wim.IdWagonInternalRoutesNavigation;
            if (!wagon.id_status_load.IsEmpty()) return (int)errors_base.error_input_cargo; // Ошибка, неправильно задан груз

            WagonInternalMoveCargo? wimc = context.WagonInternalMoveCargos.Where(w => w.IdWimLoad == wim.Id).FirstOrDefault();
            if (wimc == null) wimc = wir.GetLastMoveCargo(ref context); // Получим последнюю запись груза перемещаемого на предприятии

            //WagonInternalMoveCargo? wimc = wir.GetLastMoveCargo(ref context);// Получим последнюю запись груза перемещаемого на предприятии

            if (wimc == null || wimc != null && wimc.Empty != true && wagon.id_status_load.IsEmpty())
            {
                // Закроем груз с признаком не пустой  (Вагоны не порожние)
                if (wimc != null && wimc.Empty != true)
                {
                    wimc.Close = DateTime.Now;
                    wimc.CloseUser = user;
                }
                // Создать новый груз с весом
                WagonInternalMoveCargo new_wimc = new WagonInternalMoveCargo()
                {
                    Id = 0,
                    IdWagonInternalRoutes = wir.Id,
                    InternalDocNum = null,
                    IdWeighingNum = null,
                    DocReceived = null,
                    IdCargo = wagon.id_wagon_operations == oper_unload_uz ? 1 : null,
                    IdInternalCargo = wagon.id_wagon_operations == oper_unload_vz ? 0 : null,
                    Empty = true,
                    Vesg = null,
                    IdStationFromAmkr = wim.IdStation,
                    IdDivisionFrom = wf.IdDivision,
                    IdWimLoad = wim.Id,
                    CodeExternalStation = null,
                    IdStationOnAmkr = null,
                    IdDivisionOn = null,
                    Create = DateTime.Now,
                    CreateUser = user,
                    ParentId = wimc != null ? wimc.Id : null,
                };
                if (wim.Id == 0)
                {
                    new_wimc.IdWimLoadNavigation = wim;
                }
                context.WagonInternalMoveCargos.Add(new_wimc);
                return wim.Id;
            }
            else
            {
                if (wimc != null && wimc.Empty == true && wimc.IdWimLoad != null && wimc.IdWimLoad == wim.Id)
                {
                    // Перемещение груза есть, и операция погрузки совподает
                    wimc.InternalDocNum = null;
                    wimc.IdWeighingNum = null;
                    wimc.DocReceived = null;
                    wimc.IdCargo = wagon.id_wagon_operations == oper_unload_uz ? 1 : null;
                    wimc.IdInternalCargo = wagon.id_wagon_operations == oper_unload_vz ? 0 : null;
                    wimc.Empty = false;
                    wimc.Vesg = null;
                    wimc.IdStationFromAmkr = wim.IdStation;
                    wimc.IdDivisionFrom = wf.IdDivision;
                    wimc.CodeExternalStation = null;
                    wimc.IdStationOnAmkr = null;
                    wimc.IdDivisionOn = null;
                    wimc.Change = DateTime.Now;
                    wimc.ChangeUser = user;
                    context.WagonInternalMoveCargos.Update(wimc);
                    return wim.Id;
                }
                else
                {
                    return (int)errors_base.error_update_load; // Ошибка, обновления операции погрузки
                }
            }
        }
        #endregion

        #region WIO
        /// <summary>
        /// 
        /// </summary>
        /// <param name="wir"></param>
        /// <param name="context"></param>
        /// <param name="id_operation"></param>
        /// <param name="date_start"></param>
        /// <param name="id_condition"></param>
        /// <param name="id_loading_status"></param>
        /// <param name="id_organization_service"></param>
        /// <param name="locomotive1"></param>
        /// <param name="locomotive2"></param>
        /// <param name="note"></param>
        /// <param name="user"></param>
        /// <param name="close_parent_operation"></param>
        /// <returns></returns>
        public static WagonInternalOperation SetOpenOperation(this WagonInternalRoute wir, ref EFDbContext context, int id_operation, DateTime date_start, int? id_condition, int? id_loading_status, int? id_organization_service, string locomotive1, string locomotive2, string note, string user, bool close_parent_operation = true)
        {
            WagonInternalOperation? wio_new = null;

            if (wir != null && wir.Close == null)
            {
                WagonInternalOperation? wio_last = wir.GetLastOperation(ref context);
                wio_new = new WagonInternalOperation()
                {
                    Id = 0,
                    IdOperation = id_operation,
                    OperationStart = date_start,
                    IdCondition = (id_condition != null ? (int)id_condition : (wio_last != null ? wio_last.IdCondition : 0)),
                    ConChange = (id_condition == null && wio_last != null ? wio_last.ConChange : null),
                    ConChangeUser = (id_condition == null && wio_last != null ? wio_last.ConChangeUser : null),
                    IdLoadingStatus = (id_loading_status != null ? (int)id_loading_status : (wio_last != null ? wio_last.IdLoadingStatus : 0)),
                    Locomotive1 = locomotive1,
                    Locomotive2 = locomotive2,
                    Note = note,
                    Create = DateTime.Now,
                    CreateUser = user,
                    ParentId = close_parent_operation ? (wio_last != null ? wio_last.CloseOperation(date_start, null, id_loading_status, null, user) : null) : (wio_last != null ? wio_last.Id : null),
                    IdOrganizationService = id_organization_service
                };

                wir.WagonInternalOperations.Add(wio_new);
            }
            return wio_new;
        }
        public static WagonInternalOperation SetCloseOperation(this WagonInternalOperation wio, DateTime date_end, string note, int? id_loading_status, int? id_organization_service, string user)
        {
            if (wio != null && wio.Close == null)
            {
                wio.CloseOperation(date_end, note, id_loading_status, id_organization_service, user);
            }
            return wio;
        }
        /// <summary>
        /// Вагон на территории АМКР с операцией предявлен?
        /// </summary>
        /// <param name="context"></param>
        /// <param name="num"></param>
        /// <returns></returns>
        public static bool? isLockPresentOperation(this EFDbContext context, int num)
        {
            WagonInternalRoute wir = context.GetLastWagon(num);
            if (wir == null) return null;
            if (wir.Close == null)
            {
                WagonInternalOperation wio = wir.GetLastOperation(ref context);
                if (wio == null) return null;

                return wio.IdOperation == 9 ? true : false;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// Вернуть список вагонов по которым стоит опреация предъявить.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="nums"></param>
        /// <returns></returns>
        public static List<int> GetWagonsLockPresentOperation(this EFDbContext context, List<int> nums)
        {
            List<int> list_result = new List<int>();
            foreach (int num in nums)
            {
                if (context.isLockPresentOperation(num) == true)
                {
                    list_result.Add(num);
                }
            }
            return list_result;
        }
        /// <summary>
        /// Истроия передвижения вагона имеет признак схода или повреждения
        /// </summary>
        /// <param name="context"></param>
        /// <param name="id_wir"></param>
        /// <returns></returns>
        public static bool isDerailmentOperation(this EFDbContext context, long id_wir)
        {
            WagonInternalOperation? wio = context.WagonInternalOperations.Where(o => o.IdWagonInternalRoutes == id_wir && (o.IdCondition == 76 || o.IdCondition == 74)).FirstOrDefault();
            return wio != null;
        }
        /// <summary>
        /// Вернуть первую операцию выгрузки УЗ (вагон прибыл с внешней сети)
        /// </summary>
        /// <param name="context"></param>
        /// <param name="id_wir"></param>
        /// <returns></returns>
        public static WagonInternalOperation? GetUnloadUzOperation(this EFDbContext context, long id_wir)
        {
            WagonInternalOperation? wio = context.WagonInternalOperations.Where(o => o.IdWagonInternalRoutes == id_wir && o.IdOperation == oper_unload_uz && (o.IdLoadingStatus == status_load_empty || o.IdLoadingStatus == status_load_dirty)).OrderBy(c => c.Id).FirstOrDefault();
            return wio;
        }
        /// <summary>
        /// Вернуть последнюю операцию погрузки УЗ (вагон убывает на внешнюю сеть)
        /// </summary>
        /// <param name="context"></param>
        /// <param name="id_wir"></param>
        /// <returns></returns>
        public static WagonInternalOperation? GetLoadUzOperation(this EFDbContext context, long id_wir)
        {
            WagonInternalOperation? wio = context.WagonInternalOperations.Where(o => o.IdWagonInternalRoutes == id_wir && o.IdOperation == oper_load_uz && o.IdLoadingStatus == status_load_loaded_uz).OrderByDescending(c => c.Id).FirstOrDefault();
            return wio;
        }
        /// <summary>
        /// * Скорректируем статусы (перепишим статусы предыдущего wio до конца изменяемого статуса) 
        /// </summary>
        /// <param name="wio"></param>
        /// <param name="context"></param>
        /// <param name="OldLoadingStatus"></param>
        /// <param name="NewLoadingStatus"></param>
        public static void CorrectLoadingStatus(this WagonInternalOperation wio, ref EFDbContext context, int OldLoadingStatus, int NewLoadingStatus)
        {
            if (wio.IdLoadingStatus == OldLoadingStatus)
            {
                wio.IdLoadingStatus = NewLoadingStatus;
                context.WagonInternalOperations.Update(wio);
                WagonInternalOperation? wio_next = context.WagonInternalOperations.Where(o => o.ParentId == wio.Id).FirstOrDefault();
                if (wio_next != null)
                {
                    wio_next.CorrectLoadingStatus(ref context, OldLoadingStatus, NewLoadingStatus);
                }
                else { return; }
            }
            else return;
        }
        #endregion
        #endregion

        #region Методы работы с позициями вагонов
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="id_way"></param>
        /// <returns></returns>
        public static int GetNextPosition(this EFDbContext context, int id_way)
        {
            int Position = 1;
            List<WagonInternalMovement> wim = context.WagonInternalMovements.Where(m => m.IdWay == id_way && m.WayEnd == null).ToList();
            if (wim != null && wim.Count() > 0)
            {
                Position = wim.Max(m => m.Position) + 1;
            }
            return Position;
        }
        /// <summary>
        /// Вернуть последнюю запись позиции вагона
        /// </summary>
        /// <param name="wir"></param>
        /// <returns></returns>
        public static WagonInternalMovement GetLastMovement(this WagonInternalRoute wir, ref EFDbContext context)
        {
            if (wir.WagonInternalMovements == null) return null;
            WagonInternalMovement wim = context.WagonInternalMovements.Where(m => m.IdWagonInternalRoutes == wir.Id).OrderByDescending(c => c.Id).FirstOrDefault();
            return wim;
        }
        /// <summary>
        /// Вернуть станцию на которой стоит вагон
        /// </summary>
        /// <param name="wir"></param>
        /// <returns></returns>
        public static int? GetCurrentStation(this WagonInternalRoute wir, ref EFDbContext context)
        {
            if (wir == null || wir.WagonInternalMovements == null) return null;
            WagonInternalMovement wim = wir.GetLastMovement(ref context);
            return wim != null ? (int?)wim.IdStation : null;
        }
        /// <summary>
        /// Получить список вагонов на пути 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="id_way"></param>
        /// <returns></returns>
        public static List<WagonInternalMovement> GetMovementWagonsOfWay(this EFDbContext context, int id_way)
        {
            return context.WagonInternalMovements.Where(m => m.IdWay == id_way & m.IdOuterWay == null & m.WayEnd == null).OrderBy(p => p.Position).ToList();
        }
        /// <summary>
        /// Получить список вагонов на пути 
        /// </summary>
        /// <param name="wim"></param>
        /// <param name="id_way"></param>
        /// <returns></returns>
        public static List<WagonInternalMovement> GetMovementWagonsOfWay(this List<WagonInternalMovement> wims, int id_way)
        {
            return wims.Where(m => m.IdWay == id_way & m.IdOuterWay == null & m.WayEnd == null).OrderBy(p => p.Position).ToList();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="id_way"></param>
        /// <returns></returns>
        public static List<int> GetNumWagonsOfWay(this EFDbContext context, int id_way)
        {
            return context.GetMovementWagonsOfWay(id_way).Select(w => w.IdWagonInternalRoutesNavigation.Num).ToList();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="wims"></param>
        /// <param name="id_way"></param>
        /// <returns></returns>
        public static List<int> GetNumWagonsOfWay(this List<WagonInternalMovement> wims, int id_way)
        {
            return wims.GetMovementWagonsOfWay(id_way).Select(w => w.IdWagonInternalRoutesNavigation.Num).ToList();
        }

        #endregion

        #region Методы работы с операциями над вагонами
        public static WagonInternalOperation? GetLastOperation(this WagonInternalRoute wir, ref EFDbContext context)
        {
            if (wir.WagonInternalOperations == null) return null;
            WagonInternalOperation? wio = context.WagonInternalOperations.Where(m => m.IdWagonInternalRoutes == wir.Id).OrderByDescending(c => c.Id).FirstOrDefault();
            return wio;
        }

        public static long? CloseOperation(this WagonInternalOperation wio, DateTime date_end, string? note, int? id_loading_status, int? id_organization_service, string user)
        {
            if (wio == null) return null;
            if (wio.Close == null)
            {
                wio.OperationEnd = wio.OperationEnd == null ? date_end : wio.OperationEnd;
                wio.Note = note != null ? note : wio.Note;
                wio.IdLoadingStatus = id_loading_status != null ? (int)id_loading_status : wio.IdLoadingStatus;
                wio.IdOrganizationService = id_organization_service != null ? id_organization_service : wio.IdOrganizationService;
                wio.Close = date_end;
                wio.CloseUser = user;
            }
            return wio.Id;
        }
        public static long? UpdateOperation(this WagonInternalOperation wio, DateTime? date_start, DateTime? date_end, string? note, int? id_loading_status, int? id_organization_service, string user)
        {
            if (wio == null) return null;
            wio.OperationStart = date_start != null ? (DateTime)date_start : wio.OperationStart;
            wio.OperationEnd = date_end != null ? date_end : wio.OperationEnd;
            wio.Note = note != null ? note : wio.Note;
            wio.IdLoadingStatus = id_loading_status != null ? (int)id_loading_status : wio.IdLoadingStatus;
            wio.IdOrganizationService = id_organization_service != null ? id_organization_service : wio.IdOrganizationService;
            wio.Close = DateTime.Now;
            wio.CloseUser = user;
            return wio.Id;
        }
        #endregion
    }
}
