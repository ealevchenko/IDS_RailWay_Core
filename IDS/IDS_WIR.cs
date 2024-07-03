using EF_IDS.Concrete;
using EF_IDS.Concrete.Directory;
using EF_IDS.Concrete.Outgoing;
using EF_IDS.Concrete.Arrival;
using EF_IDS.Entities;
using IDS.Helper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GIVC;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Runtime.ConstrainedExecution;
using System.Data;

namespace IDS_
{
    /// <summary>
    /// Класс данных задание на операции дислокация, отправка, прием
    /// </summary>
    public class ListOperationWagon
    {
        public long wir_id { get; set; }
        public int position { get; set; }
    }
    /// <summary>
    /// Класс данных расчета платы пользования по вагону
    /// </summary>
    public class CalcWagonUsageFee
    {
        public long IdWir { get; set; }
        public int? Num { get; set; }
        public int? IdOperator { get; set; }
        public int? IdGenus { get; set; }
        public bool? UzWagon { get; set; }
        public DateTime? DateAdoption { get; set; }
        public DateTime? DateOutgoing { get; set; }
        public bool? Route { get; set; }
        public bool? InpCargo { get; set; }
        public bool? OutCargo { get; set; }
        public int? IdCargoArr { get; set; }
        public int? IdCargoOut { get; set; }
        public int? CodeStnFrom { get; set; }
        public int? CodeStnTo { get; set; }
        public bool? Derailment { get; set; }
        public int? CountStage { get; set; }
        public int? IdCurrency { get; set; }
        public decimal? Rate { get; set; }
        public decimal? ExchangeRate { get; set; }
        public double? Coefficient { get; set; }
        public int? UseTime { get; set; }
        public int? GraceTime { get; set; }
        public int? CalcTime { get; set; }
        public decimal? CalcFeeAmount { get; set; }
        public int? Downtime { get; set; }
        public int error { get; set; }
    }
    //public class ResultCalcWagonUsageFee
    //{
    //    public CalcWagonUsageFee? CalcWagonUsageFee { get; set; }
    //    public int error { get; set; }
    //}

    /// <summary>
    /// Класс данных WIR c новой позицией
    /// </summary>
    public class WagonInternalRoutesPosition
    {
        public WagonInternalRoute wir { get; set; }
        public int new_position { get; set; }
    }
    public class IDS_WIR : IDS_Base
    {
        private DbContextOptions<EFDbContext> options;
        private readonly ILogger<Object> _logger;
        private readonly IConfiguration _configuration;
        EventId _eventId = new EventId(0);
        private String? connectionString;

        private List<int> list_groups_cargo = new List<int>() { 11, 16, 20, 24 }; // Список id групп груза с порожними вагонами

        public IDS_WIR(ILogger<Object> logger, IConfiguration configuration) : base()
        {
            _logger = logger;
            _configuration = configuration;
            _eventId = int.Parse(_configuration["EventID:IDS_WIR"]);
            SetupDB(configuration);
        }
        public IDS_WIR(ILogger<Object> logger, IConfiguration configuration, EventId rootId) : base()
        {
            _logger = logger;
            _configuration = configuration;
            _eventId = rootId.Id + int.Parse(_configuration["EventID:IDS_WIR"]);
            SetupDB(configuration);
        }
        public void SetupDB(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("IDS");
            var optionsBuilder = new DbContextOptionsBuilder<EFDbContext>();
            this.options = optionsBuilder.UseSqlServer(connectionString).Options;
        }

        #region АРЕНДЫ ВАГОНОВ (ОПЕРАТОРЫ) - ПРАВКА, ОБНОВЛЕНИЕ, ИСПРАВЛЕНИЕ
        #region Прибытие составов
        /// <summary>
        /// Обновить по принятому составу оператора АМКР (Аренду)
        /// </summary>
        /// <param name="context"></param>
        /// <param name="id_arrival_sostav"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public ResultUpdateWagon UpdateOperationArrivalSostav(ref EFDbContext context, long id_arrival_sostav, string user)
        {
            ResultUpdateWagon result = new ResultUpdateWagon(0);
            try
            {
                if (context == null)
                {
                    context = new EFDbContext(this.options);
                };
                // Проверим и скорректируем пользователя
                if (String.IsNullOrWhiteSpace(user))
                {
                    user = System.Environment.UserDomainName + @"\" + System.Environment.UserName;
                }
                EFArrivalSostav ef_arr_sostav = new EFArrivalSostav(context);
                EFArrivalCar ef_arr_car = new EFArrivalCar(context);
                EFArrivalUzVagon ef_arr_vag = new EFArrivalUzVagon(context);
                EFDirectoryWagonsRent ef_wag_rent = new EFDirectoryWagonsRent(context);
                ArrivalSostav? sostav = ef_arr_sostav.Context.Where(s => s.Id == id_arrival_sostav).FirstOrDefault();
                if (sostav != null)
                {
                    // Состав принят
                    if (sostav.Status == 2)
                    {
                        List<ArrivalCar> cars = ef_arr_car.Context.Where(c => c.IdArrival == sostav.Id && c.PositionArrival != null).ToList();
                        result.count = cars.Count();
                        _logger.LogInformation(_eventId, "По сотаву {0} Определено {1} вагонов", id_arrival_sostav, result.count);
                        //Console.WriteLine("По сотаву {0} Определено {1} вагонов", id_outgoing_sostav, result.count);
                        foreach (ArrivalCar car in cars)
                        {
                            ArrivalUzVagon? vag = ef_arr_vag.Context.Where(v => v.Id == car.IdArrivalUzVagon).FirstOrDefault();
                            if (vag != null)
                            {
                                // Получим аренды по данному вагону
                                DirectoryWagonsRent? rent = ef_wag_rent.Context.Where(r => r.Num == vag.Num && r.RentStart <= sostav.DateAdoption && r.RentEnd > sostav.DateAdoption).FirstOrDefault();
                                DirectoryWagonsRent? rent_null = ef_wag_rent.Context.Where(r => r.Num == vag.Num && r.RentStart <= sostav.DateAdoption && r.RentEnd == null).OrderByDescending(c => c.Id).FirstOrDefault();
                                int? id_wagons_rent_arrival = null;
                                id_wagons_rent_arrival = rent != null ? (int?)rent.Id : rent_null != null ? (int?)rent_null.Id : null;
                                // если аренда новая и записаная - разные, изменим аренду
                                if (id_wagons_rent_arrival != null && vag.IdWagonsRentArrival != id_wagons_rent_arrival)
                                {
                                    vag.IdWagonsRentArrival = id_wagons_rent_arrival;
                                    vag.Change = DateTime.Now;
                                    vag.ChangeUser = user;
                                    result.SetUpdateResult(1, vag.Num);
                                    _logger.LogInformation(_eventId, "По вагону {0} определена замена аренды по прибытию {1}", vag.Num, id_wagons_rent_arrival);
                                }
                                else
                                {
                                    result.SetSkipResult(0, vag.Num);
                                }
                            }
                        }
                        if (result.update > 0)
                        {
                            result.SetResult(context.SaveChanges());
                        }
                    }
                    else
                    {
                        result.SetResult((int)errors_base.error_status_arrival_sostav); // Ошибка статуса состава (Статус не позволяет сделать эту операцию)
                    }
                }
                else
                {
                    result.SetResult((int)errors_base.not_arrival_sostav_db); //В базе данных нет записи состава
                }
                _logger.LogInformation(_eventId, "По составу {0} определено {1} вагонов (обновновить: {2}, пропустить :{3}), результат обновления :{4}", id_arrival_sostav, result.count, result.update, result.skip, result.result);
                return result;
            }
            catch (Exception e)
            {
                _logger.LogError(_eventId, e, "UpdateOperationArrivalSostav(context={0}, id_outgoing_sostav={1}, user={2})", context, id_arrival_sostav, user);
                result.SetResult((int)errors_base.global);
                return result;// Возвращаем id=-1 , Ошибка
            }
        }
        /// <summary>
        /// Обновить по принятому составу оператора АМКР (Аренду)
        /// </summary>
        /// <param name="id_arrival_sostav"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public ResultUpdateWagon UpdateOperationArrivalSostav(long id_arrival_sostav, string user)
        {
            ResultUpdateWagon rt = new ResultUpdateWagon(0);
            try
            {
                EFDbContext context = new EFDbContext(this.options);
                // Проверим и скорректируем пользователя
                if (String.IsNullOrWhiteSpace(user))
                {
                    user = System.Environment.UserDomainName + @"\" + System.Environment.UserName;
                }
                return UpdateOperationArrivalSostav(ref context, id_arrival_sostav, user);
            }
            catch (Exception e)
            {
                _logger.LogError(_eventId, e, "UpdateOperationArrivalSostav(id_outgoing_sostav={0}, user={1})", id_arrival_sostav, user);
                rt.SetResult((int)errors_base.global);
                return rt;// Возвращаем id=-1 , Ошибка
            }
        }
        /// <summary>
        /// Обновить по принятому составу оператора АМКР (Аренду)
        /// </summary>
        /// <param name="date_adoption"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public OperationResultID UpdateOperationArrivalSostav(DateTime date_adoption, string user)
        {
            OperationResultID rt = new OperationResultID();
            try
            {
                EFDbContext context = new EFDbContext(this.options);

                // Проверим и скорректируем пользователя
                if (String.IsNullOrWhiteSpace(user))
                {
                    user = System.Environment.UserDomainName + @"\" + System.Environment.UserName;
                }
                EFArrivalSostav ef_arr_sostav = new EFArrivalSostav(context);
                List<ArrivalSostav> list_sostav = ef_arr_sostav.Context.Where(s => s.DateAdoption >= date_adoption).ToList();
                foreach (ArrivalSostav sost in list_sostav)
                {
                    ResultUpdateWagon rt_st = UpdateOperationArrivalSostav(sost.Id, user);
                    rt.SetResultOperation(rt_st.result, sost.Id);
                }
                _logger.LogInformation(_eventId, "Выполнение завершено, определено {0} составов, код выполнения {1}", rt.listResult.Count(), rt.result);

                return rt;
            }
            catch (Exception e)
            {
                _logger.LogError(_eventId, e, "UpdateOperationArrivalSostav(date_outgoing={0}, user={1})", date_adoption, user);
                rt.SetResult((int)errors_base.global);
                return rt;// Возвращаем id=-1 , Ошибка
            }
        }
        #endregion

