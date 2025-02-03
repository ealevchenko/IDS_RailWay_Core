using EF_IDS.Concrete;
using EF_IDS.Entities;
using IDS_;
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
        public static int oper_load_uz = 15;
        public static int oper_load_vz = 16;
        public static int oper_unload_uz = 13;
        public static int oper_unload_vz = 14;

        public static bool IsEmpty(this int? id_status_load)
        {
            return (id_status_load == 0 || id_status_load == 3) ? true : false;
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
                wir.GetLastOperation(ref context).CloseOperation(date_end, note, null, user);
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
                //    // Исключим попытку поставить дублирования записи постановки на путь
                //    if (wim == null || (wim != null && (wim.IdStation != id_station || wim.IdWay != id_way || wim.Position != position || wim.IdOuterWay != null || wim.IdFiling != null)))
                //    {
                //        wim_new = new WagonInternalMovement()
                //        {
                //            Id = 0,
                //            IdStation = id_station,
                //            IdWay = id_way,
                //            WayStart = date_start,
                //            WayEnd = null,
                //            Position = position,
                //            IdOuterWay = null,
                //            OuterWayStart = null,
                //            OuterWayEnd = null,
                //            Create = DateTime.Now,
                //            CreateUser = user,
                //            NumSostav = null,
                //            Note = note,
                //            ParentId = wim.CloseMovement(date_start, null, user),
                //        };
                //        wir.WagonInternalMovements.Add(wim_new);
                //    }
            }
            return wim_new;
        }
        public static WagonInternalMovement SetStationWagon(this WagonInternalMovement wim, ref EFDbContext context, int id_station, int id_way, DateTime date_start, int position, string note, string user, bool check_replay)
        {
            if (wim == null) return null;
            WagonInternalMovement? wim_new = null;
            // Исключим попытку поставить дублирования записи постановки на путь
            if (wim == null || (wim != null && (wim.IdStation != id_station || wim.IdWay != id_way || wim.Position != position || wim.IdOuterWay != null || wim.IdFiling != null || wim.IdFilingNavigation != null)))
            {
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
                    ParentId = wim.CloseMovement(date_start, null, user),
                };
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
        public static long? CloseMovement(this WagonInternalMovement wim, DateTime date_end, string note, string user)
        {
            if (wim == null) return null;
            if (wim.Close == null)
            {
                // Определим какой путь закрывать Внутрений или внешний
                if (wim.IdOuterWay == null)
                {
                    // Закроем внутрений
                    wim.WayEnd = wim.WayEnd == null ? date_end : wim.WayEnd;
                    //wim.station_end = wim.station_end == null ? date_end : wim.station_end;

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
        /// <summary>
        /// Открыть операцию в подаче
        /// </summary>
        /// <param name="wim"></param>
        /// <param name="context"></param>
        /// <param name="wf"></param>
        /// <param name="id_wagon_operations"></param>
        /// <param name="date_start"></param>
        /// <param name="note"></param>
        /// <param name="user"></param>
        /// <returns></returns>

        // Проверка подача закрыта
        public static long IsFreeFiling(this WagonFiling wf, WagonInternalMovement wim)
        {
            if (wf == null) return (int)errors_base.not_wf_db; // подача пуста            
            if (wim == null) return (int)errors_base.not_wim_db; // запись пуста
            if (wim.Close != null || wim.WayEnd != null) return (int)errors_base.close_wim; // запись закрыта
            if (wim.IdOuterWay != null) return (int)errors_base.wagon_not_way; // Вагон не стоит на пути
            if (wim.FilingEnd != null || wf.EndFiling != null) return (int)errors_base.close_wf; // подача закрыта
            if (wim.IdFiling != null && wf.Id > 0 && wim.IdFiling != wf.Id) return (int)errors_base.wim_lock_wf; // Вагон пренадлежит другой подаче
            // Проверим внутренее перемещение вагона, существует? открыто?
            WagonInternalRoute wir = wim.IdWagonInternalRoutesNavigation;
            if (wir == null) return (int)errors_base.not_wir_db; // нет wir
            // Внутренее перемещение существует
            if (wir.Close != null) return (int)errors_base.close_wir; // wir закрыт
            return wim.Id; // 
        }
        public static long SetOpenOperationFiling(this WagonInternalMovement wim, ref EFDbContext context, WagonFiling wf, int? id_wagon_operations, DateTime? date_start, string note, string user)
        {
            // Проверим вагон и подачу на открытость для операции, и добавим в подачу если небыл добавлен
            long res_add = wf.SetAddWagonFiling(wim, user);
            if (res_add < 0) return res_add; // Ошибка
            if (wim.IdFiling != null && wf.Id > 0 && wim.IdFiling == wf.Id && wim.FilingStart != null) return (int)errors_base.wagon_open_operation; // Вагон операция уже применена
            WagonInternalRoute wir = wim.IdWagonInternalRoutesNavigation;
            WagonInternalMovement wim_last = wir.GetLastMovement(ref context);
            if (wim_last != null && wim_last.Id != wim.Id) return (int)errors_base.err_last_wim_db; // Ошибка позиция вагона несоответсвует последней позиции в базе
            WagonInternalOperation? wio = wim.IdWioNavigation; // Последняя операция над вагоном
            WagonInternalOperation wio_last = wir.GetLastOperation(ref context);
            if (wio != null && wio_last.Id != wio.Id) return (int)errors_base.wagon_not_operation; // Ошибка операция вагона не соответствует последней
            if (id_wagon_operations != null)
            {
                if (wio == null || wio.Close != null)
                {
                    if (date_start != null)
                    {
                        // Создать операцию
                        WagonInternalOperation new_operation = wir.SetOpenOperation(ref context, (int)id_wagon_operations, (DateTime)date_start, null, null, null, null, note, user);
                        wim.FilingStart = date_start;
                        wim.IdWioNavigation = new_operation; // добавим новую операцию
                        long res_of = wf.SetOpenFiling(user); // Обновим общее начало в подаче + обновим кто произвел обновление
                        return res_of < 0 ? res_of : wim.Id;
                    }
                    else
                    {
                        return 0; // Указана операция но нет старта
                    }
                }
                else
                {
                    if (wio.IdOperation == id_wagon_operations && wio.Close == null)
                    {
                        if (date_start != null && wio.OperationEnd == null)
                        {
                            wio.OperationStart = (DateTime)date_start;
                            wim.FilingStart = date_start;
                            long res_of = wf.SetOpenFiling(user); // Обновим общее начало в подаче + обновим кто произвел обновление
                            return res_of < 0 ? res_of : wim.Id;
                        }
                        else
                        {
                            return 0; // Указана операция уже закрыта или нет даннх начала
                        }
                    }
                    else
                    {
                        return (int)errors_base.wagon_not_operation; // Операция вагона не соответсвует выбраной
                    }
                }
            }
            else
            {
                if (wio != null && wio.OperationEnd == null && date_start == null)
                {
                    // Удалим операцию если id_операции не указано wio есть
                    wim.FilingStart = null;
                    context.WagonInternalOperations.Remove(wio);
                    long res_of = wf.SetOpenFiling(user); // Обновим общее начало в подаче + обновим кто произвел обновление
                    return res_of < 0 ? res_of : wim.Id;
                }
                else
                {
                    return 0; // Указана операция уже закрыта или ее еще нет
                }
            }
        }
        /// <summary>
        /// Закрыть операцию в подаче (проверить если все закрыты закрыть подачу и обновить время закрытия подачи)
        /// </summary>
        /// <param name="wim"></param>
        /// <param name="context"></param>
        /// <param name="wf"></param>
        /// <param name="date_stop"></param>
        /// <param name="id_status_load"></param>
        /// <param name="note"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public static long SetCloseOperationFiling(this WagonInternalMovement wim, ref EFDbContext context, WagonFiling wf, DateTime date_stop, int id_status_load, string note, string user)
        {
            long result = wf.IsFreeFiling(wim);
            if (result <= 0) return result;// Ошибка
            if (wim.IdFiling != null && wf.Id > 0 && wim.IdFiling == wf.Id && wim.FilingEnd != null) return (int)errors_base.wagon_close_operation; // Вагон операция закрыта
            WagonInternalRoute wir = wim.IdWagonInternalRoutesNavigation;
            WagonInternalMovement wim_last = wir.GetLastMovement(ref context);
            if (wim_last != null && wim_last.Id != wim.Id) return (int)errors_base.err_last_wim_db; // Ошибка позиция вагона несоответсвует последней позиции в базе
            WagonInternalOperation? wio = wim.IdWioNavigation;
            if (wio == null) return (int)errors_base.not_wio_db; // В базе данных нет записи по WagonInternalOperation (Внутренняя операция по вагону)
            WagonInternalOperation wio_last = wir.GetLastOperation(ref context);
            if (wio != null && wio.Id > 0 && wio_last.Id != wio.Id) return (int)errors_base.wagon_not_operation; // Ошибка операция вагона не соответствует последней
            // Закроем операцию и позицию создадим новую строку
            wio.SetCloseOperation((DateTime)date_stop, null, id_status_load, user);
            wim.FilingEnd = date_stop;
            //wim.CloseMovement((DateTime)date_stop, note, user);
            // Создать новую позицию закрыв старую
            WagonInternalMovement? wim_new = wir.SetStationWagon(ref context, wim.IdStation, wim.IdWay, (DateTime)date_stop, wim.Position, note, user, true);
            wf.Change = DateTime.Now;
            wf.ChangeUser = user;
            if (wim_new != null)
            {
                return wim.Id;
            }
            else
            {
                return (int)errors_base.err_create_wim_db; // Ошибка создания новой позиции вагона.
            }
        }
        /// <summary>
        /// Закрыть уже созданную операцию в подаче (проверить если подача закрыта или введен документ тогда запрет)
        /// </summary>
        /// <param name="wim"></param>
        /// <param name="context"></param>
        /// <param name="wf"></param>
        /// <param name="date_start"></param>
        /// <param name="date_stop"></param>
        /// <param name="id_status_load"></param>
        /// <param name="note"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public static long SetUpdateOperationFiling(this WagonInternalMovement wim, ref EFDbContext context, WagonFiling wf, DateTime? date_start, DateTime? date_stop, int id_status_load, string note, string user)
        {
            if (wf.Close != null) return (int)errors_base.close_wf; // подача закрыта
            if (wim.IdFiling != null && wf.Id > 0 && wim.IdFiling == wf.Id && (wim.FilingStart == null || wim.FilingEnd == null)) return (int)errors_base.wagon_not_operation; // По вагону нет операций
            WagonInternalRoute wir = wim.IdWagonInternalRoutesNavigation;
            if (wir == null) return (int)errors_base.not_wir_db; // нет wir
            // Внутренее перемещение существует
            if (wir.Close != null) return (int)errors_base.close_wir; // wir закрыт
            WagonInternalOperation? wio = wim.IdWioNavigation;
            if (wio == null) return (int)errors_base.not_wio_db; // В базе данных нет записи по WagonInternalOperation (Внутренняя операция по вагону)
            wio.UpdateOperation(date_start, date_stop, null, id_status_load, user);
            wim.FilingStart = date_start != null ? date_start : wim.FilingStart;
            wim.FilingEnd = date_stop != null ? date_stop : wim.FilingEnd;
            wf.Change = DateTime.Now;
            wf.ChangeUser = user;
            return wim.Id;
        }

        #endregion

        #region WagonFiling
        /// <summary>
        /// Закрыть подачу
        /// </summary>
        /// <param name="wf"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public static long SetCloseFiling(this WagonFiling wf, string user)
        {
            if (wf == null) return (int)errors_base.not_wf_db; // подача пуста   
            if (wf.Close != null) return (int)errors_base.close_wf; // подача закрыта
            int count = wf.WagonInternalMovements.Count();
            int count_close = wf.WagonInternalMovements.Where(m => m.FilingEnd != null).Count();

            if (wf.TypeFiling == 1 && count == count_close)
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

                    WagonInternalMoveCargo? wimc = wim.WagonInternalMoveCargoIdWimLoadNavigations.FirstOrDefault(w => w.Close == null);
                    if (wimc == null ||
                        (wio != null && wimc.DocReceived == null && wio.IdLoadingStatus != 0) //|| 
                                                                                              //(wio != null && wimc.DocReceived != null && wio.IdLoadingStatus == 0 )
                        )
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
        /// Начать или сбросить начало операций в подаче
        /// </summary>
        /// <param name="wf"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public static long SetOpenFiling(this WagonFiling wf, string user)
        {
            if (wf.Close != null) return (int)errors_base.close_wf; // подача закрыта
            WagonInternalMovement? wim_open_min = wf.WagonInternalMovements.Where(m => m.FilingStart != null).OrderBy(c => c.FilingStart).FirstOrDefault();
            DateTime? start = wim_open_min != null ? wim_open_min.FilingStart : null;
            if (wf.StartFiling != start)
            {
                wf.StartFiling = start;
                wf.Change = DateTime.Now;
                wf.ChangeUser = user;
                return wf.Id;
            }
            return 0;
        }
        /// <summary>
        /// Добавить вагон в подачу
        /// </summary>
        /// <param name="wf"></param>
        /// <param name="wim"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public static long SetAddWagonFiling(this WagonFiling wf, WagonInternalMovement wim, string user)
        {
            long result = wf.IsFreeFiling(wim);
            if (result <= 0) return result;
            // Если wim не пренадлежит подаче, тогда добавим в подачу
            if (wim.IdFiling == null)
            {
                wim.IdFilingNavigation = wf;
                wf.WagonInternalMovements.Add(wim);
                wf.Change = DateTime.Now;
                wf.ChangeUser = user;
                return wim.Id;
            }
            return 0;
        }
        /// <summary>
        /// Убрать вагон из подачи
        /// </summary>
        /// <param name="wf"></param>
        /// <param name="wim"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public static long SetDeleteWagonFiling(this WagonFiling wf, ref EFDbContext context, WagonInternalMovement wim, string user)
        {
            long result = wf.IsFreeFiling(wim);
            if (result <= 0) return result;
            // Если wim не пренадлежит подаче, тогда добавим в подачу
            if (wf.Id != wim.IdFiling) return (int)errors_base.wim_lock_wf;                                 // вагон пренадлежит другой подаче
            if (wf.Id == wim.IdFiling && (wim.FilingStart != null)) return (int)errors_base.wim_open_wf;    // Вагон заблокирован, операция в подаче уже открыта
            wf.WagonInternalMovements.Remove(wim);
            // Проверка на пустую подачу
            if (wf.WagonInternalMovements == null || wf.WagonInternalMovements.Count() == 0)
            {
                context.WagonFilings.Remove(wf); // удалить
                return wim.Id;
            }
            else
            {
                wf.Change = DateTime.Now;
                wf.ChangeUser = user;
                long res = wf.SetCloseFiling(user);
                if (res < 0)
                {
                    return res;
                }
                else
                {
                    return wim.Id;
                }
            }
        }

        #endregion

        #region WagonInternalMoveCargo
        // Получить последнюю запись внутризаводского груза
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
            // Получим последнюю запись груза перемещаемого на предприятии
            WagonInternalMoveCargo? wimc = wir.GetLastMoveCargo(ref context);


            if (wimc == null || wimc != null && wimc.IdWimLoad != wim.Id && wimc.Empty == true && !wagon.id_status_load.IsEmpty())
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
                context.WagonInternalMoveCargos.Add(new_wimc);
                return wim.Id;
            }
            else
            {
                if (wimc != null && wimc.IdWimLoad != null && wimc.IdWimLoad == wim.Id && wimc.IdWimRedirection == null && wimc.DocReceived == null)
                {
                    // Перемещение груза есть, и операция погрузки совподает
                    wimc.InternalDocNum = String.IsNullOrWhiteSpace(wf.NumFiling) ? wagon.num_nakl : null;
                    wimc.IdWeighingNum = null;
                    //wimc.DocReceived = wf.DocReceived == null ? wagon.doc_received : wf.DocReceived;
                    wimc.DocReceived = wagon.doc_received!=null ? wagon.doc_received : null;
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
        /// Закрыть строку выгрузки
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
            WagonInternalMoveCargo? wimc = wir.GetLastMoveCargo(ref context);// Получим последнюю запись груза перемещаемого на предприятии
            if (wimc == null || wimc != null && wimc.Empty != true && wagon.id_status_load.IsEmpty())
            {
                // Закроем груз с признаком не пустой  (Вагоны не порожние)
                if (wimc != null && wimc.Empty == false)
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
        public static WagonInternalOperation SetOpenOperation(this WagonInternalRoute wir, ref EFDbContext context, int id_operation, DateTime date_start, int? id_condition, int? id_loading_status, string locomotive1, string locomotive2, string note, string user)
        {
            WagonInternalOperation wio_new = null;

            if (wir != null && wir.Close == null)
            {
                WagonInternalOperation wio_last = wir.GetLastOperation(ref context);
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
                    ParentId = wio_last.CloseOperation(date_start, null, id_loading_status, user)
                };

                wir.WagonInternalOperations.Add(wio_new);
            }
            return wio_new;
        }
        public static WagonInternalOperation SetCloseOperation(this WagonInternalOperation wio, DateTime date_end, string note, int? id_loading_status, string user)
        {
            if (wio != null && wio.Close == null)
            {
                wio.CloseOperation(date_end, note, id_loading_status, user);
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
        #endregion
        #endregion


        #region Методы работы с позициями вагонов

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

        public static List<int> GetNumWagonsOfWay(this EFDbContext context, int id_way)
        {
            return context.GetMovementWagonsOfWay(id_way).Select(w => w.IdWagonInternalRoutesNavigation.Num).ToList();
        }
        public static List<int> GetNumWagonsOfWay(this List<WagonInternalMovement> wims, int id_way)
        {
            return wims.GetMovementWagonsOfWay(id_way).Select(w => w.IdWagonInternalRoutesNavigation.Num).ToList();
        }

        #endregion

        #region Методы работы с операциями над вагонами
        public static WagonInternalOperation GetLastOperation(this WagonInternalRoute wir, ref EFDbContext context)
        {
            if (wir.WagonInternalOperations == null) return null;
            WagonInternalOperation wio = context.WagonInternalOperations.Where(m => m.IdWagonInternalRoutes == wir.Id).OrderByDescending(c => c.Id).FirstOrDefault();
            return wio;
        }

        public static long? CloseOperation(this WagonInternalOperation wio, DateTime date_end, string? note, int? id_loading_status, string user)
        {
            if (wio == null) return null;
            if (wio.Close == null)
            {
                wio.OperationEnd = wio.OperationEnd == null ? date_end : wio.OperationEnd;
                wio.Note = note != null ? note : wio.Note;
                wio.IdLoadingStatus = id_loading_status != null ? (int)id_loading_status : wio.IdLoadingStatus;
                wio.Close = date_end;
                wio.CloseUser = user;
            }
            return wio.Id;
        }
        public static long? UpdateOperation(this WagonInternalOperation wio, DateTime? date_start, DateTime? date_end, string? note, int? id_loading_status, string user)
        {
            if (wio == null) return null;
            wio.OperationStart = date_start != null ? (DateTime)date_start : wio.OperationStart;
            wio.OperationEnd = date_end != null ? date_end : wio.OperationEnd;
            wio.Note = note != null ? note : wio.Note;
            wio.IdLoadingStatus = id_loading_status != null ? (int)id_loading_status : wio.IdLoadingStatus;
            wio.Close = DateTime.Now;
            wio.CloseUser = user;
            return wio.Id;
        }

        //public static WagonInternalOperation OpenOperation(this WagonInternalOperation wio, DateTime date_end, string user)
        #endregion
    }
}
