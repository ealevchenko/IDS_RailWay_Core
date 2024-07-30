using EF_IDS.Concrete;
using EF_IDS.Entities;
using IDS_;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDS.Helper
{
    public static class wir_library
    {
        #region Методы работы с вагонами
        public static WagonInternalRoute GetLastWagon(this EFDbContext context, int num)
        {
            return context.WagonInternalRoutes.Where(r => r.Num == num).OrderByDescending(w => w.Id).FirstOrDefault();
        }

        public static long? CloseWagon(this WagonInternalRoute wir, EFDbContext context, DateTime date_end, string note, string user)
        {
            if (wir == null) return null;
            if (wir.Close == null)
            {
                wir.Note = note != null ? note : wir.Note;
                wir.Close = DateTime.Now;
                wir.CloseUser = user;
                wir.GetLastMovement(ref context).CloseMovement(date_end, note, user);
                wir.GetLastOperation(ref context).CloseOperation(date_end, note, user);
                // Далее добавить закрытие перемещений по требованию
            }
            return wir.Id;
        }

        public static WagonInternalMovement SetStationWagon(this WagonInternalRoute wir, ref EFDbContext context, int id_station, int id_way, DateTime date_start, int position, string note, string user, bool check_replay)
        {
            WagonInternalMovement wim_new = null;
            if (wir != null && wir.Close == null)
            {
                // Получим последнее положение
                WagonInternalMovement wim = wir.GetLastMovement(ref context);
                // Исключим попытку поставить дублирования записи постановки на путь
                if (wim == null || (wim != null && (wim.IdStation != id_station || wim.IdWay != id_way || wim.Position != position || wim.IdOuterWay != null)))
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
                    wir.WagonInternalMovements.Add(wim_new);
                }
            }
            return wim_new;
        }
        //TODO: Удалить после удаления всех старых операций
        /// <summary>
        /// Установить вагон на станцию на путь
        /// </summary>
        /// <param name="wir"></param>
        /// <param name="id_station"></param>
        /// <param name="id_way"></param>
        /// <param name="date_start"></param>
        /// <param name="position"></param>
        /// <param name="note"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        //public static WagonInternalRoute SetStationWagon_old(this WagonInternalRoute wir, ref EFDbContext context, int id_station, int id_way, DateTime date_start, int position, string note, string user)
        //{
        //    if (wir != null && wir.Close == null)
        //    {
        //        WagonInternalMovement wim = wir.GetLastMovement(ref context);
        //        // Исключим попытку поставить дублирования записи постановки на путь
        //        if (wim == null || (wim != null && (wim.IdStation != id_station || wim.IdWay != id_way || wim.Position != position)))
        //        {
        //            WagonInternalMovement wim_new = new WagonInternalMovement()
        //            {
        //                Id = 0,
        //                //id_wagon_internal_routes = wir.Id,
        //                IdStation = id_station,
        //                //station_start = date_start,
        //                IdWay = id_way,
        //                WayStart = date_start,
        //                IdOuterWay = null,
        //                OuterWayStart = null,
        //                OuterWayEnd = null,
        //                Position = position,
        //                Create = DateTime.Now,
        //                CreateUser = user,
        //                Note = note,
        //                ParentId = wim.CloseMovement(date_start, null, user)
        //            };
        //            wir.WagonInternalMovements.Add(wim_new);
        //        }

        //    }
        //    return wir;
        //}
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
                    ParentId = wio_last.CloseOperation(date_start, null, user)
                };

                wir.WagonInternalOperations.Add(wio_new);
            }
            return wio_new;
        }
        public static WagonInternalOperation SetCloseOperation(this WagonInternalOperation wio, DateTime date_end, string note, string user)
        {
            if (wio != null && wio.Close == null)
            {
                wio.CloseOperation(date_end, note, user);
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
            else {
                return wir.Id;
            }
        }

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

        public static long? CloseOperation(this WagonInternalOperation wio, DateTime date_end, string? note, string user)
        {
            if (wio == null) return null;
            if (wio.Close == null)
            {
                wio.OperationEnd = wio.OperationEnd == null ? date_end : wio.OperationEnd;
                wio.Note = note != null ? note : wio.Note;
                wio.Close = date_end;
                wio.CloseUser = user;
            }
            return wio.Id;
        }

        //public static WagonInternalOperation OpenOperation(this WagonInternalOperation wio, DateTime date_end, string user)
        #endregion
    }
}