        #region Отправка составов
        /// <summary>
        /// Обновить по сданному или отправленному составу оператора АМКР (Аренду)
        /// </summary>
        /// <param name="context"></param>
        /// <param name="id_outgoing_sostav"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public ResultUpdateWagon UpdateOperationOutgoingSostav(ref EFDbContext context, long id_outgoing_sostav, string user)
        {
            ResultUpdateWagon result = new ResultUpdateWagon(0);
            try
            {
                if (context == null)
                {
                    context = new EFDbContext(this.options);
                };
                // Проверим и скорректируем пользователя
                if (String.IsNullOrWhiteSpace(user))
                {
                    user = System.Environment.UserDomainName + @"\" + System.Environment.UserName;
                }
                EFOutgoingSostav ef_out_sostav = new EFOutgoingSostav(context);
                EFOutgoingCar ef_out_car = new EFOutgoingCar(context);
                EFOutgoingUzVagon ef_out_vag = new EFOutgoingUzVagon(context);
                EFDirectoryWagonsRent ef_wag_rent = new EFDirectoryWagonsRent(context);
                OutgoingSostav? sostav = ef_out_sostav.Context.Where(s => s.Id == id_outgoing_sostav).FirstOrDefault();
                //List<ChangeID> del_id_wr = new List<ChangeID>(); // Список id rent для удаления

                if (sostav != null)
                {
                    // Состав определен (сдан или отправлен)
                    if (sostav.Status >= 2 && sostav.Status <= 3)
                    {
                        List<OutgoingCar> cars = ef_out_car.Context.Where(c => c.IdOutgoing == sostav.Id && c.PositionOutgoing != null).ToList();
                        result.count = cars.Count();
                        _logger.LogInformation(_eventId, "По сотаву {0} Определено {1} вагонов", id_outgoing_sostav, result.count);
                        //Console.WriteLine("По сотаву {0} Определено {1} вагонов", id_outgoing_sostav, result.count);
                        foreach (OutgoingCar car in cars)
                        {
                            OutgoingUzVagon? vag = ef_out_vag.Context.Where(v => v.Id == car.IdOutgoingUzVagon).FirstOrDefault();
                            if (vag != null)
                            {
                                // Получим аренды по данному вагону
                                DirectoryWagonsRent? rent = ef_wag_rent.Context.Where(r => r.Num == vag.Num && r.RentStart <= sostav.DateOutgoing && r.RentEnd > sostav.DateOutgoing).FirstOrDefault();
                                DirectoryWagonsRent? rent_null = ef_wag_rent.Context.Where(r => r.Num == vag.Num && r.RentStart <= sostav.DateOutgoing && r.RentEnd == null).OrderByDescending(c => c.Id).FirstOrDefault();
                                int? id_wagons_rent_outgoing = null;
                                id_wagons_rent_outgoing = rent != null ? (int?)rent.Id : rent_null != null ? (int?)rent_null.Id : null;
                                // если аренда новая и записаная - разные, изменим аренду
                                if (id_wagons_rent_outgoing != null && vag.IdWagonsRentOutgoing != id_wagons_rent_outgoing)
                                {
                                    vag.IdWagonsRentOutgoing = id_wagons_rent_outgoing;
                                    vag.Change = DateTime.Now;
                                    vag.ChangeUser = user;
                                    result.SetUpdateResult(1, vag.Num);
                                    _logger.LogInformation(_eventId, "По вагону {0} определена замена аренды на отправку {1}", vag.Num, id_wagons_rent_outgoing);
                                }
                                else
                                {
                                    result.SetSkipResult(0, vag.Num);
                                }
                            }
                        }
                        if (result.update > 0)
                        {
                            result.SetResult(context.SaveChanges());
                        }
                    }
                    else
                    {
                        result.SetResult((int)errors_base.error_status_outgoing_sostav); // Ошибка статуса состава (Статус не позволяет сделать эту операцию)
                    }
                }
                else
                {
                    result.SetResult((int)errors_base.not_outgoing_sostav_db); //В базе данных нет записи состава
                }
                _logger.LogInformation(_eventId, "По составу {0} определено {1} вагонов (обновновить: {2}, пропустить :{3}), результат обновления :{4}", id_outgoing_sostav, result.count, result.update, result.skip, result.result);
                return result;
            }
            catch (Exception e)
            {
                _logger.LogError(_eventId, e, "UpdateOperationOutgoingSostav(context={0}, id_outgoing_sostav={1}, user={2})", context, id_outgoing_sostav, user);
                result.SetResult((int)errors_base.global);
                return result;// Возвращаем id=-1 , Ошибка
            }
        }
        /// <summary>
        /// Обновить по сданному или отправленному составу оператора АМКР (Аренду)
        /// </summary>
        /// <param name="id_outgoing_sostav"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public ResultUpdateWagon UpdateOperationOutgoingSostav(long id_outgoing_sostav, string user)
        {
            ResultUpdateWagon rt = new ResultUpdateWagon(0);
            try
            {
                EFDbContext context = new EFDbContext(this.options);
                // Проверим и скорректируем пользователя
                if (String.IsNullOrWhiteSpace(user))
                {
                    user = System.Environment.UserDomainName + @"\" + System.Environment.UserName;
                }
                return UpdateOperationOutgoingSostav(ref context, id_outgoing_sostav, user);
            }
            catch (Exception e)
            {
                _logger.LogError(_eventId, e, "UpdateOperationOutgoingSostav(id_outgoing_sostav={0}, user={1})", id_outgoing_sostav, user);
                rt.SetResult((int)errors_base.global);
                return rt;// Возвращаем id=-1 , Ошибка
            }
        }
        /// <summary>
        /// Обновить по сданному или отправленному составу оператора АМКР (Аренду)
        /// </summary>
        /// <param name="date_outgoing"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public OperationResultID UpdateOperationOutgoingSostav(DateTime date_outgoing, string user)
        {
            OperationResultID rt = new OperationResultID();
            try
            {
                EFDbContext context = new EFDbContext(this.options);

                // Проверим и скорректируем пользователя
                if (String.IsNullOrWhiteSpace(user))
                {
                    user = System.Environment.UserDomainName + @"\" + System.Environment.UserName;
                }
                EFOutgoingSostav ef_out_sostav = new EFOutgoingSostav(context);
                //OutgoingSostav sostav = ef_out_sostav.Get(210619);               
                List<OutgoingSostav> list_sostav = ef_out_sostav.Context.Where(s => s.DateOutgoing >= date_outgoing).ToList();
                foreach (OutgoingSostav sost in list_sostav)
                {
                    ResultUpdateWagon rt_st = UpdateOperationOutgoingSostav(sost.Id, user);
                    rt.SetResultOperation(rt_st.result, sost.Id);
                }
                _logger.LogInformation(_eventId, "Выполнение завершено, определено {0} составов, код выполнения {1}", rt.listResult.Count(), rt.result);

                return rt;
            }
            catch (Exception e)
            {
                _logger.LogError(_eventId, e, "UpdateOperationOutgoingSostav(date_outgoing={0}, user={1})", date_outgoing, user);
                rt.SetResult((int)errors_base.global);
                return rt;// Возвращаем id=-1 , Ошибка
            }
        }
        /// <summary>
        /// Очистить задвоение операторов АМКР (Аренд)
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public ResultUpdateWagon ClearDoubling_Directory_WagonsRent(string user)
        {
            ResultUpdateWagon result = new ResultUpdateWagon(0);
            try
            {
                EFDbContext context = new EFDbContext(this.options);

                // Проверим и скорректируем пользователя
                if (String.IsNullOrWhiteSpace(user))
                {
                    user = System.Environment.UserDomainName + @"\" + System.Environment.UserName;
                }
                EFDirectoryWagonsRent ef_wag_rent = new EFDirectoryWagonsRent(context);
                EFArrivalUzVagon ef_arr_vag = new EFArrivalUzVagon(context);
                EFOutgoingUzVagon ef_out_vag = new EFOutgoingUzVagon(context);
                List<DirectoryWagonsRent> list = ef_wag_rent.Context.Where(r => r.ParentId != null).ToList();
                List<IGrouping<int?, DirectoryWagonsRent>> grents = list.GroupBy(r => r.ParentId).ToList().Where(c => c.Count() > 1).OrderBy(k => k.Key).ToList();
                result.count = grents.Count;
                _logger.LogInformation(_eventId, "Определено {0} задублированных аренд вагонов", result.count);
                foreach (IGrouping<int?, DirectoryWagonsRent> gr_wr in grents.ToList())
                {
                    int num = gr_wr.Min(c => c.Num);
                    bool closes = false; // признак в дублировании есть закрытая запись?
                    int count_arr_vag = 0;
                    int count_out_vag_rent_arr = 0;
                    int count_out_vag_rent_out = 0;
                    _logger.LogInformation(_eventId, "Обрабатываю вагон № {0}", num);
                    List<DirectoryWagonsRent> list_gr = gr_wr.Where(r => r.RentEnd == null).ToList();
                    DirectoryWagonsRent cur_wr = null; // Получим строку аренды которую оставим
                    if (list_gr.Count == gr_wr.Count())
                    {
                        // нет закрытий
                        // Получим строку аренды которую оставим
                        cur_wr = gr_wr.OrderByDescending(c => c.Id).FirstOrDefault();
                    }
                    else
                    {
                        // Получим строку аренды которую оставим
                        cur_wr = gr_wr.Where(r => r.RentEnd != null).OrderByDescending(c => c.Id).FirstOrDefault();
                        closes = true;

                    }
                    // Получим список для удаления
                    List<DirectoryWagonsRent> del_wr = gr_wr.Where(r => r.Id != cur_wr.Id).ToList();
                    // переводим на аренду которую оставили
                    foreach (DirectoryWagonsRent rent in del_wr)
                    {
                        List<ArrivalUzVagon> list_arr_vag = ef_arr_vag.Context.Where(a => a.IdWagonsRentArrival == rent.Id).ToList();
                        count_arr_vag = list_arr_vag.Count();
                        foreach (ArrivalUzVagon arr_vag in list_arr_vag)
                        {
                            arr_vag.IdWagonsRentArrival = cur_wr.Id;
                            ef_arr_vag.Update(arr_vag);
                        }
                        List<OutgoingUzVagon> list_out_vag_rent_arr = ef_out_vag.Context.Where(a => a.IdWagonsRentArrival == rent.Id).ToList();
                        List<OutgoingUzVagon> list_out_vag_rent_out = ef_out_vag.Context.Where(a => a.IdWagonsRentOutgoing == rent.Id).ToList();
                        count_out_vag_rent_arr = list_out_vag_rent_arr.Count();
                        count_out_vag_rent_out = list_out_vag_rent_out.Count();
                        foreach (OutgoingUzVagon out_vag in list_out_vag_rent_arr)
                        {
                            out_vag.IdWagonsRentArrival = cur_wr.Id;
                            ef_out_vag.Update(out_vag);
                        }
                        foreach (OutgoingUzVagon out_vag in list_out_vag_rent_out)
                        {
                            out_vag.IdWagonsRentOutgoing = cur_wr.Id;
                            ef_out_vag.Update(out_vag);
                        }
                        ef_wag_rent.Delete(rent.Id);
                    }
                    // Обновим начало аренды
                    DirectoryWagonsRent parent_wr = ef_wag_rent.Context.Where(r => r.Id == cur_wr.ParentId).FirstOrDefault();
                    if (parent_wr != null && parent_wr.RentEnd != null)
                    {
                        cur_wr.RentStart = parent_wr.RentEnd;
                        ef_wag_rent.Update(cur_wr);
                    }
                    int res_update = context.SaveChanges();
                    result.SetUpdateResult(res_update, num);

                    _logger.LogInformation(_eventId, "Вагон {0} [Задублированных строк {1}, присутсвие закрытых строк {2}, обновить аренду в документах прибытие {3} и отправки {4}:{5}] - Код выполнения : {6}",
                        num, list_gr.Count(), closes.ToString(), count_arr_vag, count_out_vag_rent_arr, count_out_vag_rent_out, res_update);
                }
                _logger.LogInformation(_eventId, "Выполнение очистки задублированных записей аренд вагонов завершено, определено {0} составов, код выполнения {1}", grents.Count(), result.result);
                return result;
            }
            catch (Exception e)
            {
                _logger.LogError(_eventId, e, "ClearDoubling_Directory_WagonsRent(user={0})", user);
                result.SetResult((int)errors_base.global);
                return result;// Возвращаем id=-1 , Ошибка
            }
        }
        #endregion
        #endregion

