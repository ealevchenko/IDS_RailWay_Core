using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_IDS.Abstract
{
    public interface IRepository<T> : IDisposable where T : class
    {
        DatabaseFacade Database { get; }
        IQueryable<T> Context { get; }
        IEnumerable<T> Get();       // получение всех объектов
        T Get(int id);              // получение одного объекта по id
        void Add(T item);           // создание объекта
        void Add(IEnumerable<T> items);           // создание объекта
        void Update(T item);        // обновление объекта
        void Update(IEnumerable<T> items);        // обновление объекта
        void AddOrUpdate(T item);   // добавить или обновить
        void Delete(int id);        // удаление объекта по id
        void Delete(IEnumerable<int> list_id);    // удаление объектов
        int Save();                 // сохранение изменений в базе
        T Refresh(T item);          // Обновить объект с базаой
    }
}
