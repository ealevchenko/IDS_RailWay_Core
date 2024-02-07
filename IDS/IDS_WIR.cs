using EF_IDS.Concrete;
using EF_IDS.Concrete.Directory;
using EF_IDS.Concrete.Outgoing;
using EF_IDS.Concrete.Arrival;
using EF_IDS.Entities;
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

namespace IDS_
{
    public class IDS_WIR : IDS_Base
    {
        private DbContextOptions<EFDbContext> options;
        private readonly ILogger<Object> _logger;
        private readonly IConfiguration _configuration;
        EventId _eventId = new EventId(0);
        private String? connectionString;

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

    }
}