        #region ВНУТРЕНЕЕ ПЕРЕМЕЩЕНИЕ - АРМ ДИСПЕТЧЕРА
        /// <summary>
        /// Перенумерация вагонов по указаному пути с указаной начальной позиции
        /// </summary>
        /// <param name="context"></param>
        /// <param name="id_way"></param>
        /// <param name="position_start"></param>
        /// <returns></returns>
        public int RenumberingWagons(ref EFDbContext context, int id_way, int position_start)
        {
            try
            {
                int count = 0;
                List<WagonInternalMovement> list_wim = context.WagonInternalMovements.Where(m => m.IdWay == id_way & m.IdOuterWay == null & m.WayEnd == null).OrderBy(p => p.Position).ToList();
                if (list_wim != null)
                {
                    count = list_wim.Count();
                    foreach (WagonInternalMovement wim in list_wim)
                    {
                        wim.Position = position_start++;
                    }
                }
                return count;
            }
            catch (Exception e)
            {
                _logger.LogError(e, String.Format("RenumberingWagons(context={0}, id_way={1}, position_start={2})", context, id_way, position_start));
                return (int)errors_base.global;// Возвращаем id=-1 , Ошибка
            }
        }

        /// <summary>
        /// Вернуть последнюю позицию вагона
        /// </summary>
        /// <param name="wir"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public WagonInternalMovement? GetLastMovement(WagonInternalRoute wir, EFDbContext context)
        {
            if (wir.WagonInternalMovements == null) return null;
            WagonInternalMovement? wim = context
                .WagonInternalMovements
                .Where(m => m.IdWagonInternalRoutes == wir.Id)
                .AsNoTracking()
                .OrderByDescending(c => c.Id)
                .FirstOrDefault();
            return wim;
        }

        #region  Операция "Принять вагон на АМКР"

        /// <summary>
        /// Принять прибывающий вагон состава с внешнего пути
        /// </summary>
        /// <param name="context"></param>
        /// <param name="id_outer_way"></param>
        /// <param name="id_way_on"></param>
        /// <param name="position_on"></param>
        /// <param name="lead_time"></param>
        /// <param name="wagon"></param>
        /// <param name="locomotive1"></param>
        /// <param name="locomotive2"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public int ArrivalWagon(ref EFDbContext context, int id_outer_way, int id_way_on, int position_on, DateTime lead_time, WagonInternalRoute wagon, string locomotive1, string locomotive2, string user)
        {
            try
            {
                if (wagon == null) return (int)errors_base.not_wir_db; // В базе данных нет записи по WagonInternalRoutes (Внутреннее перемещение вагонов)
                                                                       // Определим станцию и путь приема
                DirectoryWay way = context.DirectoryWays.Where(w => w.Id == id_way_on).FirstOrDefault();
                if (way == null) return (int)errors_base.not_dir_way_of_db;         // В базе данных нет записи указанной строки пути
                if (way.WayDelete != null) return (int)errors_base.way_is_delete;  // Путь удален
                if (way.WayClose != null) return (int)errors_base.way_is_close;    // Путь закрыт
                int id_station_on = way.IdStation;
                // Получим текущее положение вагона
                WagonInternalMovement wim = wagon.GetLastMovement(ref context);
                if (wim == null) return (int)errors_base.not_open_wir;                  // В базе данных нет открытой записи по WagonInternalRoutes (Внутреннее перемещение вагонов)
                if (wim.IdOuterWay != id_outer_way) return (int)errors_base.wagon_not_outerway; // вагон  не стоит на указаном перегоне
                                                                                                // Проверим вагон уже стоит ?
                if (wim.IdWay == id_way_on && wim.Position == position_on) return 0; // Вагон уже принят пропустить операцию
                                                                                     // Вагон не принят, принять.
                string note_sostav = "Состав:" + wim.NumSostav + "- принят";

                // Установим и закроем операцию принять -6              
                WagonInternalOperation new_operation = wagon.SetOpenOperation(ref context, 6, lead_time.AddMinutes(-10), null, null, locomotive1, locomotive2, note_sostav, user).SetCloseOperation(lead_time, null, user);
                if (new_operation == null) return (int)errors_base.err_create_wio_db;   // Ошибка создания новой операции над вагоном.

                // Установим и вагон на путь станции
                WagonInternalMovement new_movement = wagon.SetStationWagon(ref context, id_station_on, id_way_on, lead_time, position_on, null, user, true);

                if (new_movement == null) return (int)errors_base.err_create_wim_db;   // Ошибка создания новой позиции вагона.
                                                                                       // Зададим сылку на операцию
                new_movement.IdWioNavigation = new_operation;
                //context.Update(wagon); // Обновим контекст
                return 1;
            }
            catch (Exception e)
            {
                _logger.LogError(e, String.Format("ArrivalWagon(context ={ 0}, id_outer_ways ={ 1}, id_way_on ={ 2}, position_on ={ 3}, lead_time ={ 4}, wagon ={ 5}, locomotive1 ={ 6}, locomotive2 ={ 7}, user ={ 8})",
                    context, id_outer_way, id_way_on, position_on, lead_time, wagon, locomotive1, locomotive2, user));
                return (int)errors_base.global;// Возвращаем id=-1 , Ошибка
            }
        }

