using EF_IDS.Concrete;
using EF_IDS.Entities;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections.Generic;
using System.Collections.Concurrent;

namespace WebAPI.Repositories.GIVC
{
    public class GIVCRepository : IRepository<GivcRequest>
    {
        // кэшируем клиентов в потокобезопасном словаре для повышения производительности
        private static ConcurrentDictionary<int, GivcRequest> customersCache;

        private EFDbContext db;
        public GIVCRepository(EFDbContext db)
        {
            this.db = db;
            // предварительно загружаем клиентов из обычного словаря с ключом
            // CustomerID,
            // а затем конвертируем в потокобезопасный ConcurrentDictionary
            if (customersCache == null)
            {
                customersCache = new ConcurrentDictionary<int, GivcRequest>(db.GivcRequests.ToDictionary(c => c.Id));
            }
        }
        public async Task<GivcRequest> CreateAsync(GivcRequest c)
        {
            // приводим CustomerID к верхнему регистру
            //c.CustomerID = c.CustomerID.ToUpper();
            // добавляем в базу данных с помощью EF Core
            EntityEntry<GivcRequest> added = await db.GivcRequests.AddAsync(c);
            int affected = await db.SaveChangesAsync();
            if (affected == 1)
            {
                // если клиент новый, то добавляем в кэш, иначе вызываем
                // метод UpdateCache
                return customersCache.AddOrUpdate(c.Id, c, UpdateCache);
            }
            else
            {
                return null;
            }
        }
        private GivcRequest UpdateCache(int id, GivcRequest c)
        {
            GivcRequest old;
            if (customersCache.TryGetValue(id, out old))
            {
                if (customersCache.TryUpdate(id, c, old))
                {
                    return c;
                }
            }
            return null;
        }
        public async Task<IEnumerable<GivcRequest>> RetrieveAllAsync()
        {
            // для повышения производительности извлекаем из кэша
            return await Task.Run<IEnumerable<GivcRequest>>(
            () => customersCache.Values);
        }
        public async Task<GivcRequest> RetrieveAsync(int id)
        {
            return await Task.Run(() =>
            {
                GivcRequest c;
                customersCache.TryGetValue(id, out c);
                return c;
            });
        }
        //public async Task<IEnumerable<GivcRequest>> RetrieveAsync_Of_TypeRequests(string TypeRequests)
        //{
        //    return await Task.Run<IEnumerable<GivcRequest>>(
        //        () => customersCache.Values.Where(c=>c.TypeRequests == TypeRequests));
        //}

        public async Task<bool> DeleteAsync(int id)
        {
            return await Task.Run(() =>
            {
                // удаляем из базы данных
                GivcRequest c = db.GivcRequests.Find(id);
                db.GivcRequests.Remove(c);
                int affected = db.SaveChanges();
                if (affected == 1)
                {
                    // удаляем из кэша
                    return Task.Run(() => customersCache.TryRemove(id, out c));
                }
                else
                {
                    return null;
                }
            });
        }
        public async Task<GivcRequest> UpdateAsync(int id, GivcRequest c)
        {
            return await Task.Run(() =>
            {
                // нормализуем идентификатор клиента
                //id = id.ToUpper();
                //c.CustomerID = c.CustomerID.ToUpper();
                // обновляем в базе данных
                db.GivcRequests.Update(c);
                int affected = db.SaveChanges();
                if (affected == 1)
                {
                    // обновляем в кэше
                    return Task.Run(() => UpdateCache(id, c));
                }
                return null;
            });
        }

        private bool disposed = false;
        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