        /// <summary>
        /// Принять прибывающий состав с внешнего пути
        /// </summary>
        /// <param name="id_outer_way"></param>
        /// <param name="wagons"></param>
        /// <param name="id_way_on"></param>
        /// <param name="head"></param>
        /// <param name="lead_time"></param>
        /// <param name="locomotive1"></param>
        /// <param name="locomotive2"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public ResultTransfer ArrivalWagonsOfStationAMKR(int id_outer_way, List<ListOperationWagon> wagons, int id_way_on, bool head, DateTime lead_time, string locomotive1, string locomotive2, string user)
        {
            DateTime start = DateTime.Now;
            ResultTransfer rt = new ResultTransfer(wagons.Count());
            try
            {
                EFDbContext context = new EFDbContext();
                //Проверим и скорректируем пользователя
                if (String.IsNullOrWhiteSpace(user))
                {
                    user = System.Environment.UserDomainName + @"\" + System.Environment.UserName;
                }
                List<WagonInternalRoutesPosition> List_wir = new List<WagonInternalRoutesPosition>();
                // Пройдемся по вагонам отсортировав их по позиции
                foreach (ListOperationWagon sw in wagons.OrderBy(w => w.position).ToList())
                {
                    List_wir.Add(new WagonInternalRoutesPosition()
                    {
                        wir = context.WagonInternalRoutes
                        .Where(r => r.Id == sw.wir_id)
                        //.AsNoTracking()
                        //.Include(x => x.WagonInternalMovements)
                        //.Include(x => x.WagonInternalOperations)
                        .FirstOrDefault(),
                        new_position = sw.position
                    });
                }
                if (List_wir != null && List_wir.Count() > 0)
                {
                    // Выполним сортировку позиций по возрастанию
                    List<WagonInternalRoute> wagon_position = List_wir.OrderBy(w => w.new_position).Select(w => w.wir).ToList();

                    //Подготовим путь приема(перестроим позиции)
                    int res_renum = RenumberingWagons(ref context, id_way_on, (head == true ? (wagons.Count() + 1) : 1));
                    // Определим позицию переноса вагонов
                    int position = head == true ? 1 : context.GetNextPosition(id_way_on);

                    foreach (WagonInternalRoute wagon in wagon_position)
                    {
                        int result = ArrivalWagon(ref context, id_outer_way, id_way_on, position, lead_time, wagon, locomotive1, locomotive2, user);
                        rt.SetMovedResult(result, wagon.Num);
                        position++;
                    }
                }
                // 
                if (rt.error == 0)
                {
                    rt.SetResult(context.SaveChanges());
                    // Если операция успешна, перенумеруем позиции на пути с которого ушли вагоны
                    if (rt.result > 0)
                    {
                        string mess = String.Format("Операция принять состав на станцию АМКР. Код выполнения = {0}. внешний путь = {1}, путь приема = {2}, голова = {3}, время выполнения операции = {4}, локомотив-1 = {5}, локомотив-2 = {6}. Результат переноса [выбрано для переноса = {7}, перенесено = {8}, пропущено = {9}, ошибок переноса = {10}].",
                            rt.result, id_outer_way, id_way_on, head, lead_time, locomotive1, locomotive2, rt.count, rt.moved, rt.skip, rt.error);
                        _logger.LogWarning(mess);
                        DateTime stop = DateTime.Now;
                        _logger.LogDebug(String.Format("Операция принять состав на станцию АМКР."), start, stop, rt.result);
                    }
                }
                else
                {
                    rt.SetResult((int)errors_base.cancel_save_changes);
                }
                return rt;
            }
            catch (Exception e)
            {
                _logger.LogError(e, String.Format("ArrivalWagonsOfStation(id_outer_way={0}, wagons={1}, id_way_on={2}, head={3}, lead_time={4}, locomotive1={5}, locomotive2={6}, user={7})", id_outer_way, wagons, id_way_on, head, lead_time, locomotive1, locomotive2, user));
                rt.SetResult((int)errors_base.global);
                return rt;// Возвращаем id=-1 , Ошибка
            }
        }
        #endregion

        #region  Операция "Отправить вагоны на АМКР"
        /// <summary>
        /// Выполнить операцию отправить вагон в составе на станцию АМКР
        /// </summary>
        /// <param name="context"></param>
        /// <param name="id_way_from"></param>
        /// <param name="id_outer_ways"></param>
        /// <param name="position_on"></param>
        /// <param name="lead_time"></param>
        /// <param name="wagon"></param>
        /// <param name="num_sostav"></param>
        /// <param name="locomotive1"></param>
        /// <param name="locomotive2"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public int SendWagon(ref EFDbContext context, int id_way_from, int id_outer_ways, int position_on, DateTime lead_time, WagonInternalRoute wagon, string num_sostav, string locomotive1, string locomotive2, string user)
        {
            try
            {
                //Проверим и скорректируем пользователя
                if (String.IsNullOrWhiteSpace(user))
                {
                    user = System.Environment.UserDomainName + @"\" + System.Environment.UserName;
                }
                if (wagon == null) return (int)errors_base.not_wir_db; // В базе данных нет записи по WagonInternalRoutes (Внутреннее перемещение вагонов)
                                                                       // Получим текущее положение вагона
                WagonInternalMovement? wim = context.WagonInternalMovements
                    .Where(m => m.IdWagonInternalRoutes == wagon.Id)
                    .AsNoTracking()
                    .OrderByDescending(c => c.Id)
                    .FirstOrDefault();
                if (wim == null) return (int)errors_base.not_open_wir;                          // В базе данных нет открытой записи по WagonInternalRoutes (Внутреннее перемещение вагонов)
                if (wim.IdWay != id_way_from) return (int)errors_base.wagon_not_way;            // Вагон не стоит на пути
                                                                                                // Проверим вагон уже стоит ?
                if (wim.IdOuterWay == id_outer_ways && wim.Position == position_on) return 0;   // Вагон отправлен пропустить операцию
                                                                                                // Вагон не стоит, переставим.

                // Установим и закроем операцию отправления -5              
                WagonInternalOperation new_operation = wagon.SetOpenOperation(ref context, 5, lead_time.AddMinutes(-10), null, null, locomotive1, locomotive2, "Состав:" + num_sostav, user);
                if (new_operation == null) return (int)errors_base.err_create_wio_db;   // Ошибка создания новой операции над вагоном.
                long? id = new_operation.CloseOperation(lead_time, null, user);

                // Установим и вагон на внешний путь
                WagonInternalMovement? new_movement = wagon.SetSendingWagon(ref context, id_outer_ways, lead_time, position_on, num_sostav, null, user);
                if (new_movement == null) return (int)errors_base.err_create_wim_db;   // Ошибка создания новой позиции вагона.
                                                                                       // Зададим сылку на операцию
                new_movement.IdWioNavigation = new_operation;
                return 1;
            }
            catch (Exception e)
            {
                _logger.LogError(e, String.Format("SendWagon(context={0}, id_way_from={1}, id_outer_ways={2}, position_on={3}, num_sostav={4}, lead_time={5}, wagon={6}, locomotive1={7}, locomotive2={8}, user={9})",
                    context, id_way_from, id_outer_ways, position_on, num_sostav, lead_time, wagon, locomotive1, locomotive2, user));
                return (int)errors_base.global; // Возвращаем id=-1 , Ошибка
            }
        }
        /// <summary>
        /// Отправить сформированный состав на станцию АМКР
        /// </summary>
        /// <param name="id_way_from"></param>
        /// <param name="wagons"></param>
        /// <param name="id_outer_way"></param>
        /// <param name="lead_time"></param>
        /// <param name="locomotive1"></param>
        /// <param name="locomotive2"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public ResultTransfer OutgoingWagonsOfStationAMKR(int id_way_from, List<ListOperationWagon> wagons, int id_outer_way, DateTime lead_time, string locomotive1, string? locomotive2, string user)
        {
            DateTime start = DateTime.Now;
            ResultTransfer rt = new ResultTransfer(wagons.Count());
            try
            {
                //Проверим и скорректируем пользователя
                if (String.IsNullOrWhiteSpace(user))
                {
                    user = System.Environment.UserDomainName + @"\" + System.Environment.UserName;
                }
                string num_sostav = id_outer_way.ToString() + '-' + lead_time.ToString("ddMMyyyyHHmmss");

                List<WagonInternalRoutesPosition> List_wir = new List<WagonInternalRoutesPosition>();
                EFDbContext context = new EFDbContext(this.options);
                //using (EFDbContext context = new EFDbContext(this.options))
                {
                    // Пройдемся по вагонам отсортировав их по позиции
                    foreach (ListOperationWagon sw in wagons.OrderBy(w => w.position).ToList())
                    {
                        List_wir.Add(new WagonInternalRoutesPosition()
                        {
                            wir = context.WagonInternalRoutes.Where(r => r.Id == sw.wir_id).FirstOrDefault(),
                            new_position = sw.position
                        });
                    }
                    if (List_wir != null && List_wir.Count() > 0)
                    {
                        // Выполним сортировку позиций по возрастанию
                        List<WagonInternalRoute> wagon_position = List_wir.OrderBy(w => w.new_position).Select(w => w.wir).ToList();
                        int position = 1;
                        foreach (WagonInternalRoute wagon in wagon_position)
                        {
                            int result = SendWagon(ref context, id_way_from, id_outer_way, position, lead_time, wagon, num_sostav, locomotive1, locomotive2, user);
                            rt.SetMovedResult(result, wagon.Num);
                            position++;
                        }
                    }
                    // 
                    if (rt.error == 0)
                    {
                        rt.SetResult(context.SaveChanges());
                        // Если операция успешна, перенумеруем позиции на пути с которого ушли вагоны
                        if (rt.result > 0)
                        {
                            string mess = String.Format("Операция отправки вагонов на станцию АМКР. Код выполнения = {0}. Путь отправки = {1}, внешний путь приема = {2}, номер состава = {3}, время выполнения операции = {4}, локомотив-1 = {5}, локомотив-2 = {6}. Результат переноса [выбрано для переноса = {7}, перенесено = {8}, пропущено = {9}, ошибок переноса = {10}].",
                                rt.result, id_way_from, id_outer_way, num_sostav, lead_time, locomotive1, locomotive2, rt.count, rt.moved, rt.skip, rt.error);
                            _logger.LogWarning(mess);
                            DateTime stop = DateTime.Now;
                            _logger.LogDebug(String.Format("Операция отправки вагонов на станцию АМКР."), start, stop, rt.result);
                            int result_rnw = RenumberingWagons(ref context, id_way_from, 1);
                            if (result_rnw > 0)
                            {
                                // Применим перенумерацию
                                int res_renum = context.SaveChanges();
                            }
                        }
                    }
                    else
                    {
                        rt.SetResult((int)errors_base.cancel_save_changes);
                    }
                }
                return rt;
            }
            catch (Exception e)
            {
                _logger.LogError(e, String.Format("OutgoingWagonsOfStationAMKR(id_way_from={0}, wagons={1}, id_outer_way={2}, lead_time={3}, locomotive1={4}, locomotive2={5}, user={6})"
                    , id_way_from, wagons, id_outer_way, lead_time, locomotive1, locomotive2, user));
                rt.SetResult((int)errors_base.global);
                return rt;// Возвращаем id=-1 , Ошибка
            }
        }
        #endregion

        #endregion

        #region РАСЧЕТ ПЛАТЫ ЗА ПОЛЬЗОВАНИЕ - АРМ ДИСПЕТЧЕРА
        /// <summary>
        /// Класс данных периоды ставок по вагону
        /// </summary>
        public class Wagon_Usage_Fee_Period
        {
            public DateTime? date_adoption { get; set; }
            public DateTime? date_outgoing { get; set; }
            public int id_operator { get; set; }
            public int id_genus { get; set; }
            public DateTime start { get; set; }
            public DateTime stop { get; set; }
            public int? id_currency { get; set; }
            public decimal? rate { get; set; }
            public int? id_currency_derailment { get; set; }
            public decimal? rate_derailment { get; set; }
            public float? coefficient_route { get; set; }
            public float? coefficient_not_route { get; set; }
            public int? grace_time_1 { get; set; }
            public int? grace_time_2 { get; set; }
            public bool? hour_after_30 { get; set; }
        }
        public class SettlementRate
        {
            public int id_currency { get; set; } = -1;               // Выбранная валюта
            public decimal rate { get; set; } = 0;                   // Выбранная ставка
            public decimal rate_currency { get; set; } = 0;          // ставка для расчета (с учетом схода)
            public decimal exchange_rate { get; set; } = 1;
        }
        /// <summary>
        /// Получить ставку, курс и валюту (с учетом схода)
        /// </summary>
        /// <param name="context"></param>
        /// <param name="derailment"></param>
        /// <param name="wufp"></param>
        /// <param name="list_bank"></param>
        /// <returns></returns>
        public SettlementRate GetExchangeRate(EFDbContext context, bool derailment, Wagon_Usage_Fee_Period wufp, List<DirectoryBankRate> list_bank)
        {
            SettlementRate srate = new SettlementRate();
            // Ставка, курс и валюта (с учетом схода)
            // Проверим на сход
            if (!derailment)
            {
                if (wufp.id_currency != null)
                {
                    srate.id_currency = (int)wufp.id_currency;
                    srate.rate = wufp.rate != null ? (decimal)wufp.rate : 0;
                    srate.rate_currency = wufp.rate != null ? (decimal)wufp.rate : 0;
                    if (wufp.id_currency > 1)
                    {
                        // отправимся за курсом
                        DirectoryCurrency? curr = context.DirectoryCurrencies
                            .AsNoTracking().Where(c => c.Id == wufp.id_currency).FirstOrDefault();
                        if (curr != null)
                        {
                            DirectoryBankRate? res_er = list_bank.Where(e => e.Code == curr.Code).FirstOrDefault();
                            srate.exchange_rate = res_er != null ? (decimal)res_er.Rate : 0;
                        }
                    }
                }
            }
            else
            {
                // сход
                if (wufp.id_currency_derailment != null)
                {
                    srate.id_currency = (int)wufp.id_currency_derailment;
                    srate.rate = wufp.rate_derailment != null ? (decimal)wufp.rate_derailment : 0;
                    srate.rate_currency = wufp.rate_derailment != null ? (decimal)wufp.rate_derailment : 0;
                    if (wufp.id_currency_derailment > 1)
                    {
                        // отправимся за курсом
                        DirectoryCurrency? curr = context.DirectoryCurrencies
                                                    .AsNoTracking()
                                                    .Where(c => c.Id == wufp.id_currency_derailment)
                                                    .FirstOrDefault();

                        if (curr != null)
                        {
                            DirectoryBankRate res_er = list_bank.Where(e => e.Code == curr.Code).FirstOrDefault();
                            srate.exchange_rate = res_er != null ? (decimal)res_er.Rate : 0;
                        }

                    }
                }
            }
            return srate;
        }
        /// <summary>
        /// Получить расчет платы за пользование по вагону (через id_wir)
        /// </summary>
        /// <param name="id_wir"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public CalcWagonUsageFee CalcUsageFeeOfWIR(long id_wir)
        {
            //if (id_wir == 821933) { 

            //}
            CalcWagonUsageFee cwuf = new CalcWagonUsageFee()
            {
                IdWir = id_wir,
                Num = null,
                IdOperator = 0,
                IdGenus = 0,
                UzWagon = false,
                DateAdoption = null,
                DateOutgoing = null,
                Route = false,
                InpCargo = false,
                OutCargo = false,
                IdCargoArr = null,
                IdCargoOut = null,
                CodeStnFrom = null,
                CodeStnTo = null,
                Derailment = false,
                CountStage = 0,
                IdCurrency = 0,
                Rate = 0,
                ExchangeRate = 0,
                Coefficient = 0,
                UseTime = 0,
                GraceTime = 0,
                CalcTime = 0,
                CalcFeeAmount = 0,
                Downtime = 0,
                error = 0,
            };
            try
            {
                using (EFDbContext context = new EFDbContext(this.options))
                {
                    WagonInternalRoute? wir = context.WagonInternalRoutes
                        .AsNoTracking()
                        .Where(r => r.Id == id_wir).FirstOrDefault();
                    if (wir == null) { cwuf.error = (int)errors_base.not_wir_db; return cwuf; }
                    cwuf.Derailment = context.isDerailmentOperation(id_wir); // проверим признак схода
                                                                             // Получить id_wir прибытия с учетом (если был возврат)
                    long id_wir_arrival = context.GetIDWIR(wir.Id);
                    // Прибытие
                    WagonInternalRoute? wir_arrival = context.WagonInternalRoutes
                        .AsNoTracking()
                        .Where(w => w.Id == id_wir_arrival)
                        .FirstOrDefault();
                    if (wir_arrival == null) { cwuf.error = (int)errors_base.not_wir_db; return cwuf; }
                    // Подгружаем состав, аренду и груз
                    ArrivalCar? arr_car = context.ArrivalCars
                        .AsNoTracking()
                        .Where(c => c.Id == wir_arrival.IdArrivalCar)
                        .Include(car => car.IdArrivalNavigation)
                        .Include(car => car.IdArrivalUzVagonNavigation)
                            .ThenInclude(uzvag => uzvag.IdWagonsRentArrivalNavigation)
                        .Include(car => car.IdArrivalUzVagonNavigation)
                            .ThenInclude(uzvag => uzvag.IdCargoNavigation)
                        .Include(car => car.IdArrivalUzVagonNavigation)
                            .ThenInclude(sost => sost.IdDocumentNavigation)
                        .FirstOrDefault();
                    if (arr_car == null) { cwuf.error = (int)errors_base.not_arrival_cars_db; return cwuf; }
                    ArrivalSostav? arr_sostav = arr_car.IdArrivalNavigation;
                    if (arr_sostav == null) { cwuf.error = (int)errors_base.not_arrival_sostav_db; return cwuf; }
                    ArrivalUzVagon? arr_uz_vag = arr_car.IdArrivalUzVagonNavigation;
                    if (arr_uz_vag == null) { cwuf.error = (int)errors_base.not_inp_uz_vag_db; return cwuf; }
                    ArrivalUzDocument? arr_uz_sost = arr_car.IdArrivalUzVagonNavigation.IdDocumentNavigation;
                    if (arr_uz_sost == null) { cwuf.error = (int)errors_base.not_inp_uz_doc_db; return cwuf; }
                    // номер вагона
                    cwuf.Num = arr_car.Num;
                    cwuf.IdGenus = (int)arr_uz_vag.IdGenus;
                    // уставки времени
                    cwuf.DateAdoption = arr_sostav.DateAdoptionAct != null ? (DateTime)arr_sostav.DateAdoptionAct : (arr_car.DateAdoptionAct != null ? (DateTime)arr_car.DateAdoptionAct : (DateTime)arr_sostav.DateAdoption);    // Зашел
                    cwuf.DateOutgoing = DateTime.Now;
                    // признак гружен по прибытию
                    cwuf.InpCargo = (arr_uz_vag.IdCargoNavigation != null ? list_groups_cargo.Find(x => x == arr_uz_vag.IdCargoNavigation.IdGroup) == 0 : false);
                    cwuf.IdCargoArr = (arr_uz_vag.IdCargoNavigation != null ? arr_uz_vag.IdCargoNavigation.Id : null);
                    cwuf.CodeStnFrom = (arr_uz_sost != null ? arr_uz_sost.CodeStnFrom : null);
                    //cwuf.OutCargo = false;
                    // оператор
                    cwuf.IdOperator = arr_uz_vag.IdWagonsRentArrivalNavigation != null ? arr_uz_vag.IdWagonsRentArrivalNavigation.IdOperator : null;
                    // маршрут (по отправке)
                    //cwuf.Route = null;
                    // Отправка
                    OutgoingCar? out_car = context.OutgoingCars
                        .AsNoTracking()
                        .Where(c => c.Id == wir.IdOutgoingCar)
                        .Include(car => car.IdOutgoingNavigation) // состав
                        .Include(car => car.IdOutgoingUzVagonNavigation) // документ по вагону
                            .ThenInclude(uzvag => uzvag.IdWagonsRentOutgoingNavigation) // аренда по документу на вагону
                        .Include(car => car.IdOutgoingUzVagonNavigation) // документ по вагону
                            .ThenInclude(uzvag => uzvag.IdCargoNavigation) // груз по документу на вагону
                        .Include(car => car.IdOutgoingUzVagonNavigation) // документ по вагону
                            .ThenInclude(doc_uz => doc_uz.IdDocumentNavigation) // документ
                        .FirstOrDefault();
                    // Проверим отправка есть тогда расчет платы окончательный, если нет считаем по текущему времени
                    if (out_car != null)
                    {
                        OutgoingUzVagon? out_uz_vag = out_car.IdOutgoingUzVagonNavigation;
                        if (out_uz_vag != null)
                        {
                            // Аренда по отправке
                            DirectoryWagonsRent? out_wag_rent = out_uz_vag.IdWagonsRentOutgoingNavigation;
                            if (out_wag_rent != null)
                            {
                                cwuf.IdOperator = out_wag_rent.IdOperator;
                            }
                            // Груз по отправке
                            DirectoryCargo? outg_cargo = out_uz_vag.IdCargoNavigation;
                            OutgoingUzDocument out_doc_uz = out_uz_vag.IdDocumentNavigation;
                            // признак гружен по отправке 
                            cwuf.OutCargo = (outg_cargo != null ? list_groups_cargo.Find(x => x == outg_cargo.IdGroup) == 0 : false);
                            cwuf.IdCargoOut = (outg_cargo != null ? outg_cargo.Id : null);
                            cwuf.CodeStnTo = (out_uz_vag.IdDocumentNavigation != null ? out_uz_vag.IdDocumentNavigation.CodeStnTo : null);
                        }
                        OutgoingSostav? out_sostav = out_car.IdOutgoingNavigation;
                        if (out_sostav != null)
                        {
                            cwuf.DateOutgoing = out_sostav.DateOutgoingAct != null ? (DateTime)out_sostav.DateOutgoingAct : (out_car.DateOutgoingAct != null ? (DateTime)out_car.DateOutgoingAct : (out_sostav.DateOutgoing != null ? (DateTime)out_sostav.DateOutgoing : DateTime.Now));//   Вышел (согласно всех актов) или еще нет тогда текущее время
                            cwuf.Route = out_sostav.RouteSign;
                        }
                    }
                    // Получим оператора
                    if (cwuf.IdOperator != null)
                    {
                        DirectoryOperatorsWagon? oper_wag = context.DirectoryOperatorsWagons.AsNoTracking().Where(w => w.Id == cwuf.IdOperator).FirstOrDefault();
                        // Уточним оператора так как есть основной и дополнительные (ЦТЛ и доп ЦТЛ)
                        if (oper_wag != null)
                        {
                            cwuf.IdOperator = oper_wag.ParentId != null ? oper_wag.ParentId : oper_wag.Id;
                        }
                    }
                    // Взведем в исходное состояние (исключим ошибку если нет условий расчета)


                    TimeSpan tm = (DateTime)cwuf.DateOutgoing - (DateTime)cwuf.DateAdoption; // за первый период
                    cwuf.Downtime = (int)tm.TotalMinutes;
                    // Получим периоды для расчетов
                    List<UsageFeePeriod> list_uf_period_outgoing = context.UsageFeePeriods
                        .AsNoTracking()
                        .Where(p => p.IdOperator == cwuf.IdOperator && p.IdGenus == cwuf.IdGenus)
                        .Include(detali => detali.UsageFeePeriodDetalis) // документ по вагону
                        .OrderByDescending(c => c.Id)
                        .ToList();
                    // Если периодов нет выйдем с расчета
                    if (list_uf_period_outgoing == null || list_uf_period_outgoing.Count() == 0) return cwuf;
                    UsageFeePeriod? arr_perriod = list_uf_period_outgoing.Where(o => o.Start <= cwuf.DateAdoption && o.Stop >= cwuf.DateAdoption).FirstOrDefault();
                    UsageFeePeriod? out_perriod = list_uf_period_outgoing.Where(o => o.Start <= cwuf.DateOutgoing && o.Stop >= cwuf.DateOutgoing).FirstOrDefault();
                    // Если периодов нет выйдем с расчета
                    if ((arr_perriod == null && out_perriod == null)) { cwuf.error = (int)errors_base.not_dt_calc_usage_fee; return cwuf; }
                    // Периоды определены. Сформируем список
                    List<UsageFeePeriod> list_period_where = new List<UsageFeePeriod>();
                    list_period_where.Clear();
                    if (arr_perriod != null)
                    {
                        list_period_where.Add(arr_perriod);
                        if (out_perriod != null && arr_perriod.Id != out_perriod.Id)
                        {
                            list_period_where.Add(out_perriod);
                        }
                    }
                    // Детальный период
                    //UsageFeePeriod ufp_last = list_period_where.Last();
                    //UsageFeePeriodDetali ufpd_last = 
                    // Расчеты
                    DateTime date = DateTime.Now.Date;
                    List<DirectoryBankRate> list_bank = context.DirectoryBankRates.Where(b => b.Date == date).ToList();
                    if (list_bank == null || list_bank.Count() < 2) { cwuf.error = (int)errors_base.not_list_exchange_rate; return cwuf; }
                    List<Wagon_Usage_Fee_Period> list_period_setup = new List<Wagon_Usage_Fee_Period>();

                    foreach (UsageFeePeriod ufp in list_period_where)
                    {
                        cwuf.UzWagon = (ufp.HourAfter30 != null && ufp.HourAfter30 == true ? true : false); // Определим вагоны ЦТЛ
                        list_period_setup.Add(new Wagon_Usage_Fee_Period()
                        {
                            date_adoption = cwuf.DateAdoption >= ufp.Start && cwuf.DateAdoption <= ufp.Stop ? (DateTime?)cwuf.DateAdoption : null,
                            date_outgoing = cwuf.DateOutgoing >= ufp.Start && cwuf.DateOutgoing <= ufp.Stop ? (DateTime?)cwuf.DateOutgoing : null,
                            id_operator = (int)ufp.IdOperator,
                            id_genus = ufp.IdGenus,
                            start = ufp.Start,
                            stop = ufp.Stop,
                            id_currency = ufp.IdCurrency,
                            rate = ufp.Rate,
                            id_currency_derailment = ufp.IdCurrencyDerailment,
                            rate_derailment = ufp.RateDerailment,
                            coefficient_route = ufp.CoefficientRoute,
                            coefficient_not_route = ufp.CoefficientNotRoute,
                            grace_time_1 = ufp.GraceTime1,
                            grace_time_2 = ufp.GraceTime2,
                            hour_after_30 = ufp.HourAfter30
                        });
                    } // {end foreach (Usage_Fee_Period ufp in list_period_where)}
                      //============================================================================
                      //  РАСЧЕТ
                      //============================================================================
                    int hour_period = 0;                                // Время (часов)
                    int remaining_minutes_period = 0;                   // Остаток минут
                    int sum_remaining_minutes_period = 0;               // Остаток минут (суммируемый)
                    int grace_time = 0;                                 // Льготное время
                    int grace_detali_time = 0;                          // Льготное время (детальное условие)
                    SettlementRate curr_rate = new SettlementRate();    // Ставка, курс и валюта (с учетом схода)
                    float coefficient_route = 1;                        // коэффициент маршрута, для всех по умолчанию 1;
                                                                        // -----
                    int calc_hour_period = 0;                           // Время расчетное (часов)
                    int calc_time = 0;                                  // Расчетное время
                    decimal calc_fee_amount = 0;                        // Расчетная сумма
                                                                        //int cammon_minut_period = 0;                        // Общее время простоя
                    TimeSpan tm_period;
                    if (list_period_setup.Count() > 0)
                    {
                        UsageFeePeriod ufp_last = list_period_where.Last();
                        // Проверим детальные настройки

                        List<UsageFeePeriodDetali> ufpds = ufp_last.UsageFeePeriodDetalis.ToList();
                        if (ufpds != null && ufpds.Count() > 0)
                        {
                            // есть детальные настройки, пройдемся
                            foreach (UsageFeePeriodDetali det in ufpds)
                            {
                                if (det.CodeStnFrom != null)
                                {
                                    grace_detali_time = det.CodeStnFrom == cwuf.CodeStnFrom ? (int)det.GraceTime : 0;
                                }
                                if (det.IdCargoArrival != null)
                                {
                                    grace_detali_time = det.IdCargoArrival == cwuf.IdCargoArr ? (int)det.GraceTime : 0;
                                }
                                if (det.CodeStnTo != null)
                                {
                                    grace_detali_time = det.CodeStnTo == cwuf.CodeStnTo ? (int)det.GraceTime : 0;
                                }
                                if (det.IdCargoOutgoing != null)
                                {
                                    grace_detali_time = det.IdCargoOutgoing == cwuf.IdCargoOut ? (int)det.GraceTime : 0;
                                }
                            }
                        }
                        // Опредеим расчет ЦТЛ или собственники
                        if (!(bool)cwuf.UzWagon)
                        {
                            // Вагоны собственники (Считаем без периодов берем по последнему)
                            DateTime? dt_start = list_period_setup.First().date_adoption;
                            DateTime? dt_end = list_period_setup.Last().date_outgoing;
                            Wagon_Usage_Fee_Period wufp = list_period_setup.Last();
                            if (dt_start == null || dt_end == null)
                            {
                                cwuf.error = (int)errors_base.not_dt_calc_usage_fee; return cwuf;
                            };
                            // просчитаем временной интервал
                            tm_period = (DateTime)dt_end - (DateTime)dt_start; // первый и последний период
                            hour_period = (int)Math.Truncate(tm_period.TotalHours);
                            remaining_minutes_period = (int)Math.Truncate(tm_period.TotalMinutes - (hour_period * 60));
                            // Округление до часа
                            if (remaining_minutes_period > 0)
                            {
                                hour_period++;
                                remaining_minutes_period = 0;
                            }
                            // определим льготный период
                            if ((bool)cwuf.InpCargo && (bool)cwuf.OutCargo)
                            {
                                grace_time = wufp.grace_time_2 != null ? (int)wufp.grace_time_2 : 0;
                            }
                            else
                            {
                                grace_time = wufp.grace_time_1 != null ? (int)wufp.grace_time_1 : 0;
                            }
                            // Получим ставку, курс
                            curr_rate = GetExchangeRate(context, (bool)cwuf.Derailment, wufp, list_bank);
                            // Определим коэффициент маршрута если нет схода
                            if (!(bool)cwuf.Derailment && wufp.coefficient_route != null && cwuf.Route != null && cwuf.Route == true)
                            {
                                coefficient_route = (float)wufp.coefficient_route;
                            }
                            if (!(bool)cwuf.Derailment && wufp.coefficient_not_route != null && cwuf.Route != null && cwuf.Route == false)
                            {
                                coefficient_route = (float)wufp.coefficient_not_route;
                            }
                            calc_hour_period = hour_period;
                            int hour_calc = (hour_period - grace_time - grace_detali_time);
                            if (hour_calc < 0) hour_calc = 0;
                            // пересчет времени с учетом uz_wagon
                            var remainder = (hour_calc % 24);
                            if (remainder > 0)
                            {
                                remainder = 24 - remainder;
                            }
                            // текущая плата почасово
                            long rc_res = (long)((curr_rate.rate_currency / 24) * 10000000); // Убрал обрезание до 3 знака
                            decimal rate_currency_hour = (decimal)(rc_res / 10000000.0);
                            //decimal rate_currency_hour = curr_rate.rate_currency / 24;

                            calc_time = hour_calc + (remainder); // округлим до целых суток
                            calc_fee_amount = (rate_currency_hour * calc_time * (decimal)coefficient_route) * curr_rate.exchange_rate;
                        }
                        else
                        {
                            int stage = 0;                      // этапы расчета (0-одинарный, 1-первый, 2-промежуточный, 3-последний)
                            bool rounding = false;              // Признак округление в первом периоде расчета уже было
                                                                // Пройдемся по активным периодам
                            foreach (Wagon_Usage_Fee_Period wufp in list_period_setup)
                            {
                                grace_time = 0;                 // сбросим льготный период
                                                                //grace_detali_time = 0;          // сбросим льготный период
                                curr_rate.exchange_rate = 1;    // сбросим курс
                                curr_rate.rate_currency = 0;    // сбросим ставку
                                coefficient_route = 1;          // сбросим коэффициент

                                if (wufp.date_adoption != null)
                                {
                                    // Первый период
                                    if (wufp.date_outgoing != null)
                                    {
                                        tm_period = (DateTime)wufp.date_outgoing - (DateTime)wufp.date_adoption; // первый и последний период
                                        stage = 0;
                                    }
                                    else
                                    {
                                        tm_period = (DateTime)wufp.stop.AddSeconds(1) - (DateTime)wufp.date_adoption; // за первый период
                                        stage = 1;
                                    }
                                    // определим льготный период
                                    if ((bool)cwuf.InpCargo && (bool)cwuf.OutCargo)
                                    {
                                        grace_time = wufp.grace_time_2 != null ? (int)wufp.grace_time_2 : 0;
                                    }
                                    else
                                    {
                                        grace_time = wufp.grace_time_1 != null ? (int)wufp.grace_time_1 : 0;
                                    }
                                }
                                else
                                {
                                    if (wufp.date_outgoing != null)
                                    {
                                        tm_period = ((DateTime)wufp.date_outgoing) - (DateTime)wufp.start; // за последний период
                                        stage = 3;
                                    }
                                    else
                                    {
                                        tm_period = (DateTime)wufp.stop.AddSeconds(1).AddMinutes(1) - (DateTime)wufp.start; // за промежуточный период период
                                        stage = 2;
                                    }
                                    // Льготное время 

                                }
                                // просчитаем интервал
                                hour_period = (int)Math.Truncate(tm_period.TotalHours);
                                remaining_minutes_period = (int)Math.Truncate(tm_period.TotalMinutes - (hour_period * 60));
                                sum_remaining_minutes_period += remaining_minutes_period;
                                // Округление до часа,
                                //rounding = false; //!!! в первом случаее сделал округление всегда, но 3.01.2024 id_outgoing=242270 ваг 52136538 замечание применяется только 1 раз- убрал
                                if (remaining_minutes_period >= 30 && !rounding)
                                {
                                    hour_period++;
                                    rounding = true;
                                }
                                remaining_minutes_period = 0;

                                // Ставка, курс и валюта (с учетом схода)
                                curr_rate = GetExchangeRate(context, (bool)cwuf.Derailment, wufp, list_bank);
                                // Определим коэффициент маршрута если нет схода
                                if (!(bool)cwuf.Derailment && wufp.coefficient_route != null && cwuf.Route != null && cwuf.Route == true)
                                {
                                    coefficient_route = (float)wufp.coefficient_route;
                                }
                                if (!(bool)cwuf.Derailment && wufp.coefficient_not_route != null && cwuf.Route != null && cwuf.Route == false)
                                {
                                    coefficient_route = (float)wufp.coefficient_not_route;
                                }
                                // текущая плата почасово
                                long rc_res = (long)((curr_rate.rate_currency / 24) * 10000000); // Убрал обрезание до 3 знака
                                decimal rate_currency_hour = (decimal)(rc_res / 10000000.0);
                                //decimal rate_currency_hour = curr_rate.rate_currency / 24; 
                                // расчеты
                                switch (stage)
                                {
                                    case 0:
                                        {
                                            // первый и последний период
                                            calc_hour_period = hour_period;
                                            int hour_calc = (hour_period - grace_time - grace_detali_time);
                                            if (hour_calc < 0) hour_calc = 0;
                                            // пересчет времени с учетом uz_wagon
                                            calc_time = hour_calc; // округлим до целых суток
                                            calc_fee_amount = (rate_currency_hour * calc_time * (decimal)coefficient_route) * curr_rate.exchange_rate;
                                            break;
                                        };
                                    case 1:
                                        {
                                            // Первый период
                                            calc_hour_period = hour_period;
                                            int hour_calc = (hour_period - grace_time);
                                            calc_time = hour_calc;
                                            calc_fee_amount = (rate_currency_hour * hour_calc * (decimal)coefficient_route) * curr_rate.exchange_rate;
                                            break;
                                        };
                                    case 2:
                                        {
                                            // промежуточный период
                                            calc_hour_period += hour_period;
                                            int hour_calc = (hour_period);
                                            calc_time += hour_calc;
                                            calc_fee_amount += (rate_currency_hour * hour_calc * (decimal)coefficient_route) * curr_rate.exchange_rate;
                                            break;
                                        }
                                    case 3:
                                        {
                                            // последний период
                                            int hour_calc = (hour_period - grace_detali_time);
                                            // Если небыло округления а сумма остатков >=0 тогда добавим час
                                            if (sum_remaining_minutes_period >= 30 && !rounding)
                                            {
                                                hour_calc++;
                                            }
                                            calc_hour_period += hour_calc;
                                            calc_time += hour_calc;
                                            calc_fee_amount += (rate_currency_hour * hour_calc * (decimal)coefficient_route) * curr_rate.exchange_rate;
                                            break;
                                        }
                                }
                            }
                        }
                    }
                    cwuf.CountStage = list_period_setup.Count();
                    cwuf.IdCurrency = curr_rate.id_currency;
                    cwuf.Rate = curr_rate.rate;
                    cwuf.ExchangeRate = curr_rate.exchange_rate;
                    cwuf.Coefficient = coefficient_route;
                    cwuf.UseTime = calc_hour_period;
                    cwuf.GraceTime = grace_time;
                    cwuf.CalcTime = calc_time;

                    int cfa_res = (int)(calc_fee_amount * 100);
                    decimal calc_fee_amount_res = (decimal)(cfa_res / 100.0);
                    cwuf.CalcFeeAmount = Math.Round(calc_fee_amount_res, 1, MidpointRounding.AwayFromZero);
                    return cwuf;
                }
            }
            catch (Exception e)
            {
                _logger.LogError(_eventId, e, "CalcUsageFeeOfCar(id_wir={0})", id_wir);
                cwuf.error = (int)errors_base.global;
                return cwuf;
            }
        }
        /// <summary>
        /// Получить расчет платы за пользование по вагонам на пути
        /// </summary>
        /// <param name="id_way"></param>
        /// <returns></returns>
        public List<CalcWagonUsageFee> CalcUsageFeeCarsOfWay(int id_way)
        {
            try
            {
                List<CalcWagonUsageFee> results = new List<CalcWagonUsageFee>();
                using (EFDbContext context = new(this.options))
                {
                    List<WagonInternalMovement> list_wim = context.WagonInternalMovements
                        .AsNoTracking()
                        .Where(w => w.IdWay == id_way & w.WayEnd == null)
                        .Include(wir => wir.IdWagonInternalRoutesNavigation) // wir
                            .ThenInclude(wag => wag.NumNavigation) // вагоны
                            .ThenInclude(rent => rent.DirectoryWagonsRents.Where(r => r.RentEnd == null).OrderByDescending(c => c.Id)) // аренда
                            .ThenInclude(oper => oper.IdOperatorNavigation)
                        .ToList();
                    if (list_wim != null)
                    {
                        foreach (WagonInternalMovement wim in list_wim)
                        {
                            DirectoryWagon? dirwag = wim.IdWagonInternalRoutesNavigation.NumNavigation;
                            if (dirwag != null)
                            {
                                DirectoryWagonsRent? rent = dirwag.DirectoryWagonsRents.OrderByDescending(c => c.Id).FirstOrDefault();
                                if (rent != null)
                                {
                                    DirectoryOperatorsWagon? oper = rent.IdOperatorNavigation;
                                    if (oper != null && oper.Paid)
                                    {
                                        results.Add(CalcUsageFeeOfWIR(wim.IdWagonInternalRoutes));
                                    }
                                }
                            }
                        }
                    }
                }
                return results;
            }
            catch (Exception e)
            {
                _logger.LogError(_eventId, e, "CalcUsageFeeCarsOfWay(id_way={0})", id_way);
                return null;
            }
        }
        /// <summary>
        /// Получить расчет платы за пользование по вагонам (через список id_wir)
        /// </summary>
        /// <param name="list_id_wir"></param>
        /// <returns></returns>
        public ResultUpdateIDWagon CalcUsageFeeOfListWir(List<long> list_id_wir)
        {
            ResultUpdateIDWagon result = new ResultUpdateIDWagon(0, list_id_wir.Count());
            try
            {
                if (list_id_wir == null || list_id_wir.Count() == 0)
                {
                    result.result = (int)errors_base.not_input_list_wagons; // Ошибка нет списка вагонов
                    return result;
                }
                string user = System.Environment.UserDomainName + @"\" + System.Environment.UserName;
                using (EFDbContext context = new(this.options))
                {
                    foreach (long id in list_id_wir)
                    {
                        CalcWagonUsageFee res = CalcUsageFeeOfWIR(id);
                        if (res.error < 0)
                        {
                            result.SetErrorResult(id, res.error, res.Num != null ? (int)res.Num : 0);
                        }
                        else if (res.error == 0)
                        {
                            WagonInternalRoute wir = context.WagonInternalRoutes
                                .Where(w => w.Id == id)
                                .Include(wus => wus.IdUsageFeeNavigation) // расчитанная плата
                                .FirstOrDefault();

                            if (wir != null)
                            {
                                WagonUsageFee wuf = context.WagonUsageFees
                                    .Where(w => w.IdWir == res.IdWir && w.Num == res.Num)
                                    .FirstOrDefault();

                                if (wuf != null)
                                {
                                    wir.IdUsageFee = wuf.Id;
                                    // Обновим
                                    wuf.IdOperator = (int)res.IdOperator;
                                    wuf.IdGenus = (int)res.IdGenus;
                                    wuf.DateAdoption = (DateTime)res.DateAdoption;
                                    wuf.DateOutgoing = (DateTime)res.DateOutgoing;
                                    wuf.Route = (bool)res.Route;
                                    wuf.InpCargo = (bool)res.InpCargo;
                                    wuf.OutCargo = (bool)res.OutCargo;
                                    wuf.Derailment = (bool)res.Derailment;// сход
                                    wuf.CountStage = (int)res.CountStage; // Количество стадий расчета
                                    wuf.IdCurrency = (int)res.IdCurrency;
                                    wuf.Rate = (decimal)res.Rate;
                                    wuf.ExchangeRate = (decimal)res.ExchangeRate;
                                    wuf.Coefficient = (double)res.Coefficient;
                                    wuf.UseTime = (int)res.UseTime;
                                    wuf.GraceTime = (int)res.GraceTime;
                                    wuf.CalcTime = (int)res.CalcTime; //calc_time
                                    wuf.CalcFeeAmount = (decimal)res.CalcFeeAmount;
                                    wuf.Downtime = res.Downtime;
                                    wuf.Change = DateTime.Now;
                                    wuf.ChangeUser = user;
                                }
                                else
                                {
                                    // Добавим
                                    wuf = new WagonUsageFee()
                                    {
                                        Id = 0,
                                        IdWir = res.IdWir,
                                        Num = (int)res.Num,
                                        IdOperator = (int)res.IdOperator,
                                        IdGenus = (int)res.IdGenus,
                                        DateAdoption = (DateTime)res.DateAdoption,
                                        DateOutgoing = (DateTime)res.DateOutgoing,
                                        Route = (bool)res.Route,
                                        InpCargo = (bool)res.InpCargo,
                                        OutCargo = (bool)res.OutCargo,
                                        Derailment = (bool)res.Derailment,
                                        CountStage = (int)res.CountStage,
                                        IdCurrency = (int)res.IdCurrency,
                                        Rate = (decimal)res.Rate,
                                        ExchangeRate = (decimal)res.ExchangeRate,
                                        Coefficient = (double)res.Coefficient,
                                        UseTime = (int)res.UseTime,
                                        GraceTime = (int)res.GraceTime,
                                        CalcTime = (int)res.CalcTime,
                                        CalcFeeAmount = (decimal)res.CalcFeeAmount,
                                        Downtime = res.Downtime,
                                        Note = null,
                                        Create = DateTime.Now,
                                        CreateUser = user,
                                    };
                                    wir.IdUsageFeeNavigation = wuf;
                                    //context.WagonUsageFees.Add(wuf);
                                }
                                int res_save = context.SaveChanges();
                                if (res_save < 0)
                                {
                                    result.SetErrorResult(id, res_save, res.Num != null ? (int)res.Num : 0);
                                }
                                else
                                {
                                    if (wuf.Id == 0)
                                    {
                                        result.SetInsertResult(id, res_save, res.Num != null ? (int)res.Num : 0);
                                    }
                                    else
                                    {
                                        result.SetUpdateResult(id, res_save, res.Num != null ? (int)res.Num : 0);
                                    }
                                }
                            }
                        }
                    }
                }
                return result;
            }
            catch (Exception e)
            {
                _logger.LogError(_eventId, e, "CalcUsageFeeOfListWir(list_id_wir={0})", list_id_wir);
                result.result = (int)errors_base.global; // Глобальная ошибка
                return result;
            }
        }
        /// <summary>
        /// Расчет платы за пользование по сданному составу
        /// </summary>
        /// <param name="id_sostav"></param>
        /// <returns></returns>
        public ResultUpdateIDWagon CalcUsageFeeOfOutgoingSostav(long id_sostav)
        {
            ResultUpdateIDWagon result = new ResultUpdateIDWagon(id_sostav, 0);
            try
            {
                using (EFDbContext context = new(this.options))
                {
                    OutgoingSostav out_sostav = context.OutgoingSostavs
                            .AsNoTracking()
                            .Where(s => s.Id == id_sostav)
                            .Include(cars => cars.OutgoingCars.Where(p => p.PositionOutgoing != null)) // cars
                                .ThenInclude(wir => wir.WagonInternalRoutes)
                            .FirstOrDefault();

                    if (out_sostav == null)
                    {
                        result.result = (int)errors_base.not_outgoing_sostav_db;    // В базе данных нет записи состава для оправки
                        return result;
                    }
                    if (out_sostav.DateOutgoing == null)
                    {
                        result.result = (int)errors_base.error_status_outgoing_sostav;     // Ошибка статуса состава (Статус не позволяет сделать эту операцию)
                        return result;
                    }
                    if (out_sostav.OutgoingCars == null || out_sostav.OutgoingCars.Count() == 0)
                    {
                        result.result = (int)errors_base.not_outgoing_cars_db; // В базе данных нет записи по вагонам для отправки
                        return result;
                    }
                    //List<OutgoingCar> cars = out_sostav.OutgoingCars.ToList();
                    List<long> lost_id_wir = new List<long>();
                    foreach (OutgoingCar car in out_sostav.OutgoingCars)
                    {
                        lost_id_wir.Add(car.WagonInternalRoutes.FirstOrDefault().Id);
                    }
                    result = CalcUsageFeeOfListWir(lost_id_wir);
                    result.id = id_sostav;
                }
                return result;

            }
            catch (Exception e)
            {
                _logger.LogError(_eventId, e, "CalcUsageFeeOfOutgoingSostav(id_sostav={0})", id_sostav);
                result.result = (int)errors_base.global; // Глобальная ошибка
                return result;
            }
        }
        /// <summary>
        /// Расчет платы за пользование по сданным составам за выбранный период
        /// </summary>
        /// <param name="start"></param>
        /// <param name="stop"></param>
        /// <returns></returns>
        public List<ResultUpdateIDWagon> CalcUsageFeeOfOutgoingSostav(DateTime start, DateTime stop)
        {
            List<ResultUpdateIDWagon> result = new List<ResultUpdateIDWagon>();
            try
            {

                using (EFDbContext context = new(this.options))
                {
                    List<OutgoingSostav> list_sostav = context.OutgoingSostavs
                        .AsNoTracking()
                        .Where(s => s.DateOutgoing >= start && s.DateOutgoing <= stop).ToList();
                    int count = list_sostav.Count();
                    foreach (OutgoingSostav out_sost in list_sostav)
                    {
                        ResultUpdateIDWagon res_sost = CalcUsageFeeOfOutgoingSostav(out_sost.Id);
                        result.Add(res_sost);
                        Console.WriteLine("Обработал состав id = {0} [ вагонов: {1}/ добавил: {2}/ обновил: {3} / ОШИБОК: {4}], результат = {5}, осталось {6}", out_sost.Id, res_sost.count, res_sost.add, res_sost.update, res_sost.error, res_sost.result, count--);
                    }
                }
                return result;
            }
            catch (Exception e)
            {
                _logger.LogError(_eventId, e, "CalcUsageFeeOfOutgoingSostav(start={0}, stop={1})", start, stop);
                return result;
            }
        }

        #endregion

    }
}
